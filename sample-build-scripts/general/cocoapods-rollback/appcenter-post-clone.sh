#!/usr/bin/env bash
#
# Removes the latest version of CocoaPods  which may have lack of backward compatibilities
# Installs version 1.5.3 which has no compatibility issues
# 1.5.3 can be changed to another version you'd prefer


echo "uninstalling all cocoapods versions"
sudo gem uninstall cocoapods -ax
echo "installing cocoapods version 1.5.3"
sudo gem install cocoapods -v 1.5.3
