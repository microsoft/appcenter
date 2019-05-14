import * as React from 'react';
import { StyleSheet, View, Text, Image, TouchableOpacity } from 'react-native';
import CodePushLib from 'react-native-code-push';

type State = {
    syncMessage?: string,
    progress?: {
        receivedBytes: number,
        totalBytes: number
    }
}

export default class CodePush extends React.Component<{}, State> {
    constructor(props: {}, state: State) {
        super(props, state);
        this.state = {};
    }

    codePushStatusDidChange(syncStatus) {
        switch (syncStatus) {
            case CodePushLib.SyncStatus.CHECKING_FOR_UPDATE:
                this.setState({ syncMessage: "Checking for update." });
                break;
            case CodePushLib.SyncStatus.DOWNLOADING_PACKAGE:
                this.setState({ syncMessage: "Downloading package." });
                break;
            case CodePushLib.SyncStatus.AWAITING_USER_ACTION:
                this.setState({ syncMessage: "Awaiting user action." });
                break;
            case CodePushLib.SyncStatus.INSTALLING_UPDATE:
                this.setState({ syncMessage: "Installing update." });
                break;
            case CodePushLib.SyncStatus.UP_TO_DATE:
                this.setState({ syncMessage: "App up to date.", progress: null });
                break;
            case CodePushLib.SyncStatus.UPDATE_IGNORED:
                this.setState({ syncMessage: "Update cancelled by user.", progress: null });
                break;
            case CodePushLib.SyncStatus.UPDATE_INSTALLED:
                this.setState({ syncMessage: "Update installed and will be applied on restart.", progress: null });
                break;
            case CodePushLib.SyncStatus.UNKNOWN_ERROR:
                this.setState({ syncMessage: "An unknown error occurred.", progress: null });
                break;
        }
    }

    sync = () => {
        CodePushLib.sync(
            { updateDialog: CodePushLib.DEFAULT_UPDATE_DIALOG },
            this.codePushStatusDidChange.bind(this),
            this.codePushDownloadDidProgress.bind(this)
        );
    }

    codePushDownloadDidProgress(progress) {
        this.setState({ progress });
    }

    render() {

        let progressView;
        if (this.state.progress) {
            progressView = (
                <Text style={styles.status}>{this.state.progress.receivedBytes} of {this.state.progress.totalBytes} bytes received</Text>
            );
        }

        return (
            <View style={styles.infoView}>
                <View style={{ flex: 1 }} />
                <Text style={styles.message}>{this.state.syncMessage || ""}</Text>
                {progressView}
            </View>
        );
    }
}

const styles = StyleSheet.create({
    infoView: {
        height: 60,
        alignSelf: "center"
    },
    message: {
        color: "white",
        fontSize: 17,
    },
    status: {
        color: "white",
        fontSize: 14,
    }
});