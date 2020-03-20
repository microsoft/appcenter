#!/bin/bash
# Customize this script to download build data you have permission to access in App Center. 

TEAM_APP='ORG_NAME/APP_NAME'

for i in {1..10} #downloads data from builds #1 - #10
do
   eval appcenter build download --id "$i" --app $TEAM_APP --type "logs"    
   #eval appcenter build download --id "$i" --app $TEAM_APP --type "build" #uncomment to download app packages
   #eval appcenter build download --id "$i" --app $TEAM_APP --type "symbols" #uncomment to download symbols
done
