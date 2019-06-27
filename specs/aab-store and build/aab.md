# App Center Stores and Build support for Android App Bundles
	
This proposal will cover a new feature to to add support for Android App Bundles in App Center.
This feature will only cover support for .aab packages in the Stores service, and Build.
	
## Problem Statement
	
Google has introduced Android App Bundles (AAB) and it's been the recommended method to upload packages to the Play Store.

The company has also announced that By August 1, 2019, all apps that use native code must provide a 64-bit version in addition to the 32-bit version in order to publish an update. 
Using Android App Bundle feature will minimize the size impact of including both 32- and 64-bit native code in the same APK.

App Center today offers the ability to build and upload .apk files to Google Play Store. Users looking to start using bundles will not be able to upload them via App Center today.
   
## Goal
Given that users are looking to utilize Android App bundles, they can use App Center to Build the bundle and distribute it to the Google Play store directly from App Center.
	
## Solution
	
### Build
* Given an Android project where the configuration contains a bundle, then users will see a new setting under build configuration for building an Android Bundle package.

* App Center will offer a new selector in the Android build configuration: "Also Build an Android Bundle".
This will be pre-selected if the module selected in the configuration screen contains a "bundle {...}" block.

* After the build is complete, the bundle will be available as a separate downloadable artifact named "android-app-bundle"

* Signing the bundle

    Building the bundle needs to honor the signing settings of the app. This means the following:
    - If the build configuration is configured to rely on the automatic Gradle signing, the bundle should also assumed to be signed automatically during the Gradle task.
    - If manual signing is enabled, the resulting .aab file needs to be signed separately.
    - For manual signing, the app bundle should be signed with the same credentials as the APK
    
### Stores

Given an Android App Center user, where the user would like to upload an Android App Bundle to the Google Play Store directly from App Center, then user can upload an .aab file through the App Center UI, API, CLI, Azure DevOps task, or the Fastlane plug-in.

The experience to upload the .aab file will remain the same as upload an .apk file to one of the Google Play tracks (production, alpha, or beta).

To publish directly from App Center, users will have to have at least one release using Android App Bundle directly from the Google Play console to ensure they have enrolled in app signing.

### Distribute an App Bundle package from Build to Store

Given an Android app on App Center where the user is using App Center Build to build an Android App Bundle, and have previously configured a Google Play Store connection from App Center Store, then users can automate distributing the App Bundle to the Store after a complete build.

## Designs
	
To be shared
	
## API Experience
	
To be shared
	
## Limitations and Out of Scope
	
* Distributing to users and/or distirbution groups is out of scope for this solution.
* Installing a package after releasing to Store is out of scope for this solution.