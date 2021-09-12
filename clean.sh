#!/usr/bin/env bash

source ./CONFIG.inc

clean() {
	local DLL=$1.dll

	find ./bin -name "$DLL" -delete
	find ./obj -name "$DLL" -delete
	rm -f "./GameData/$DLL"
	rm -R -f "./GameData/$TARGETBINDIR"
	rm -f "$LIB/$DLL"
	rm -f "${KSP_DEV}/GameData/$DLL"
	rm -f "${KSP_DEV}/GameData/$TARGETBINDIR/$DLL"
}

VERSIONFILE=$PACKAGE.version

rm -fR "./bin"
rm -fR "./obj"
rm -f "./GameData/$TARGETDIR/*.version"
rm -f "./GameData/$TARGETDIR/CHANGE_LOG.md"
rm -f "./GameData/$TARGETDIR/README.md"
rm -f "./GameData/$TARGETDIR/*.LICENSE"
for dll in Scale 999_Scale_Redist Scale_TweakableEverything ; do
    clean $dll
done
