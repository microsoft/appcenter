#!/usr/bin/env bash -e
#
# Send a slack notification specifying whether or
# not a build successfully completed.
#
# Contributed by: David Siegel
# https://github.com/quicktype/quicktype/

cd $APPCENTER_SOURCE_DIRECTORY

ORG=quicktype
APP=quicktype

ICON=https://pbs.twimg.com/profile_images/881784177422725121/hXRP69QY_200x200.jpg

build_url=https://appcenter.ms/orgs/$ORG/apps/$APP/build/branches/$APPCENTER_BRANCH/builds/$APPCENTER_BUILD_ID
build_link="<$build_url|$APP $APPCENTER_BRANCH #$APPCENTER_BUILD_ID>"

version() {
    cat package.json | jq -r .version
}

slack_notify() {
    local message
    local "${@}"

    curl -X POST --data-urlencode \
        "payload={
            \"channel\": \"#notifications\",
            \"username\": \"App Center\",
            \"text\": \"$message\",
            \"icon_url\": \"$ICON\" \
        }" \
        $SLACK_WEBHOOK
}

slack_notify_build_passed() {
    slack_notify message="✓ $build_link built"
}

slack_notify_build_failed() {
    slack_notify message="💥 $build_link build failed"
}

slack_notify_deployed() {
    slack_notify message="✓ <$build_url|$APP v`version`> released to npm"
}

slack_notify_homebrew_bump() {
    slack_notify message="✓ <https://github.com/Homebrew/homebrew-core/pulls|$APP v`version`> bump PR sent to Homebrew"
}

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    slack_notify_build_failed
    exit 0
fi

