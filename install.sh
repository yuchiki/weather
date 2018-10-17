#!/bin/sh

###################utility functions ##########################################

yes_or_no() {
    while true; do
        echo $1
        read answer
        case $answer in
            "Y" | "y" | "yes" | "Yes" | "YES" ) return 0 ;;
            "N" | "n" | "no" | "No" | "NO" ) return 1 ;;
        esac
    done
}

confirm_if_overwrite() {
    if [ ! -e $1 ] ; then
        return 0
    fi

    yes_or_no "$1 already exists. Would you like to replace the existing file with the new one?[Y/n]"
    if [ $? -gt 0 ] ; then
        echo "Installation aborted."
        exit 1
    fi
}

################### main process ##############################################

# confirm that environemt variables are set
: ${DOTNET_DIRECTORY:?}
: ${DOTNET_ALIASES:?}

# set constants
APP=weather
FRAMEWORK=netcoreapp2.0
SOURCE_DIRECTORY=./bin/Release/$FRAMEWORK/publish
TARGET_DIRECTORY=$DOTNET_DIRECTORY/$APP

# this function creates alias only if it was not created.
create_alias() {
    cat $DOTNET_ALIASES | grep "alias $APP" 1>/dev/null
    if [ $? -ne 0 ] ; then
        echo "alias $APP='dotnet $TARGET_DIRECTORY/$APP.dll'" >> $DOTNET_ALIASES
    fi
}

main() {
    confirm_if_overwrite $TARGET_DIRECTORY
    dotnet publish -f $FRAMEWORK -c Release
    cp -r $SOURCE_DIRECTORY $TARGET_DIRECTORY
    create_alias
    echo "$APP is installed."
}

main
