## ANNOUNCE

Release 2.4.4.0 is available for downloading, with the following changes:

+ Updated KSPe Light for TweakScale:
	+ Suport for KSP 1.11
* Chain Scaling now "jumps over" parts without TweakScale support, instead of just breaking the chain.
* Parts with Variants that change Mass and/or Cost are now supported. #HURRAY
* Formal Public Interface for scaling helpers (used mainly by the Companions)
* Added `ignoreResourcesForCost` attribute to allow custom modules to deactivate TweakScale calculations for resources
* Installation checks, detecting common installation errors.
* **Complete** overhaul of the patches for Stock (and DLC) parts
	+ Only fixes that don't break current savegames were applied.
	+ TweakScale 2.5 will have further fixes merged, even when unbalancing existing crafts.
* Issues Fixed:
	+ Formally closes 49 issues, backporting (almost) all fixes from the Beta Releases until 2.5.0.27. Please see the [Change Log](https://github.com/net-lisias-ksp/TweakScale/blob/master/CHANGE_LOG.md), as they are too much to be enumerated comfortably here!

All of these totalling **353** commits to be merged since the previous 2.4.3.21 release!!
	
See [OP](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*) for the links.

## Highlights

### Formal support for KSP from 1.4.4 to 1.11

KSP 1.11 didn't introduced any changes that break TweakScale, so it's formally supported. However, in order to proper support Variants, the minimal KSP version supported by now is 1.4.4 . Sorry for that.

TweakScale 2.5 aims to bring back support for KSP down to 1.2.2 (exactly how, is still work in progress, but it's feasible as being demonstrated by some Unofficial forks of mine).

### Parts with Variants

Variants that change Cost and/or Nass are now fully supported, but TweakScale is struggling to support Parts with Variants that changes Attachment Nodes.

I had planned to withdraw TweakScale support from such parts as I did in the past, but then I realised that most Version 2 parts from already scalable parts (as Terrier...) would had TweakScale withdrawn, and this would render some crafts and savegames corrupted.

Since most of these parts didn't misbehave on a noticeable way, I decided to just let it go as is for while. Just a few stock parts are misbehaving into a noticeable way, and these parts are (until the moment):

- The Mastodon engine
- The Structural Tubes
	- T-12
	- T-18
	- T-25
	- T-37
	- T-50

And probably more, as Add'Ons starts to use such feature. 

The only workaround at the moment is to descale these parts before applying the variant and then scaling them again. Alternatively, just detach and reattach the misbehaving parts.

A proper fix is Work in Progress, and is expected to be released on 2.4.4.1.

### Deprecating Patches

Support for all non Squad parts are on a deprecating status and will be definitively removed on TweakScale 2.5. The [TweakScale Companion Program](https://forum.kerbalspaceprogram.com/index.php?/topic/192216-tweakscale-companion-program-2020-1001/) will be the source for supporting third-parties add'ons.

It's advised to install the needed Companions as they reach Gold status.

### Sanity Checks

Parts using Firespitter's `FSbuoyancy` needs the latest [TweakScale Companion for Firespitter](https://forum.kerbalspaceprogram.com/index.php?/topic/192216-tweakscale-companion-program-2020-1001/), as only the Companion has the specific code that knows how to handle it.

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

TweakScale stills "mangles further" affected crafts and savegames with some badly (but recoverable) patched parts so when things are fixed, your crafts preserve the TweakScale settings without harm. **THIS DOES NOT FIX THE PROBLEM**, as this is beyond the reach of TweakScale - but it at least prevents you from losing your crafts and savegames once the problem happens and then it is later fixed. You will detect this by KSP complaining about a missing `TweakScaleRogueDuplicate` module (previously `TweakScaleDisabled`, renamed for clarity). You can safely ignore this.

Keep an eye on the [Known Issues](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) file.

— — — — —

This Release will be published using the following Schedule:

* GitHub, reaching first manual installers and users of KSP-AVC. Right now.
* CurseForge, by Sunday night
* SpaceDock (and CKAN users), by Monday night.

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
