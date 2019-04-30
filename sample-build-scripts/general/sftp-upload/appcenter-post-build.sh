#!/usr/bin/env bash
#
# Upload the binary from the build output directory via SFTP using LFTP https://lftp.yar.ru/lftp-man.html
# Make sure to create the environment variable $LFTP_PASSWORD in the App Center build configuration

brew install lftp
cd $APPCENTER_OUTPUT_DIRECTORY
lftp -f $APPCENTER_SOURCE_DIRECTORY/upload.lftp