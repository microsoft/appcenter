# Acquaint (N)

A simple Xamarin app called Acquaint. This is the __native Xamarin__ version of Acquaint, known as Acquaint N.

##Two platforms (three platforms in the Forms version)
The app targets two platforms:
* iOS
* Android

##Integrations
Includes integrations such as:
* getting directions
* making calls
* sending text messages
* email composition

## Google Maps API key (Android)
For Android, you'll need to obtain a Google Maps API key:
https://developer.xamarin.com/guides/android/platform_features/maps_and_location/maps/obtaining_a_google_maps_api_key/

Insert it in the Android project: `~/Properties/AndroidManifest.xml`:

    <application ...>
      ...
      <meta-data android:name="com.google.android.geo.API_KEY" android:value="GOOGLE_MAPS_API_KEY" />
      ...
    </application>

## Native UI Features
| 3D Touch Previewing (iOS) | Shared View Transitions (Android) |
| --- | --- |
| <img src="https://github.com/xamarinhq/app-acquaint/blob/master/Screenshots/Acquaint_N_3DTouch.gif" width="300" /> | <img src="https://github.com/xamarinhq/app-acquaint/blob/master/Screenshots/Acquaint_N_SharedViewTransitions.gif" width="300" /> |

## Screens

The app has three main screens:
* a list screen
* a read-only detail screen
* an editable detail screen

##People

All images of people in the app come from [UIFaces.com](http://uifaces.com/authorized). In accordance with the guidelines, fictitious names have been provided.
