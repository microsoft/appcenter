import * as React from 'react';
import { View, Text, Image } from 'react-native';

import BaseScreen from '../components/baseScreen';
import images from '../images';

export class BuildScreen extends React.Component {
    render() {
        return (
            <View style={{ flex: 1 }} testID="buildScreen" accessibilityLabel={"buildScreen"} accessible={true}>
                <BaseScreen options={
                    {
                        title: "Build",
                        topContainer: {
                            height: 200,
                            backgroundColor: "#0064C3",
                            imageSource: images.build
                        },
                        bottomContainer: {
                            backgroundColor: "#0078D7",
                            description: "Create an installable app package automatically with every push to your repository. Supports GitHub, or Git repos on Bitbucket and Visual Studio Team Services (VSTS).\n\nNo additional build hardware required."
                        }
                    }
                }>
                </BaseScreen>
            </View>
        );
    }
}