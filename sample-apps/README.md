# Sample Applications

This directory contains Git - Submodule references to the App Center Samples we have posted on GitHub.

## Viewing from GitHub

Simply click on the directories of any of the samples we have to be redirected over to the corresponding Git Repository for them:

- [Android Java Sample](android/getting-started)
- [iOS Swift Sample](ios/getting-started)
- [macOS Swift Sample](macos/getting-started)
- [React Native Sample](react-native/getting-started)
- [Xamarin Sample](xamarin/getting-started)

## Viewing after git clone

After cloning this respository, the submodules will not be cloned yet. You'll need to execute the following commands to also clone the submodules:

```shell
    git submodule init
    git submodule update
```

> Note: Some git clients automatically perform these steps for you

## Viewing after .zip download

After downloading this repository, the version information will not be available since git information and metadata is striped out of the repository when downloading as a .zip file.

But the submodules should be included in the files with the .zip download.
