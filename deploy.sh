#!/usr/bin/env bash

source ./CONFIG.inc

check() {
	if [ ! -d "./GameData/$TARGETBINDIR/" ] ; then
		rm -f "./GameData/$TARGETBINDIR/"
		mkdir -p "./GameData/$TARGETBINDIR/"
	fi

	if [ ! -f "./GameData/$TARGETBINDIR/KSPe.Light.TweakScale.dll" ] ; then
		if [ ! -f "${LIB}/KSPe.Light.TweakScale.dll" ] ; then
				echo "KSPe.Light.TweakScale not found!!! Aborting."
				read line
				exit -1
		fi
		cp "${LIB}/KSPe.Light.TweakScale.dll" "./GameData/$TARGETBINDIR/"
	fi
}

deploy_dev() {
	local DLL=$1

	if [ -f "./bin/Release/$DLL.dll" ] ; then
		cp "./bin/Release/$DLL.dll" "$LIB"/$DLL.BETA.dll
	fi
}

deploy_bin() {
	local DLL=$1

	if [ -f "./bin/Release/$DLL.dll" ] ; then
		cp "./bin/Release/$DLL.dll" "./GameData/$TARGETBINDIR/"
		if [ -d "${KSP_DEV}/GameData/$TARGETBINDIR/" ] ; then
			cp "./bin/Release/$DLL.dll" "${KSP_DEV}/GameData/$TARGETBINDIR/"
		fi
	fi
	if [ -f "./bin/Debug/$DLL.dll" ] ; then
		if [ -d "${KSP_DEV}/GameData/$TARGETBINDIR/" ] ; then
			cp "./bin/Debug/$DLL.dll" "${KSP_DEV}/GameData/$TARGETBINDIR/"
		fi
	fi
}

deploy_md() {
	local MD=$1
	#![NxMyyTK.png](./PR_material/img/NxMyyTK.png)
	sed $MD -e "s/!\\[.\+\\]\\(.\+\\)//g" > "./GameData/$TARGETDIR"/$MD
}

deploy_gamedata() {
	local PLACE=$1
	local DLL=$2.dll

	if [ -f "./bin/Release/$DLL" ] ; then
		cp "./bin/Release/$DLL" "./GameData/${PLACE}_$DLL"
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Release/$DLL" "${KSP_DEV}/GameData/${PLACE}_$DLL"
		fi
	fi
	if [ -f "./bin/Debug/$DLL" ] ; then
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Debug/$DLL" "${KSP_DEV}/GameData/${PLACE}_$DLL"
		fi
	fi
}
VERSIONFILE=$PACKAGE.version

check
cp $VERSIONFILE "./GameData/$TARGETDIR"
cp CHANGE_LOG.md "./GameData/$TARGETDIR"
cp LICENSE* "./GameData/$TARGETDIR"
cp NOTICE "./GameData/$TARGETDIR"
deploy_md README.md

for dll in Scale KSPe.Light.TweakScale ; do
    deploy_dev $dll
    deploy_bin $dll
done

deploy_dev Scale_Redist
deploy_gamedata 999 Scale_Redist

# HACK! See how to do it properly later.
#TARGETBINDIR="TweakableEverything/Plugins_TweakScale" check
#TARGETBINDIR="TweakableEverything/Plugins_TweakScale" deploy Scale_TweakableEverything
# Note: Avoiding redistributing the TweakableEverything until further notice from its Maintainers
