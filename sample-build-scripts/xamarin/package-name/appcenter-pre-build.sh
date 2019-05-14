#!/usr/bin/env bash
#
# For Xamarin Android or iOS, change the package name located in AndroidManifest.xml and Info.plist. 
# AN IMPORTANT THING: YOU NEED DECLARE PACKAGE_NAME ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION.

if [ -z "$PACKAGE_NAME" ]
then
    echo "You need define the PACKAGE_NAME variable in App Center"
    exit
fi

ANDROID_MANIFEST_FILE=$APPCENTER_SOURCE_DIRECTORY/Droid/Properties/AndroidManifest.xml
INFO_PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/iOS/Info.plist

if [ -e "$ANDROID_MANIFEST_FILE" ]
then
    echo "Updating package name to $PACKAGE_NAME in AndroidManifest.xml"
    sed -i '' 's/package="[^"]*"/package="'$PACKAGE_NAME'"/' $ANDROID_MANIFEST_FILE

    echo "File content:"
    cat $ANDROID_MANIFEST_FILE
fi


if [ -e "$INFO_PLIST_FILE" ]
then
    echo "Updating package name to $PACKAGE_NAME in Info.plist"
    plutil -replace CFBundleIdentifier -string $PACKAGE_NAME $INFO_PLIST_FILE

    echo "File content:"
    cat $INFO_PLIST_FILE
fi

