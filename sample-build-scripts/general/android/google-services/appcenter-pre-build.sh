#!/usr/bin/env bash
#
# For Android apps, update the contents of the google-service.json file.
# This can be used for an app that is open source, or to have a different configuration
# for different branches.
#
# Suppose in our project exists two branches: master and develop. 
# We can release an app for production push notifications from the master branch 
# and a version of the app for test push notifications from the develop branch. 
# We just need configure this behaviour with environment variable in each branch :)
#
# DECLARE THE GOOGLE_SERVICES_JSON ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION, SET
# TO THE CONTENTS OF YOUR google-services.json FILE

if [ -z "$GOOGLE_SERVICES_JSON" ]
then
    echo "You need define the GOOGLE_SERVICES_JSON variable in App Center"
    exit
fi

# This is the path to the google-services.json file, Update 'Android' to be the
# correct path to the file relative to the root of your repo
GOOGLE_SERVICES_JSON_FILE=$APPCENTER_SOURCE_DIRECTORY/Android/google-services.json

if [ -e "$GOOGLE_SERVICES_JSON_FILE" ]
then
    echo "Updating google-services.json"
    echo "$GOOGLE_SERVICES_JSON" > $GOOGLE_SERVICES_JSON_FILE
    sed -i -e 's/\\"/'\"'/g' $GOOGLE_SERVICES_JSON_FILE

    echo "File content:"
    cat $GOOGLE_SERVICES_JSON_FILE
fi

