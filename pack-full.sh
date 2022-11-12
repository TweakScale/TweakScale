#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc

clean() {
	rm -fR $FILE
	if [ ! -d Archive ] ; then
		mkdir Archive
	fi
}

pwd=$(pwd)
FILE=${pwd}/Archive/$PACKAGE-$VERSION${PROJECT_STATE}.zip
echo $FILE
clean
zip -r $FILE ./GameData/* -x ".*"
set +e
zip -r $FILE ./PluginData/* -x ".*"
zip -r $FILE ./Extras/* -x ".*"
zip $FILE INSTALL.md
zip -d $FILE "__MACOSX/*" "**/.DS_Store"
set -e
cd $pwd
