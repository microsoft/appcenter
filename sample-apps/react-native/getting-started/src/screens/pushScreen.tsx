import * as React from 'react';
import { View, Text, Image } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';

export class PushScreen extends React.Component {
    render() {
        return (
            <View style={{ flex: 1 }} testID="pushScreen" accessibilityLabel={"pushScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "Push",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#E2553D",
                            imageSource: images.push
                        },
                        bottomContainer: {
                            backgroundColor: "#F56D4F",
                            description: "Engage your users by sending them targeted messages to specific sets of users at exactly the right time.Create segments of users based on device and custom properties."
                        }
                    }
                }>
                </BaseScreen>
            </View>
        );
    }
}