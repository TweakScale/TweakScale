## ANNOUNCE

Release 2.4.5.4 is available for downloading, with the following changes:

+ Declutters craft files, preventing TweakScale MODULE nodes from being written on it when the part is not scaled or it's deactivated.
	- Now your unscaled crafts can be shared on KerbalX *et all* without being tagged as using TweakScale.
	- And you can play Challenges where TweakScale is not allowed without deinstalling TweakScale - or creating a new installment just because of it.
+ Updates [ModuleManagerWatchDog](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/) to 1.0.1.0.
	- Copes with KSP 1.12.x DLL loading new behaviours
	- Add sanity checks for `999_Scale_Redist.dll`
	- Add sanity checks for `Interstallar_Redist.dll`
+ Closes Issues:
	- [#85](https://github.com/net-lisias-ksp/TweakScale/issues/85) Clean TweakScale's Module from unchanged parts on save 

See [OP](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*) for the links.


## Highlights

### Selective persistence on saving craft files

From 2.4.5.4, a long time wished feature (the [task](https://github.com/net-lisias-ksp/TweakScale/issues/85) was created on late 2019!!) is finally reaching the mainstream after being tested inhouse for half the year: TweakScale now omits its data on parts with default settings when saving craft files.

This new (and unexpected) feature is meant to declutter your craft files from unnecessary TweakScale sections, preventing your crafts from being flagged as using TweakScale when all your parts are plain vanilla sized!

This will save you from creating special KSP installments when participating of challenges where TweakScale is not allowed. Now you just can save the file normally and publish it without that pesky TweakScale config sections lingering on the file without doing anything useful. ;)

This feature works on **every KSP** that have that *Upgrade Pipeline thingy* that will recreate the default TweakScale data from the prefab when it doesn't find it on loading. TweakScale **does not** activate this feature for KSP versions without this *thingy* for obvious reasons. Even users of older TweakScale versions will be able to load the craft files (the heavy lifting is done by the Pipeline) - so this feature is 100% retro-compatible.

There's a [proof of concept on Kerbal-X](https://kerbalx.com/Lisias/TweakScale-2454-Save-Test) where a craft with all parts unescaled (but the `miniFulelage`) is available for downloading. By inspecting the craft, you will see that only `miniFuselage` has a TweakScale section - but, yet, the craft will load fine on your KSP 1.12.2. There's another version made on KSP 1.4.5 that you can use to test the feature on previous KSP versions (make the Pipeline sweat!). Click on the "Version 1 of 2" thingy on the upper left part of the page.


### `TweakScaleExperimental` support for parts and modules

From 2.4.5.2, TweakScale is starting to support, as possible, all KSP modules - and not only the most visible ones, as well parts.

In order to pursue that goal without risking your ongoing savegames (as changing Exponents **will** unbalance your designs, potentially ruining your crafts), some parts and modules scaling are only available on **Experimental** mode.

Such mode will patch almost all KSP parts and modules (but **Serenity/Breaking Ground** as this one will be tackled down later, see the [backlog](https://github.com/net-lisias-ksp/TweakScale/milestone/23) for more information), including some that I'm unsure should be scaled - not to mention Exponents that I'm pretty sure will need some rebalancing.

In order to toy with these parts and modules, you need to create a directory called `TweakScaleExperimental` in your `GameData`. The directory may be empty, it's enough to have it on `GameData` so Module Manager will register it, satisfying the `:NEEDS` that enable such patches and Exponents.

Please only enable these patches on disposable or non valuable KSP installments. These patches are going to change for sure in the near future, and these changes will be incompatible with savagames created with the previous set of Experimental patches.


### Standard mechanism to control TweakScale availability

From 2.4.5.0 and up, it's possible to deactivate TweakScale on some (or even all) parts by patching or on the user interface without affecting living crafts on the savegame (or even already existent craft files).

A patching only feature can lock up TweakScale on the current state, making easier to create artefacts to automatically reconfigure a savegame for Challenges with specific rules. Again, without affecting existent crafts or savegames - once the craft is launched, it's not affected by these options.

See the [Documentation](https://github.com/net-lisias-ksp/TweakScale/tree/master/GameData/TweakScale/Docs) for details.


### Formal support for KSP from 1.4.4 to 1.12.2

KSP 1.12 didn't introduced any changes that break TweakScale, so it's formally supported. However, in order to proper support Variants, the minimal KSP version supported by now is 1.4.4 . Sorry for that.

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

Keep an eye on the [Known Issues](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) file.

— — — — —

This Release will be published using the following Schedule:

* GitHub, reaching first manual installers and users of KSP-AVC. Right now.
* CurseForge, by Thursday night (I hope)
* SpaceDock (and CKAN users), by Saturday night (with luck)

The reasoning is to gradually distribute the Release to easily monitor the deployment and cope with eventual mishaps.
