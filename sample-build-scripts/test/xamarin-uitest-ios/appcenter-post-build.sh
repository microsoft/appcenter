#!/usr/bin/env bash

echo "Found UI tests projects:"
find $APPCENTER_SOURCE_DIRECTORY -regex '*.UITest.*\.csproj' -exec echo {} \;
echo
echo "Building UI test projects:"
find $APPCENTER_SOURCE_DIRECTORY -name '*.UITest.csproj' -exec msbuild {} \;
echo "Compiled projects to run UI tests:"
find $APPCENTER_SOURCE_DIRECTORY -regex '*.bin.*UITest.*\.dll' -exec echo {} \;
#echo "Running UI test in App Center Test:"
echo "What is in the output directory"
ls $APPCENTER_OUTPUT_DIRECTORY
echo "What is in the source directory"
APPPATH=$APPCENTER_OUTPUT_DIRECTORY/PrettyWeather.iOS.ipa #-regex '*.bin.*ipa' echo {} \;
BUILDDIR=$APPCENTER_SOURCE_DIRECTORY/PrettyWeather.UITest/bin/Debug/ #-regex '*.bin.*UITest.*\' -exec echo {} \;
appcenter test run uitest --app $APP_OWNER --devices $DEVICE_SET --test-series "$APPCENTER_BRANCH-$APPCENTER_TRIGGER" --locale $LOCALE --app-path $APPPATH --build-dir $BUILDDIR --async --uitest-tools-dir $APPCENTER_SOURCE_DIRECTORY/packages/Xamarin.UITest.*/tools --token $APPCENTER_TOKEN
