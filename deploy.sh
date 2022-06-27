#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc
IFS=$' '

check() {
	if [ ! -d "./GameData/$TARGETBINDIR/" ] ; then
		rm -f "./GameData/$TARGETBINDIR/"
		mkdir -p "./GameData/$TARGETBINDIR/"
	fi

	for dll in $EXT_DLLS ; do
		if [ ! -f "${LIB}/$dll.dll" ] ; then
			echo "$dll not found!!! Aborting."
			exit -1
		fi
	done
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
		cp -R "./bin/Release/$DLL" "./GameData/$TARGETBINDIR/"
		if [ -d "${KSP_DEV}/GameData/$TARGETBINDIR/" ] ; then
			cp -R "./bin/Release/$DLL" "${KSP_DEV}/GameData/$TARGETBINDIR/"
		fi
	fi
	if [ -f "./bin/Debug/$DLL" ] ; then
		if [ -d "${KSP_DEV}/GameData/$TARGETBINDIR/" ] ; then
			cp -R "./bin/Debug/$DLL" "${KSP_DEV}GameData/$TARGETBINDIR/"
		fi
	fi
}

deploy_plugindata() {
	local DLL=$1.dll

	if [ -f "./bin/Release/$DLL" ] ; then
		cp "./bin/Release/$DLL" "./GameData/$TARGETBINDIR/PluginData/"
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Release/$DLL" "${KSP_DEV}GameData/$TARGETBINDIR/PluginData/"
		fi
	fi
	if [ -f "./bin/Debug/$DLL" ] ; then
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Debug/$DLL" "${KSP_DEV}GameData/$TARGETBINDIR/PluginData/"
		fi
	fi
}

deploy_gamedata() {
	local PLACE=$1
	local DLL=$2.dll

	if [ -f "./bin/Release/$DLL" ] ; then
		cp "./bin/Release/$DLL" "./GameData/${PLACE}_$DLL"
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Release/$DLL" "${KSP_DEV/}GameData/${PLACE}_$DLL"
		fi
	fi
	if [ -f "./bin/Debug/$DLL" ] ; then
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp "./bin/Debug/$DLL" "${KSP_DEV}GameData/${PLACE}_$DLL"
		fi
	fi
}

deploy_ext() {
	local DLL=$1.dll

	if [ -f "$LIB/$DLL" ] ; then
		cp -R "$LIB/$DLL" "./GameData/$TARGETBINDIR/"
		if [ -d "${KSP_DEV}/GameData/" ] ; then
			cp -R "$LIB/$DLL" "${KSP_DEV/}GameData/$TARGETBINDIR/"
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

for dll in $PD_DLLS ; do
    deploy_plugindata $dll
done

for dll in $DLLS ; do
    deploy_dev $dll
    deploy $dll
done

for dll in $EXT_DLLS ; do
    deploy_ext $dll
done

echo "${VERSION} Deployed into ${KSP_DEV}"
