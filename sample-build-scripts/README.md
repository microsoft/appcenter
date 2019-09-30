# App Center Build Scripts Examples

This folder contains a collection of build scripts examples for the [App Center](https://appcenter.ms) Build service. Build scripts are a powerful way to control the build process on App Center at three pre-defined stages. More details about the usage of build scripts on App Center can be found in the [documentation](https://docs.microsoft.com/en-us/appcenter/build/custom/scripts/). Feel free to use and modify the scripts as you need them.

# Table of Content

### General
- [Upload build output via SFTP](general/sftp-upload/)
- [Slack build status notification](general/slack/)
- [Notify build status via bash email](general/bash-email/)
- [Report build status next to github commit](general/github-commit-status/)
- [Change google-services.json](general/android/google-services/)
- [Rollback CocoaPods to 1.5.3](general/cocoapods-rollback)

### iOS
- [Use App Center pre-release SDK](ios/appcenter-beta-sdk/)
- [Use the same CocoaPods version as the Pod lockfile](ios/match-cocoapods-version-to-lockfile/appcenter-post-clone.sh)

### React Native
- [Run Detox](react-native/detox/)
- [Change version name](react-native/version-name)
- [Check current cocoapods version and podfile.lock](react-native/compare-cocoapods)

### Xamarin
- [Run NUnit based tests](xamarin/nunit-test/)
- [Change package name](xamarin/package-name)
- [Change version name](xamarin/version-name)
- [Change app constants](xamarin/app-constants)
- [Change resource dictionary](xamarin/resource-dictionary)
- [Change app display name](xamarin/app-displayname)

### Flutter
- [iOS Build](flutter/ios-build)
- [Android Build](flutter/android-build)
