#!/usr/bin/env bash

echo "Starting App Center post-clone script"

# If your CI output will eventually be uploaded to App Store Connect, the build
# numbers have to be increasing for the upload to succeed. You can accomplish this
# in many ways. This is a sample script that automates this via agvtool.

# This example will set the build number to the number of commits on this branch.
# Since that only goes up under normal circumstances, it's sufficent to satisfy
# the "ever increasing build number" criteria of App Store Connect.
# See https://git-scm.com/docs/git-rev-list for use of the rev-list command
NUM_COMMITS=$(git rev-list --count HEAD)

# Use the agvtool to alter build numbers in the project. For more info see:
# https://developer.apple.com/library/archive/qa/qa1827/_index.html
echo "There are $NUM_COMMITS commits on the current branch. Setting that as the build number."
xcrun agvtool new-version $NUM_COMMITS

# ... and a build that happens after this script will use the updated build number!

# Whether you commit this update to the project file to source control is up to you.
# There is no need to do so in this simple case, since the build number will be
# explicitly set during each build. In other words, the build number in the repo is
# essentially ignored.
