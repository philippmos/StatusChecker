#!/usr/bin/env bash


# Variables
APPSETTINGS_FILE = $APPCENTER_SOURCE_DIRECTORY/StatusChecker/appsettings.json
# ./Variables


# Replacing AppSettings.Json
if [ -e "$APPSETTINGS_FILE" ]
then
    echo "Updating configuration in appsettings.json"
    sed -i '' 's/"WebRequestUsername": ""/"WebRequestUsername": "usernameTest"/' $APPSETTINGS_FILE
    sed -i '' 's/"WebRequestPassword": ""/"WebRequestPassword": "test12345"/' $APPSETTINGS_FILE

    echo "File content:"
    cat $APPSETTINGS_FILE
fi
# ./Replacing AppSettings.Json