#!/usr/bin/env bash
#
# Report build status next to github commit.
#
# Adjust settings in github.sh file
#
# Contributed by: Mirosław Zawisza
# https://zeyomir.github.io

source github.sh

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    github_set_status_fail
else
    github_set_status_success
fi

