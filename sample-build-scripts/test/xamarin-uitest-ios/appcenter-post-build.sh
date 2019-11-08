#!/usr/bin/env bash
set -e

##
# Environment variables required in App Center Build
# https://docs.microsoft.com/en-us/appcenter/build/custom/variables/
#
# APPCENTER_TOKEN
# https://docs.microsoft.com/en-us/appcenter/api-docs/
#
# DEVICE_SET
# Both the device slug and a defined device set can be used here
#
# APP_OWNER
# It's a combinations of Organization and app name, i.e., {Organization/App-name}
#
# LOCALE
# UTF-8 locale like en_US for english American
#
# If you're having trouble setting valid values for DEVICE_SET, APP_OWNER & LOCALE,
# you can generate a prototype command line to help: 
# https://docs.microsoft.com/en-us/appcenter/test-cloud/starting-a-test-run#new-test-run
##


if find $APPCENTER_SOURCE_DIRECTORY -name '*.UITest.csproj';
then
	echo "Building UI test projects:"
	find $APPCENTER_SOURCE_DIRECTORY -name '*.UITest.csproj' -exec msbuild {} \;
else
	echo "Can't find UI test project"
	exit 9999
fi
echo "Compiled projects to run UI tests:"
find $APPCENTER_SOURCE_DIRECTORY -regex '*.bin.*UITest.*\.dll' -exec echo {} \;
echo "Running test in App Center Test"
APPPATH=$APPCENTER_OUTPUT_DIRECTORY/*.ipa
BUILDDIR=$APPCENTER_SOURCE_DIRECTORY/*.UITest/bin/Debug/
UITESTTOOL=$APPCENTER_SOURCE_DIRECTORY/packages/Xamarin.UITest.*/tools
appcenter test run uitest --app $APP_OWNER --devices $DEVICE_SET --test-series "$APPCENTER_BRANCH-$APPCENTER_TRIGGER" --locale $LOCALE --app-path $APPPATH --build-dir $BUILDDIR --async --uitest-tools-dir $UITESTTOOL --token $APPCENTER_TOKEN
