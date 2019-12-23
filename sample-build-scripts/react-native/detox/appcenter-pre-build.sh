#!/usr/bin/env bash

brew tap wix/brew
brew update
brew install applesimutils

# Change ios.sim.release to the detox configuration you want to run

echo "Building the project for Detox tests..."
npx detox build --configuration ios.sim.release

echo "Executing Detox tests..."
npx detox test --configuration ios.sim.release --cleanup
