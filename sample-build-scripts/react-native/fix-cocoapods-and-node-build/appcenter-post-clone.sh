#!/usr/bin/env bash

# This post clone script prepares the build process
# in App Center for React Native 0.60 projects by
# resolving a few build errors.
#
# Put this script in your ios/ folder and commit.
#
# Please mind the version numbers so that they
# correspond to your Podfile and package.json.

echo "Uninstalling all cocoapods versions"
sudo gem uninstall cocoapods --all
echo "Installing cocoapods version 1.7.5"
sudo gem install cocoapods -v 1.7.5

# Upgrade Node to a version expected by React Native 0.60
set -ex
brew uninstall node@6
NODE_VERSION="8.10.0"
curl "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}.pkg" > "$HOME/Downloads/node-installer.pkg"
sudo installer -store -pkg "$HOME/Downloads/node-installer.pkg" -target "/"

# Run Yarn
yarn
