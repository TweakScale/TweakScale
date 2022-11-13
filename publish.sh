#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc

VERSIONFILE=$PACKAGE.version

deploy_md() {
	local MD=$1
	local TARGET=$2
	#![NxMyyTK.png](./PR_material/img/NxMyyTK.png) -> ![NxMyyTK.png](./PR_material/$PACKAGE/img/NxMyyTK.png)
	sed $MD -e "s/\\.\\/PR_material\\//.\\/PR_material\\/$PACKAGE\\//g" | ssh -i $SSH_ID $SITE "cat - >$TARGET_CMS_PATH/$TARGET"
}

deploy_assets() {
	local IN_DIR=$1
	local OUT_DIR=$2
	ssh -i $SSH_ID $SITE "mkdir ${TARGET_CONTENT_PATH}$OUT_DIR"
	local cur_path=`pwd`
	cd $IN_DIR
	scp -i $SSH_ID -rp `find . -type f \! -name ".DS_Store" ` "$SITE:${TARGET_CONTENT_PATH}$OUT_DIR"
	cd $cur_path
}

scp -i $SSH_ID ./GameData/$PACKAGE/$VERSIONFILE $SITE:/$TARGET_CONTENT_PATH
deploy_assets ./PR_material ./PR_material/$PACKAGE
deploy_md README.md $PACKAGE.md
