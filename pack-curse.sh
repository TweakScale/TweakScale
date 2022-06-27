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
FILE=${pwd}/Archive/$PACKAGE-$VERSION${PROJECT_STATE}-CurseForge.zip
echo $FILE
clean
cd GameData

zip -r $FILE ./$PACKAGE/* -x ".*"
zip -r $FILE ./ModuleManagerWatchDog/* -x ".*"
zip -r $FILE ./666_ModuleManagerWatchDog.dll
zip -r $FILE ./999_Scale_Redist.dll
zip -d $FILE "__MACOSX/*" "**/.DS_Store"
cd $pwd
