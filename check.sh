#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc

echo "LIB = ${LIB}"
echo "PACKAGE = $PACKAGE"
echo "TARGETDIR = $TARGETDIR"
echo "TARGETBINDIR = $TARGETBINDIR"
echo "PROJECTSDIR = $PROJECTSDIR"
echo "DLLS = $DLLS"
echo "PD_DLLS = $PD_DLLS"
echo "PD_SUB_DLLS = $PD_SUB_DLLS"
echo "PD_SUB_DIRS = $PD_SUB_DIRS"
echo "GD_DLLS = $GD_DLLS"
echo "GD_PRIORITY = $GD_PRIORITY"
echo "LIB_DLLS = $LIB_DLLS"
echo "PROJECT_BRANCH = $PROJECT_BRANCH"
echo "PROJECT_STATE = $PROJECT_STATE"
echo "VERSION = $VERSION"
echo "KSP_DEV = $KSP_DEV"
