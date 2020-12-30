#!/usr/bin/env bash


# Variables
APPSETTINGS_FILE=$APPCENTER_SOURCE_DIRECTORY/StatusChecker/appsettings.json
# ./Variables


# Replacing AppSettings.Json
if [ -e "$APPSETTINGS_FILE" ]
then
    echo "Updating configuration in appsettings.json"
    sed -i '' 's/"WebRequestUsername": ""/"WebRequestUsername": "'$CONF_WEBREQUESTUSERNAME'"/' $APPSETTINGS_FILE
    sed -i '' 's/"WebRequestPassword": ""/"WebRequestPassword": "'$CONF_WEBREQUESTPASSWORD'"/' $APPSETTINGS_FILE

    sed -i '' 's/"AppCenterSecretForms": ""/"AppCenterSecretForms": "'$CONF_APPCENTERSECRETFORMS'"/' $APPSETTINGS_FILE
    sed -i '' 's/"AppCenterSecretIOS": ""/"AppCenterSecretIOS": "'$CONF_APPCENTERSECRETIOS'"/' $APPSETTINGS_FILE

    sed -i '' 's/"InitialStatusRequestUrl": ""/"InitialStatusRequestUrl": "'$CONF_INITIALSTATUSREQUESTURL'"/' $APPSETTINGS_FILE
fi
# ./Replacing AppSettings.Json