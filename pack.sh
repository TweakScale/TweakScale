#!/usr/bin/env bash

pwd=$(pwd)
./pack-full.sh
cd $pwd
./pack-curse.sh
cd $pwd
