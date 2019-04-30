#!/usr/bin/env bash
#
# Send a build notification over Email on Success/Failure of AppCenter Builds.
#
# Ensure you exclude the Sender Email Address from your Mail Spam Filters,   
# or it will go to the Junk/Spam Folder.
# The 'from' email looks like this, vsts <vsts@Mac-n.local> where n could be {1,2,3...}
# Create Rules/Filters to route to Inbox based on Subject/Sender.
#
# Modify the ORG, APP, TO_ADDRESS, SUBJECT, SUCCESS_BODY, FAILURE_BODY as required. 
#
# Add it as a Post-Build Script (appcenter-post-build.sh)
# Configure your AppCenter build(s) to ensure the Build Script is picked.
#
# Uses UNIX LF EOL format. 
#
# Contributed by Manigandan Balachandran
#

# Update your ORG name and APP name
ORG=AndroidOrg
APP=AndrXam
# This is to get the Build Details so we could pass it as part of the Email Body
build_url=https://appcenter.ms/orgs/$ORG/apps/$APP/build/branches/$APPCENTER_BRANCH/builds/$APPCENTER_BUILD_ID
# Address to send email
TO_ADDRESS="receiver@domain.com"
# A sample Subject Title 
SUBJECT="AppCenter Build"
# Content of the Email on Build-Success.
SUCCESS_BODY="Success! Your build completed successfully!\n\n"
# Content of the Email on Build-Failure.
FAILURE_BODY="Sorry! Your AppCenter Build failed. Please review the logs and try again.\n\n"
#If Agent Job Build Status is successful, Send the email, if not send a failure email.
if [ "$AGENT_JOBSTATUS" == "Succeeded" ];
then
	echo "Build Success!"
	echo -e ${SUCCESS_BODY} ${build_url} | mail -s "${SUBJECT} - Success!" ${TO_ADDRESS}
	echo "success mail sent"
else
	echo "Build Failed!"
	echo -e ${FAILURE_BODY} ${build_url} | mail -s "${SUBJECT} - Failed!" ${TO_ADDRESS}
	echo "failure mail sent"
fi
