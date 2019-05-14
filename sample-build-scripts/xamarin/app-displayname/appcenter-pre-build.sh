#!/usr/bin/env bash
#
# For Xamarin Android or iOS, change the package name located in MainActivity.cs and Info.plist. 
# In this sample, suppose we have DAP-branches and for each distribution we want to rename the displayname of the app.
# On each branch define a build and configure the Environment Variables for the app name to display
# Dev. Env.  | My App - Dev
# Acc. Env.  | My App - Acc
# Prod. Env. | My App
# 
# A FEW IMPORTANT THINGS: 
# - YOU NEED TO DECLARE APP_DISPLAY_NAME ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION.
#
# - Check if the app name is set on the Activity within the MainActivity.cs with Label = "{Your app name}"
#       namespace MyXamarinFormsApp.Droid
#       {
#           [Activity(Label = "My XF App", Icon = "....
#           public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
#           ...
#       }
#
# - Check if the app name is set in the Info.plist like this:
#   <key>CFBundleDisplayName</key>
#   <string>My XF App</string>

echo "##[warning][Pre-Build Action] - Lets do some Pre build transformations..."

# Declare local script variables
SCRIPT_ERROR=0

# Define the files to manipulate
INFO_PLIST_FILE=${APPCENTER_SOURCE_DIRECTORY}/My_XF_App.iOS/Info.plist
ANDROID_MAINACTIVITY_FILE=${APPCENTER_SOURCE_DIRECTORY}/My_XF_App.Droid/MainActivity.cs

echo "##[warning][Pre-Build Action] - Checking if all files and environment variables are available..."

if [ -z "${APP_DISPLAY_NAME}" ]
then
    echo "##[error][Pre-Build Action] - APP_DISPLAY_NAME variable needs to be defined in App Center!!!"
    let "SCRIPT_ERROR += 1"
    else
    echo "##[warning][Pre-Build Action] - APP_DISPLAY_NAME variable - oK!"
fi

if [ -e "${INFO_PLIST_FILE}" ]
then
    echo "##[warning][Pre-Build Action] - Info.plist file found - oK!"
else
    echo "##[error][Pre-Build Action] - Info.plist file not found!"
    let "SCRIPT_ERROR += 1"
fi

if [ -e "${ANDROID_MAINACTIVITY_FILE}" ]
then
    echo "##[warning][Pre-Build Action] - MainActivity file found - oK!"
else
    echo "##[error][Pre-Build Action] - MainActivity file not found!"
    let "SCRIPT_ERROR += 1"
fi

if [ ${SCRIPT_ERROR} -gt 0 ]
then
    echo "##[error][Pre-Build Action] - There are ${SCRIPT_ERROR} errors."
    echo "##[error][Pre-Build Action] - Fix them and try again..."
    exit 1 # this will kill the build
    # exit # this will exit this script, but continues building
    else
fi

echo "##[warning][Pre-Build Action] - There are ${SCRIPT_ERROR} errors."
echo "##[warning][Pre-Build Action] - Now everything is checked, lets change the app display name on iOS and Android..."

######################## Changes on Android
if [ -e "${ANDROID_MAINACTIVITY_FILE}" ]
then
    echo "##[command][Pre-Build Action] - Changing the App display name on Android to: ${APP_DISPLAY_NAME} "
    sed -i '' "s/Label = \"[-a-zA-Z0-9_ ]*\"/Label = \"${APP_DISPLAY_NAME}\"/" ${ANDROID_MAINACTIVITY_FILE}

    echo "##[section][Pre-Build Action] - MainActivity.cs File content:"
    cat ${ANDROID_MAINACTIVITY_FILE}
    echo "##[section][Pre-Build Action] - MainActivity.cs EOF"
fi

######################## Changes on iOS
if [ -e "$INFO_PLIST_FILE" ]
then
    echo "##[command][Pre-Build Action] - Changing the App display name on iOS to: $APP_DISPLAY_NAME "
    plutil -replace CFBundleDisplayName -string "$APP_DISPLAY_NAME" $INFO_PLIST_FILE

    echo "##[section][Pre-Build Action] - Info.plist File content:"
    cat $INFO_PLIST_FILE
    echo "##[section][Pre-Build Action] - Info.plist EOF"
fi