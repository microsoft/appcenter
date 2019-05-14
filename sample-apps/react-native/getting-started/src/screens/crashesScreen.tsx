import * as React from 'react';
import { View, Text, Image, Alert } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';
import Crashes from 'appcenter-crashes';

export class CrashesScreen extends React.Component {

    render() {
        return (
            <View style={{ flex: 1 }} testID="crashesScreen" accessibilityLabel={"crashesScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "Crashes",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#6FA22E",
                            imageSource: images.crashes
                        },
                        bottomContainer: {
                            backgroundColor: "#91CA47",
                            description: "Collect crashes from all devices, prioritize them based on the number of users seeing the crash, and get the full stack traces to help you fix them.\n\nBacked by HockeyApp.",
                            bottomButton: {
                                text: "Send crash report",
                                onPress: () => {
                                    Alert.alert(
                                        null,
                                        'A crash report will be sent when you reopen the app.',
                                        [
                                            { text: 'Cancel' },
                                            { text: 'Crash App', onPress: () => Crashes.generateTestCrash() },
                                        ],
                                        { cancelable: false }
                                    )
                                }
                            }
                        }
                    }
                }>
                </BaseScreen>
            </View>
        );
    }
}