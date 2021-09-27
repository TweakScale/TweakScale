#!/usr/bin/env bash

source ./CONFIG.inc

clean() {
	rm $FILE
	if [ ! -d Archive ] ; then
		rm -f Archive
		mkdir Archive
	fi
}

echo "Not available yet!"
exit 1

pwd=$(pwd)
FILE=${pwd}/Archive/$PACKAGE-$VERSION${PROJECT_STATE}-CurseForge.zip
echo $FILE
clean
cd GameData

zip -r $FILE ./TweakScale/* -x ".*"
zip -r $FILE ./ModuleManagerWatchDog/* -x ".*"
zip -r $FILE ./666_ModuleManagerWatchDog.dll
zip -r $FILE ./999_Scale_Redist.dll
zip -d $FILE __MACOSX "**/.DS_Store"
cd $pwd
