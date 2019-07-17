#!/usr/bin/env bash
#
# For Android/iOS apps, update the contents of the appcenter-config.json/ AppCenter-Config.plist file.
# this is useful when having muliple variations for the app, and need to track each variation independently using dedicated app token which is provided by the ms app center.
#
# DECLARE THE APP_CENTER_TRACKING_JSON ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION, SET
# TO THE CONTENTS OF YOUR appcenter-config.json FILE

if [ -z "$APP_CENTER_TRACKING_JSON" ]
then
    echo "You need define the APP_CENTER_TRACKING_JSON variable in App Center"
    exit
fi

if [ -z "$APP_CENTER_CURRENT_PLATFORM" ]
then
    echo "You need define the APP_CENTER_CURRENT_PLATFORM variable in App Center with values android or ios"
    exit
fi

# This is the path to the appcenter-config.json file

if [ "$APP_CENTER_CURRENT_PLATFORM" == "android" ]
then
    CONFIG_FILE="appcenter-config.json"
    APP_CENTER_TRACKING_JSON_FILE=$APPCENTER_SOURCE_DIRECTORY/android/app/src/main/assets/$CONFIG_FILE
else
    #iOS
    CONFIG_FILE="AppCenter-Config.plist"
    APP_NAME="my-app-name"
    APP_CENTER_TRACKING_JSON_FILE=$APPCENTER_SOURCE_DIRECTORY/ios/$APP_NAME/$CONFIG_FILE
fi


echo "Updating $CONFIG_FILE"

echo "$APP_CENTER_TRACKING_JSON" > $APP_CENTER_TRACKING_JSON_FILE
sed -i -e 's/\\"/'\"'/g' $APP_CENTER_TRACKING_JSON_FILE

echo "File content:"
cat $APP_CENTER_TRACKING_JSON_FILE
