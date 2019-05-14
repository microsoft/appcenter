import * as React from 'react';
import { View, Text, Image, Alert, NativeModules } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';
import Analytics from 'appcenter-analytics';

export class AnalyticsScreen extends React.Component {
    render() {
        return (
            <View style={{ flex: 1 }} testID="analyticsScreen" accessibilityLabel={"analyticsScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "Analytics",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#FDAD00",
                            imageSource: images.analytics
                        },
                        bottomContainer: {
                            backgroundColor: "#FFC326",
                            description: "Get information about the number of daily, weekly, and monthly users, session duration, the top devices and more. Use events to collect rich behavioral analytics data.",
                            topButton: {
                                text: "Send a sample event",
                                onPress: () => {
                                    Analytics.trackEvent("Sample event", {});
                                    Alert.alert(
                                        null,
                                        'Event sent',
                                        [
                                            { text: 'Ok' },
                                        ],
                                        { cancelable: false }
                                    )
                                }
                            },
                            bottomButton: {
                                text: "Send event with color property",
                                onPress: () => {
                                    NativeModules.AppCenterSample.showColoredEventsDialog();
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