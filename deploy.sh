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
	local DLL=$1.dll

	if [ -f "./bin/Release/$DLL" ] ; then
		cp "./bin/Release/$DLL" "$LIB"
	fi
}

deploy() {
	local DLL=$1.dll

	if [ -f "./bin/Release/$DLL" ] ; then
		cp "./bin/Release/$DLL" "./GameData/$TARGETBINDIR/"
		if [ -d "${KSP_DEV}/GameData/$TARGETBINDIR/" ] ; then
			cp "./bin/Release/$DLL" "${KSP_DEV}GameData/$TARGETBINDIR/"
		fi
	fi
	if [ -f "./bin/Debug/$DLL" ] ; then
		if [ -d "${KSP_DEV}/GameData/$TARGETBINDIR/" ] ; then
			cp "./bin/Debug/$DLL" "${KSP_DEV}GameData/$TARGETBINDIR/"
		fi
	fi
}

deploy_gamedata() {
	local PLACE=$1
	local DLL=$2.dll

	if [ -f "./bin/Release/$DLL" ] ; then
		cp "./bin/Release/$DLL" "./GameData/000_$DLL"
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Release/$DLL" "${KSP_DEV}GameData/${PLACE}_$DLL"
		fi
	fi
	if [ -f "./bin/Debug/$DLL" ] ; then
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Debug/$DLL" "${KSP_DEV}GameData/${PLACE}_$DLL"
		fi
	fi
}

check
cp $VERSIONFILE "./GameData/$TARGETDIR"
cp CHANGE_LOG.md "./GameData/$TARGETDIR"
cp README.md  "./GameData/$TARGETDIR"
cp LICENSE* "./GameData/$TARGETDIR"
cp NOTICE "./GameData/$TARGETDIR"

for dll in $GD_DLLS ; do
    deploy_dev $dll
    deploy_gamedata $GD_PRIORITY $dll
done

for dll in $DLLS ; do
    deploy_dev $dll
    deploy $dll
done
