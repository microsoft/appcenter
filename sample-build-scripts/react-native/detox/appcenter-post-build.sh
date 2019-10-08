#!/usr/bin/env bash
#
# Run Detox.
#
# Notes:
# 1) This script was originally designed to work with the wix Detox 
#    sample app located in the main repo here: http://www.github.com/wix/detox. 
#
# 2) Currently there are issues suppresing UI's when executing
#    `brew tap` commands so we chose to install applesimutils
#    from source instead.
#
# 3) Your Node version and Detox build command might be slightly different.
#
# 4) If you're using yarn, make sure to use it instead of npm, to avoid clashes in dependencies 


echo "Installing applesimutils..."
mkdir simutils
cd simutils
curl -L https://github.com/wix/AppleSimulatorUtils/archive/0.7.2.tar.gz -o applesimutils.tar.gz
tar xzvf applesimutils.tar.gz
sh buildForBrew.sh .
cd ..
export PATH=$PATH:./simutils/build/Build/Products/Release

echo "Installing NVM..."
brew install nvm
source $(brew --prefix nvm)/nvm.sh

echo "Installing v8.5..."
nvm install v8.5.0
nvm use --delete-prefix v8.5.0
nvm alias default v8.5.0

echo "Identifying selected node version..."
node --version

echo "Installing detox cli..."
npm install -g detox-cli

echo "Installing dependencies for detox tests..."
npm install

echo "Building the project..."
detox build --configuration ios.sim.release

echo "Executing tests..."
detox test --configuration ios.sim.release --cleanup
