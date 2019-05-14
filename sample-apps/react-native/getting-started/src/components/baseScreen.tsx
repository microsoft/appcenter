import * as React from 'react';
import { StyleSheet, View, Text, Image, TouchableOpacity, ImageRequireSource } from 'react-native';
import CodePush from '../components/codePush';
import { ifIphoneX } from 'react-native-iphone-x-helper'

export type Props = {
    options: {
        title: string,
        topContainer: {
            height: number,
            backgroundColor: string,
            imageSource: ImageRequireSource,
        },
        bottomContainer: {
            backgroundColor: string,
            description: string,
            topButton?: {
                text: string,
                onPress: () => void
            }
            bottomButton?: {
                text: string,
                onPress: () => void
            }
        },
        codepush?: boolean
    }
}

export default class BaseScreen extends React.Component<Props> {
    codepush: CodePush

    constructor(props: Props) {
        super(props);
        this.state = {};
    }

    render() {
        const createButtonView = (buttonProps) => {
            if (buttonProps) {
                const onPress = () => {
                    if (buttonProps.onPress) {
                        buttonProps.onPress();
                    }
                }

                return (
                    <View style={styles.buttonView}>
                        <TouchableOpacity
                            style={styles.button}
                            onPress={onPress}>
                            <Text style={styles.buttonText}>{buttonProps.text}</Text>
                        </TouchableOpacity>
                    </View>
                );
            }
        }

        const bottomButtonView = createButtonView(this.props.options.bottomContainer.bottomButton);
        const topButtonView = createButtonView(this.props.options.bottomContainer.topButton);
        let codePushComponent;
        const self = this;
        if (this.props.options.codepush) {
            codePushComponent = <CodePush ref={codepush => { self.codepush = codepush }} />;
        }

        return (
            <View style={{ flex: 1, ...ifIphoneX({paddingTop: 30}, {paddingTop: 0}) }}>
                <View style={styles.headerContainer}>
                    <View style={{ flex: 1 }} />
                    <View style={styles.headerTextContainer}>
                        <Text style={styles.headerText}>{this.props.options.title}</Text>
                    </View>
                </View>
                <View style={{
                    height: this.props.options.topContainer.height,
                    backgroundColor: this.props.options.topContainer.backgroundColor,
                }} >
                    <Image style={styles.topImage} source={this.props.options.topContainer.imageSource} />
                </View>
                <View style={{
                    flex: 1,
                    backgroundColor: this.props.options.bottomContainer.backgroundColor
                }} >
                    <Text style={[styles.descriptionText, styles.mainText]}>{this.props.options.bottomContainer.description}</Text>
                    <View style={{ flex: 2 }}>
                        <View style={{ flex: 1 }} />
                        {codePushComponent}
                        {topButtonView}
                        {bottomButtonView}
                        <View style={{ height: 40 }} />
                    </View>
                </View>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    mainText: {
        color: "white",
        fontSize: 17
    },
    headerContainer: {
        backgroundColor: "#252525",
        height: 40,
        justifyContent: 'center',
        alignItems: 'center',
    },
    headerTextContainer: {
        borderBottomWidth: 3,
        borderBottomColor: "white",
        paddingHorizontal: 15,
        paddingBottom: 4
    },
    headerText: {
        color: "white",
        fontSize: 18
    },
    topImage: {
        width: "100%",
        height: "100%"
    },
    descriptionText: {
        flexGrow: 1,
        alignSelf: "center",
        marginTop: 24,
        width: 300,
    },
    buttonView: {
        height: 55,
        width: 300,
        alignSelf: "center"
    },
    infoView: {
        height: 100,
        alignSelf: "center",
        borderWidth: 2
    },
    button: {
        alignItems: 'center',
        justifyContent: 'center',
        paddingVertical: 12,
        paddingHorizontal: 12,
        borderRadius: 2,
        backgroundColor: "white",
    },
    buttonText: {
        fontSize: 17,
        color: "black",
    }
});