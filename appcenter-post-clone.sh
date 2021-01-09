#!/usr/bin/env bash


# Variables
PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/StatusChecker.iOS/Info.plist
APPVERSION_FILE=$APPCENTER_SOURCE_DIRECTORY/appversion.txt
# ./Variables

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