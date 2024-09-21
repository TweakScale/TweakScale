#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'

pwd=$(pwd)
./pack-full.sh
cd $pwd
./pack-curse.sh
cd $pwd
./pack-spacedock.sh
cd $pwd
