#!/usr/bin/env bash
#
# For Xamarin, change some resource dictionary value located in App.xaml. 
#
# Sample of App.xaml file:
#
# <?xml version="1.0" encoding="utf-8"?>
# <Application xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Core.App">
# 	<Application.Resources>
#         <ResourceDictionary>
#             <Color x:Key="MainColor">#00FF00</Color>
#         </ResourceDictionary>
# 	</Application.Resources>
# </Application>
#
# In this sample, we change MainColor value in build time.
# AN IMPORTANT THING: YOU NEED DECLARE MAIN_COLOR ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION.
#

if [ -z "$MAIN_COLOR" ]
then
    echo "You need define the MAIN_COLOR variable in App Center"
    exit
fi

APP_FILE=$APPCENTER_SOURCE_DIRECTORY/Core/App.xaml

if [ -e "$APP_FILE" ]
then
    echo "Updating main color to $MAIN_COLOR in App.xaml"
    sed -i '' 's/"MainColor">[a-zA-Z0-9#]*</"MainColor">'$MAIN_COLOR'</' $APP_FILE

    echo "File content:"
    cat $APP_FILE
fi

