#!/usr/bin/env bash
#Place this script in project/android/app/

#!/usr/bin/env bash
set -ex

PROJECT_DIR=$(dirname $(dirname $(pwd)))

mkdir ~/dev && cd $_

git clone -b stable https://github.com/flutter/flutter.git
export PATH=`pwd`/flutter/bin:$PATH

flutter channel stable && flutter upgrade
flutter doctor

echo "Installed flutter to `pwd`/flutter"

# Go to project directory
cd $PROJECT_DIR

# build APK
# if you get "Execution failed for task ':app:lintVitalRelease'." error, uncomment next two lines
# flutter build apk --debug
# flutter build apk --profile
flutter build apk --release

# if you need build bundle (AAB) in addition to your APK, uncomment line below and last line of this script.
#flutter build appbundle --release --build-number $APPCENTER_BUILD_ID

# copy the APK where AppCenter will find it
mkdir -p android/app/build/outputs/apk/; mv build/app/outputs/apk/release/app-release.apk $_

# copy the AAB where AppCenter will find it
#mkdir -p android/app/build/outputs/bundle/; mv build/app/outputs/bundle/release/app-release.aab $_
