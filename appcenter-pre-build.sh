#!/usr/bin/env bash


# Variables
APPSETTINGS_FILE=$APPCENTER_SOURCE_DIRECTORY/StatusChecker/appsettings.json
PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/StatusChecker.iOS/Info.plist
APPVERSION_FILE=$APPCENTER_SOURCE_DIRECTORY/appversion.txt
# ./Variables


# Replacing appsettings.json
if [ -e "$APPSETTINGS_FILE" ]
then
    echo "Updating configuration in appsettings.json"
    sed -i '' 's/"AppBuildNumber": ""/"AppBuildNumber": "'$APPCENTER_BUILD_ID'"/' $APPSETTINGS_FILE

    sed -i '' 's/"WebRequestUsername": ""/"WebRequestUsername": "'$CONF_WEBREQUESTUSERNAME'"/' $APPSETTINGS_FILE
    sed -i '' 's/"WebRequestPassword": ""/"WebRequestPassword": "'$CONF_WEBREQUESTPASSWORD'"/' $APPSETTINGS_FILE

    sed -i '' 's/"AppCenterSecretForms": ""/"AppCenterSecretForms": "'$CONF_APPCENTERSECRETFORMS'"/' $APPSETTINGS_FILE
    sed -i '' 's/"AppCenterSecretIOS": ""/"AppCenterSecretIOS": "'$CONF_APPCENTERSECRETIOS'"/' $APPSETTINGS_FILE

    sed -i '' 's+"InitialStatusRequestUrl": ""+"InitialStatusRequestUrl": "'$CONF_INITIALSTATUSREQUESTURL'"+' $APPSETTINGS_FILE

    sed -i '' 's+"LegalImprintUrl": ""+"LegalImprintUrl": "'$CONF_LEGALIMPRINTURL'"+' $APPSETTINGS_FILE
    sed -i '' 's+"LegalPrivacyPolicyUrl": ""+"LegalPrivacyPolicyUrl": "'$CONF_LEGALPRIVACYPOLICYURL'"+' $APPSETTINGS_FILE

fi
# ./Replacing appsettings.json


# Replacing Info.plist
if [ -e "$PLIST_FILE" ]
then
    echo "Updating configuration in Info.plist"
    sed -i '' 's/$(CFBundleIdentifier)/'$PLIST_CFBUNDLEIDENTIFIER'/' $PLIST_FILE

    if [ -e "$APPVERSION_FILE" ]
    then
        read -r appVersionNumber<$APPVERSION_FILE
        echo "$appVersionNumber"

        sed -i '' 's/$(AppVersionNumber)/'$appVersionNumber'/' $PLIST_FILE
    fi

fi
# ./Replacing Info.plist