# Report build status next to github commit.
# 
# - Fill in USER and APP with your user and appname in appcenter
# - Also provide GITHUB_TOKEN env variable in build config
#   (create token in GitHub Settings / Developer Settings / Personal access tokens, with 'repo:status' scope)
#
# Contributed by: Mirosław Zawisza
# https://zeyomir.github.io

USER=
APP=

build_url=https://appcenter.ms/users/$USER/apps/$APP/build/branches/$APPCENTER_BRANCH/builds/$APPCENTER_BUILD_ID

github_set_status() {
    local status job_status
    local "${@}"

    curl -X POST https://api.github.com/repos/$USER/$BUILD_REPOSITORY_NAME/statuses/$BUILD_SOURCEVERSION -d \
        "{
            \"state\": \"$status\", 
            \"target_url\": \"$build_url\",
            \"description\": \"The build status is: $job_status!\",
            \"context\": \"continuous-integration/appcenter\"
        }" \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3.raw+json"
}

github_set_status_pending() {
    github_set_status status="pending" job_status="In progress"
}

github_set_status_success() {
    github_set_status status="success" job_status="$AGENT_JOBSTATUS"
}

github_set_status_fail() {
    github_set_status status="failure" job_status="$AGENT_JOBSTATUS"
}

