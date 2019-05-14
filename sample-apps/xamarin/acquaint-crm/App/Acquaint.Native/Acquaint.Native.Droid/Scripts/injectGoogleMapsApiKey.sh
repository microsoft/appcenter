#!/bin/bash

sed -i '' "s/GOOGLE_MAPS_API_KEY/$2/g" "$1/Properties/AndroidManifest.xml"