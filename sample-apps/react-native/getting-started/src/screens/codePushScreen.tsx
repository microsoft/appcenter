import * as React from 'react';
import { View, Text, Image, Alert, NativeModules } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';

export class CodePushScreen extends React.Component {

    baseScreen: BaseScreen

    render() {
        let self = this;
        return (
            <View style={{ flex: 1 }}  testID="codePushScreen" accessibilityLabel={"codePushScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "CodePush",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#00695c",
                            imageSource: images.codepush
                        },
                        bottomContainer: {
                            backgroundColor: "#009688",
                            description: "Deploy mobile app updates directly to their users' devices.",
                            bottomButton: {
                                text: "Sync",
                                onPress: () => {
                                    self.baseScreen.codepush.sync();
                                }
                            }
                        },
                        codepush: true
                    }
                } ref={baseScreen => { this.baseScreen = baseScreen }}>
                </BaseScreen>
            </View>
        );
    }
}