import * as React from 'react';
import { View, Text, Image } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';

export class DistributeScreen extends React.Component {
    render() {
        return (
            <View style={{ flex: 1 }} testID="distributeScreen" accessibilityLabel={"distributeScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "Distribute",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#38A495",
                            imageSource: images.distribute
                        },
                        bottomContainer: {
                            backgroundColor: "#44B8A8",
                            description: "Users can install the app via email distribution lists for testing, much as they\'d download an app from the app store.\n\nBacked by HockeyApp."
                        }
                    }
                }>
                </BaseScreen>
            </View>
        );
    }
}