source ../CONFIG.inc

PACKAGE=TweakScale
TARGETDIR="$PACKAGE"
TARGETBINDIR="$TARGETDIR/Plugins"
PROJECTSDIR="$PACKAGE/"
VERSIONFILE="TweakScale.version"
DLLS="Scale KSPe.Light.TweakScale"
PD_DLLS=""
GD_DLLS="KSPe"
GD_PRIORITY="999"

PROJECT_BRANCH=`git branch | grep \* | cut -d ' ' -f2`
if [ "$PROJECT_BRANCH" == "master" ] ; then
    # Release mode
    PROJECT_STATE=""
else
    # Note: Add a leading dash when this value is present!
    PROJECT_STATE=""
fi

VERSION=$( cat $PACKAGE.version | tr '\n' ' ' | sed -n -E 's/^.*?"VERSION\":\{.*"MAJOR":([0-9]+?),.*?"MINOR":([0-9]+?),.*?"PATCH":([0-9]+?),.*?"BUILD":([0-9]+?).*?\}.*$/\1.\2.\3.\4/p' )
