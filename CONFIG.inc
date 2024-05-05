SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
source ${SCRIPT_DIR}/../CONFIG.inc

PACKAGE=TweakScale
TARGETDIR="$PACKAGE"
TARGETBINDIR="$TARGETDIR/Plugins"
PROJECTSDIR="$PACKAGE/"
VERSIONFILE="TweakScale.version"

DLLS="Scale TweakScale.WatchDog"
# PluginData DLLs
PD_DLLS="Scale.PartDB.13x Scale.PartDB.14x Scale.PartDB.15x Scale.PartDB.18x Scale_Sanitizer Scale_Redist"
# Rule oriented deployoment for PluginData
declare -A PD_SUB_RULES=( )
PD_SUB_DLLS=`{ for K in "${!PD_SUB_RULES[@]}"; do echo $(basename ${PD_SUB_RULES[$K]}); done } | sort -u | tr '\n' ' '`
PD_SUB_DIRS=`{ for K in "${!PD_SUB_RULES[@]}"; do echo $(dirname ${PD_SUB_RULES[$K]}); done } | sort -u | tr '\n' ' '`

# GameData deployed DLLs
GD_DLLS="Scale_Redist"
GD_PRIORITY="999"

# Dev LIB deployed DLLs
LIB_DLLS="Scale"

# External Libs from the LIB directory
EXT_DLLS="KSPe.Light.TweakScale"


PROJECT_BRANCH=`git branch | grep \* | cut -d ' ' -f2`
if [ "$PROJECT_BRANCH" == "master" ] ; then
    # Release mode
    PROJECT_STATE="-BETA"
else
    # Note: Add a leading dash when this value is present!
    PROJECT_STATE="-BETA"
fi

VERSION=$( cat $VERSIONFILE | tr '\n' ' ' | sed -n -E 's/^.*?"VERSION\":\{.*"MAJOR":([0-9]+?),.*?"MINOR":([0-9]+?),.*?"PATCH":([0-9]+?),.*?"BUILD":([0-9]+?).*?\}.*$/\1.\2.\3.\4/p' )
