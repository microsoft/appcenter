#!/usr/bin/env bash
#
# For Android/iOS apps, update the contents of the appcenter-config.json/ AppCenter-Config.plist file.
# This can be used for an app that is open source, or to have a different configuration
# for different branches.
#
# Suppose in our project exists two branches: master and develop.
# We can release an app for production push notifications from the master branch
# and a version of the app for test push notifications from the develop branch.
# We just need configure this behaviour with environment variable in each branch :)
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
