# TweakScale :: Changes

* 2021-0925: 2.4.5.9 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Adds (missing) patches for 3 parts that I left behind:
		+ LV-T30 Reliant V2
		+ LV-T45 Swivel V2
		+ Ground Anchor (in Experimental yet)
	+ Ongoing savegames still using the 'V1' parts didn't noticed the bork, because these parts still exists on the game (they are only hidden) and are being scaled normally. Only new crafts and savegames were being hindered by the absence of these two patches.
* 2021-0913: 2.4.5.8 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Fixes (again) the Decluttering thingy. 
	+ Closes Issues:
		- [#201](https://github.com/net-lisias-ksp/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
* 2021-0912: 2.4.5.7 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Better compatibility with 3rd Party Add'Ons when Decluttering. 
	+ Closes Issues:
		- [#201](https://github.com/net-lisias-ksp/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
* 2021-0907: 2.4.5.4 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Declutters craft files, preventing TweakScale MODULE nodes from being written on it when the part is not scaled or it's deactivated.
		- Now your unscaled crafts can be shared on KerbalX *et all* without being tagged as using TweakScale.
		- And you can play Challenges where TweakScale is not allowed without deinstalling TweakScale - or creating a new installment just because of it.
	+ Updates [ModuleManagerWatchDog](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/) to 1.0.1.0.
		- Copes with KSP 1.12.x DLL loading new behaviours
		- Add sanity checks for `999_Scale_Redist.dll`
		- Add sanity checks for `Interstallar_Redist.dll`
	+ Closes Issues:
		- [#85](https://github.com/net-lisias-ksp/TweakScale/issues/85) Clean TweakScale's Module from unchanged parts on save 
* 2021-0819: 2.4.5.2 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Raise the bar to KSP 1.12.2
	+ (Finally) adds support for Parts and Modules from KSP 1.11 and newer.
	+ Implements the `TweakScaleExperimental` Patches Program.
		+ A lot of patches are not fully tested, and some Exponents will probably need revising.
		+ Since both these patches itself, as well the unavoidable revisions that will follow may unbalance current crafts in savegames, it's advised discretion on activating the Experimental features.
	+ Closes Issues:
		- [#186](https://github.com/net-lisias-ksp/TweakScale/issues/186) Check and implement all Modules left behind from 1.4.0 up to 1.10.1
		- [#184](https://github.com/net-lisias-ksp/TweakScale/issues/184) Scale some unsupported parts on EXPERIMENTAL status
		- [#182](https://github.com/net-lisias-ksp/TweakScale/issues/182) Get rid of TODOs related to updating scale types.
		- [#181](https://github.com/net-lisias-ksp/TweakScale/issues/181) Support the new Parts introduced on KSP 1.12 and Update Scale Exponents to the new Modules
		- [#128](https://github.com/net-lisias-ksp/TweakScale/issues/128) Support the new Parts introduced on KSP 1.12 and Update Scale Exponents to the new Modules
		- [#120](https://github.com/net-lisias-ksp/TweakScale/issues/120) Support the new Parts introduced on KSP 1.10
		- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/150) Support the new Parts introduced on KSP 1.11 and Update Scale Exponents to the new Modules
* 2021-0627: 2.4.5.1 (Lisias) for 1.4.4 <= KSP <= 1.12.0
	+ Allows running on KSP 1.12.0 without an warning
	+ Warns the user to install KSP Recall on KSP 1.12.x too.
* 2021-0410: 2.4.5.0 (Lisias) for 1.4.4 <= KSP <= 1.11.2
	+ Adds `active` and `available` properties on TweakScale, allowing patches and 3rd parties' plugins to selectively disable TweakScale on some parts without the need to remove it by brute force and rebooting.
		- Aimed to make easier to define constraints on Challenges and Custom Installations. 
	+ Fixing some last standing mistakes on some Extras content
	+ Updating KSPe Light for TweakScale
