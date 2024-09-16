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

zip -r $FILE ./TweakScale/* -x ".*"
zip -r $FILE ./ModuleManagerWatchDog/* -x ".*"
zip -r $FILE ./666_ModuleManagerWatchDog.dll
zip -r $FILE ./999_Scale_Redist.dll

mkdir -p $pwd/bin/TweakScale
cat $TARGETDIR/TweakScale.cfg | sed 's/curseforge_ready = False/curseforge_ready = True/g' > $pwd/bin/TweakScale/TweakScale.cfg
pushd $pwd/bin
zip -u $FILE TweakScale/TweakScale.cfg
popd

zip -d $FILE __MACOSX "**/.DS_Store" || echo ""

set -e
cd $pwd
