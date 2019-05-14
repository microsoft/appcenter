import * as React from 'react';
import { View, Text, Image } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';

export class TestScreen extends React.Component {
    render() {
        return (
            <View style={{ flex: 1 }} testID="testScreen" accessibilityLabel={"testScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "Test",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#3192B3",
                            imageSource: images.test
                        },
                        bottomContainer: {
                            backgroundColor: "#24B8D4",
                            description: "Run your tests on more than 400 unique device configurations. Tests can be written for iOS and Android apps with Xamarin.UITest, Appium, Espresso (Android), and XCUITest (iOS).\n\nBacked by Xamarin Test Cloud."
                        }
                    }
                }>
                </BaseScreen>
            </View>
        );
    }
}