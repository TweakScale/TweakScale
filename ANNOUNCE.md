## ANNOUNCE

Release 2.4.3.5 is available for downloading, with the following changes:

+ Updated KSPe Light for TweakScale:
	+ Standard Installation Check
	+ Common Dialogs
		- More consistent appearance between different installations 
	+ Internal routines updated to understand Unity 2019. 
		- KSP 1.8 Ready, baby! ;)
* Issues Fixed:
	+ [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
	+ [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
	+ [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!

See [OP](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*) for the links.

## Highlights

### New Runtime Check

TweakScale knows complains with a "Houston" (Fatal Error Message) when it detectes it was wrongly installed.

### Deprecated Patches

Then following patches were deprecated as the respectives Add'Ons are not available for downloading anymore, rendering them useless and even dangerous to be used by the ones that have them on their archives:

* [KOSMOS](https://forum.kerbalspaceprogram.com/index.php?/topic/6679-*)
* [UDK's Large Structural Components](https://forum.kerbalspaceprogram.com/index.php?/topic/31891-*)

These ones were deprecated due the Add'On's maintainers decided to internalize them and maintain the patches themselves.

* [HGR](https://forum.kerbalspaceprogram.com/index.php?/topic/131556-*)
* [KAX](https://forum.kerbalspaceprogram.com/index.php?/topic/180268-*)
* [Mining Extensions](https://forum.kerbalspaceprogram.com/index.php?/topic/130325-*)
* [Mark 3 Expansion](https://forum.kerbalspaceprogram.com/index.php?/topic/109401-*)

Deprecated patches are preserved on the `Extras/Deprecated` directory on the distribution file. In the unlikely event you need them back, it's recommended to copy them into a customized place on your installment and not on the TweakScale's directory hierarchy. TweakScale suggests `GameData/__LOCAL/TweakScale` .

### Overrules

A overrule, as the name says, is a patch the overrules TweakScale (and anything else) in order to make things "broken" in a deterministic way.

A complete essay can be found [here](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-14-tweakscale-under-lisias-management-2434-2019-0903/&do=findComment&comment=3663098).

### Hot Fixes

A Hot Fix is a hand crafted patch that fixes by brute force patching problems, forcing the original intended result for a given KSP installment. The difference from an overrule is that Hot Fixes don't break compatibility with sane installments, so you can start new savegames and share your crafts without worries.

A complete essay can be found [here](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-14-tweakscale-under-lisias-management-2434-2019-0903/&do=findComment&comment=3663098).

### New Scaling Behaviour

A new TWEAKSCALEBEHAVIOUR, ModuleGeneratorExtended , is available for parts using ModuleGenerator that wants to scale the INPUT_RESOURCES too. This feature wasn't introduced directly into the ModuleGenerator's TWEAKSCALEEXPONENTS to prevent damage on Add'Ons (and savegames) that rely on the current behaviour (scaling only the output), as suddenly the resource consumption would increase on already stablished bases and crafts.

Just add the lines as the example below (the output resources scaling is still inherited from the default patch!).

```
@PART[my_resource_converter]:NEEDS[TweakScale]
{
    #@TWEAKSCALEBEHAVIOR[ModuleGeneratorExtended]/MODULE[TweakScale] { }
    %MODULE[TweakScale]
    {
        type = free
    }
}
```

## WARNINGS

The known *Unholy interaction between modules* (Kraken Food), rogue patches or known incompatibilities between third parties Add'On that can lead to disasters are being detected on the Sanity Checks with a proper (scaring) warning being shown. A full essay about these issues can be found [here](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-14-tweakscale-under-lisias-management-2434-2019-0903/).

Unfortunately, such issues are a serious Show Stopper, potentially (and silently) ruining your savegames. This is not TweakScale fault, but yet it's up to it to detect the problem and warn you about it. If this happens with you, call for help. A "Cancel" button is available for the brave Kerbonauts willing to fly unsafe.

TweakScale strongly recommends using [S.A.V.E.](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-*).

Special procedures for recovering mangled installments once the TweakScale are installed (triggering the MM cache rebuilding) are possible, but **keep your savegames backed up**. And **DON`T SAVE** your crafts once you detect the problem. Reach me on [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*) for help.

TweakScale stills "mangles further" affected crafts and savegames with some badly (but recoverable) patched parts so when things are fixed, your crafts preserve the TweakScale settings without harm. **THIS DOES NOT FIX THE PROBLEM**,  as this is beyond the reach of TweakScale - but it at least prevents you from losing your crafts and savegames once the problem happens and then is later fixed. You will detect this by KSP complaining about a missing `TweakScaleRogueDuplicate` module (previously `TweakScaleDisabled`, renamed for clarity). You can safely ignore this.

As usual, this version still drops support in runtime for some problematic parts. Any savegame with such problematic parts scaled will have them "descaled". This is not a really big problem as your game was going to crash sooner or later anyway - but if you plan to return to such savegame later when TweakScale will fully support that parts again, it's better to backup your savegames!

Keep an eye on the [Known Issues](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) file.

— — — — —

This Release will be published using the following Schedule:

* GitHub, reaching first manual installers and users of KSP-AVC. Right now.
* CurseForge, by Friday night
* SpaceDock (and CKAN users), by Saturday night.

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
