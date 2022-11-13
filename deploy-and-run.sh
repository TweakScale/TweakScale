#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'

. ./CONFIG.inc
. ./deploy.sh

echo "Running ${KSP_DEV}"
open ${KSP_DEV}/KSP.app

