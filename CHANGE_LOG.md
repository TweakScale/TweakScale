# TweakScale :: Change Log

* 2022-0708: 2.5.0.47 **BETA** (Lisias) for KSP >= 1.3 
	+ Incepts a Fix/Check Engine to parametrize the Sanity Checks and shove them on the GameData, where they can be patcheable. Will be terribly useful for the TweakScale Companions.
	+ Moves the Sanitizer Contract (and Interface) to Scale_Redist, so 3rd parties can implement checks without hard dependency on TweakScale.
* 2022-0627: 2.5.0.46 **BETA** (Lisias) for KSP >= 1.3
	+ Updates ModuleManagerWatchDog to 1.1.0.1
	+ Better coping with Curse Installer
	+ Fixes a brain fart of mine on the AutoScale feature.
	+ Move the DryCostWriter and SanityChecks out of the Main Menu due Making History
		- The thing is now executed when LOADING is phasing out, before Main Menu phase in.
		- This will prevent the race condition I detected when the rig is heavily loaded, but whatever MH is creating on GameDatabase from now on is unchecked.
	+ Closes Issues:
		- [#256](https://github.com/net-lisias-ksp/TweakScale/issues/256) Move the DryCostWriter (and Sanity Checkes) out of the Main Menu startup
		- [#255](https://github.com/net-lisias-ksp/TweakScale/issues/255) Special Deployment for Curseforge due Scale_Redist
* 2022-0620: 2.5.0.45 **BETA** (Lisias) for KSP >= 1.3
	+ **HUGE** refactoring on the Sanity Checks, "exporting" the checks into a dedicated DLL (`Scale_Sanitizer.dll`) at the same time allowing 3rd parties to include their own checks on it.
	+ Some extra precautions on `EditorHelper`
	+ Some less (useless) precautions on the Variant Support.
	+ Closes Issues:
		- [#254](https://github.com/net-lisias-ksp/TweakScale/issues/254) Extract the Sanity Checks in their own DLL
* 2022-0523: 2.4.6.15 (Lisias) for KSP >= 1.3
	+ Well, it's a bit embarrassing but I finally detected and fixed a regression on legacy support I inadvertently did when I removed the kludges I made on TweakScale when KSP 1.9.0 was launched.
		- Long history made short, when I added that kludge, I broke support for [1.4.4 <= KSP <= 1.7.3] and then added another kludge to counter act the first kludge.
		- Once I removed the 1.9.x kludge and moved it as a proper work around into KSP-Recall, I forgot to remove the second kludge...
		- As a side effect, less Scaling Engines are needed now, so we have one less DLL to worry about.
	+ Additionally, yet another stupidity was detected and fixed on handling Variants (and this one was pretty old...)
	+ Closes Issues:
		- [#249](https://github.com/net-lisias-ksp/TweakScale/issues/249) Reorganize the Scaling Engines
* 2022-0522: 2.4.6.14 (Lisias) for KSP >= 1.3
	+ ***DITCHED*** as a new released was issued in less than 24 hours.
* 2022-0415: 2.5.0.44 (Lisias) for KSP >= 1.3
	+ Well, it's a bit embarrassing but I finally detected and fixed a regression on legacy support I inadvertently did when I removed the kludges I made on TweakScale when KSP 1.9.0 was launched.
		- Long history made short, when I added that kludge, I broke support for [1.4.4 <= KSP <= 1.7.3] and then added another kludge to counter act the first kludge.
		- Once I removed the 1.9.x kludge and moved it as a proper work around into KSP-Recall, I forgot to remove the second kludge...
		- As a side effect, less Scaling Engines are needed now, so we have one less DLL to worry about.
	+ Closes Issues:
		- [#249](https://github.com/net-lisias-ksp/TweakScale/issues/249) Reorganize the Scaling Engines
* 2022-0508: 2.4.6.13 (Lisias) for KSP >= 1.3
	+ Fixes a nasty bug about scaling down crewed parts, reported by [robi_243](https://forum.kerbalspaceprogram.com/index.php?/profile/221308-robi_243/) on [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/208059-tweakscale-problem/#comment-4128151). Thanks, dude!!
	+ Closes Issues:
		- [#247](https://github.com/net-lisias-ksp/TweakScale/issues/247) Scaling down a crewed part makes the crew go M.I.A. and the crew capacity is not restored later!
* 2022-0429: 2.4.6.12 (Lisias) for KSP >= 1.3
	+ Fixes a not so subtle but definitively insidious problem reported by [Alexsys](https://forum.kerbalspaceprogram.com/index.php?/profile/211693-alexsys/). Thanks and sorry, dude!
	+ Closes Issues:
		- [#246](https://github.com/net-lisias-ksp/TweakScale/issues/246) New bug related to IVA and Cameras when TweakScale is installed
		- [#222](https://github.com/net-lisias-ksp/TweakScale/issues/222) Update KSPe.Light for KSPe
* 2022-0415: 2.4.6.11 (Lisias) for KSP >= 1.3
	+ Fixes a subtile and insidious problem [reported by BTAxis](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-130/&do=findComment&comment=4117283). Thanks, dude!
	+ Closes Issues:
		- [#244](https://github.com/net-lisias-ksp/TweakScale/issues/244) Reactivating TweakScale is disabling the scaling feature for good
* 2022-0415: 2.5.0.43 (Lisias) for KSP >= 1.3
	+ Fixes a subtile and insidious problem [reported by BTAxis](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-130/&do=findComment&comment=4117283). Thanks, dude!
	+ Closes Issues:
		- [#244](https://github.com/net-lisias-ksp/TweakScale/issues/244) Reactivating TweakScale is disabling the scaling feature for good
* 2022-0326: 2.5.0.42 **BETA** (Lisias) for KSP >= 1.3
	+ Removes an (now) unnecessary "gambiarra", as KSP-Recall is now fixing the mess on KSP >= 1.9 editor.
		- A small (and 3rd party safe) fraction of it remains to cover what may be a missing use-case on KSP-Recall, or a fishy code on TweakScale itself. 
	+ Implements a new Sanity Check against a worrisome situation where a Part is given to TweakScale **without the partInfo**!!!
		- I don't have the slightest idea about how in hell this can happen, but I got confirmation of this problem from reliable sources.
	+ **Finally** implementing a full-fledged "TweakScale Upgrade Pipeline", allowing run-time, on-the-fly conversions between ScaleTypes and DefaultScales.
		- No more worries about installing or updating Add'Ons that changes the TweakScale patches.
		- [All Tweak!!!](https://forum.kerbalspaceprogram.com/index.php?/topic/182700-111x-all-tweak-07-23rdoctober2019/) users, this one is dedicated to you! :) 
	+ Closes Issues:
		- [#237](https://github.com/net-lisias-ksp/TweakScale/issues/237) New Sanity Check: parts without partInfo!!!
		- [#236](https://github.com/net-lisias-ksp/TweakScale/issues/236) Extent the Scale migration feature to allow switching ScaleTypes and DefaultScales!
		- [#218](https://github.com/net-lisias-ksp/TweakScale/issues/218) Implement GetInfo on TweakScale's Part Module
* 2022-0322: 2.5.0.41 (Lisias) for KSP >= 1.3 **BETA**
	+ ***DITCHED*** because I screwed the pooch on a merge **after testing the thing**, and ended up publishing a crappy release. :(
		- I need some rest from dayjob. :/ 
* 2021-1215: 2.4.6.8 (Lisias) for 1.3.0 <= KSP <= 1.12.3
	+ Raises the bar to 1.12.3
		- Smoke Tests using the most hairy use cases suggests nothing wrong should happens. 
* 2021-1211: 2.4.6.7 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Rollbacks (really, this time) an unfortunate merge from `/dev/orthodox` where the Localization stunt leaked again.
* 2021-1205: 2.4.6.6 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ ***DITCHED*** due a mishap on the distribution files.
* 2021-1130: 2.4.6.5 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Closes Issues:
		- [#219](https://github.com/net-lisias-ksp/TweakScale/issues/219) Apparently, OnCopy parts is misbehaving on Parts with Variants
		- [#86](https://github.com/net-lisias-ksp/TweakScale/issues/86) When a root part is scaled, a part attached to it is displaced on the Y axis!
* 2021-1130: 2.5.0.40 **BETA** (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#219](https://github.com/net-lisias-ksp/TweakScale/issues/219) Apparently, OnCopy parts is misbehaving on Parts with Variants
		- [#86](https://github.com/net-lisias-ksp/TweakScale/issues/86) When a root part is scaled, a part attached to it is displaced on the Y axis!
* 2021-1123: 2.4.6.4 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Turns off by default (and makes hard to activate) the StealthSave due a (another) bug related to the Upgrade Pipeline.
	+ Reverts the `KSPe.Light.TweakScale` to the previous release due a shitstorm apparently related to borked `Kernel32.dll` on some systems
* 2021-1120: 2.4.6.3 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Closes Issues:
		- [#211](https://github.com/net-lisias-ksp/TweakScale/issues/211) Mitigate the HotKeys being hijacked by 3rd-parties
		- [#209](https://github.com/net-lisias-ksp/TweakScale/issues/209) TweakScale not installed on wrong directory
		- [#197](https://github.com/net-lisias-ksp/TweakScale/issues/197) Flags (the parts attachable) are losing the attaching points when its parent is duplicated
		- [#167](https://github.com/net-lisias-ksp/TweakScale/issues/167) Mirror Symmetry is displacing some (all?) parts when scaled
		- [#139](https://github.com/net-lisias-ksp/TweakScale/issues/139) Scaling Part with Variants that change attachment nodes is not working as expecnted
* 2021-1120: 2.5.0.39 **BETA** (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#211](https://github.com/net-lisias-ksp/TweakScale/issues/211) Mitigate the HotKeys being hijacked by 3rd-parties
		- [#209](https://github.com/net-lisias-ksp/TweakScale/issues/209) TweakScale not installed on wrong directory
		- [#197](https://github.com/net-lisias-ksp/TweakScale/issues/197) Flags (the parts attachable) are losing the attaching points when its parent is duplicated
		- [#167](https://github.com/net-lisias-ksp/TweakScale/issues/167) Mirror Symmetry is displacing some (all?) parts when scaled
		- [#139](https://github.com/net-lisias-ksp/TweakScale/issues/139) Scaling Part with Variants that change attachment nodes is not working as expecnted
* 2021-1026: 2.4.6.2 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Fixes some mishaps on the scale types.
	+ Adds a way to deactivate the StealthSave via MM patching.
	+ Closes Issues:
		- [#208](https://github.com/net-lisias-ksp/TweakScale/issues/208) Chain Scaling Parts with variants are borking when the parent part is "inverted"
		- [#207](https://github.com/net-lisias-ksp/TweakScale/issues/207) The Upgrade Pipeline thingy (or something else?) is playing havoc with TweakScale 
		- [#175](https://github.com/net-lisias-ksp/TweakScale/issues/175) Wrong displacement of the attached part placed inverted when scaling its parent
		- [#163](https://github.com/net-lisias-ksp/TweakScale/issues/163) Radial Symmetry (when using variants) are misplacing parts.
		- [#131](https://github.com/net-lisias-ksp/TweakScale/issues/131) Chain Scaling parts is playing havoc with the Radial Attachment Positions. 
		- [#36](https://github.com/net-lisias-ksp/TweakScale/issues/36) [TweakScale Warning] Exception during ModulePartVariants interaction
* 2021-0927: 2.5.0.38 **BETA** (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#208](https://github.com/net-lisias-ksp/TweakScale/issues/208) Chain Scaling Parts with variants are borking when the parent part is "inverted"
		- [#207](https://github.com/net-lisias-ksp/TweakScale/issues/207) The Upgrade Pipeline thingy (or something else?) is playing havoc with TweakScale 
		- [#175](https://github.com/net-lisias-ksp/TweakScale/issues/175) Wrong displacement of the attached part placed inverted when scaling its parent
		- [#163](https://github.com/net-lisias-ksp/TweakScale/issues/163) Radial Symmetry (when using variants) are misplacing parts.
		- [#131](https://github.com/net-lisias-ksp/TweakScale/issues/131) Chain Scaling parts is playing havoc with the Radial Attachment Positions. 
		- [#36](https://github.com/net-lisias-ksp/TweakScale/issues/36) [TweakScale Warning] Exception during ModulePartVariants interaction
* 2021-1016: 2.4.6.1 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Rolls back an incomplete localization issue that passed through while merging features from the development branch.
* 2021-1010: 2.4.6.0 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	* Breaks the 1.4.4 barrier! Now TweakScale supports from KSP 1.3.0 to the latest! **#HURRAY!!**
	* Resurrects the AutoScale feature. Use `CTRL-L` to activate/deactivate
		+ A HotKey collision with a 3rd-party add'on is a Known Issue. This will be tackled down on [Issue #202](https://github.com/net-lisias-ksp/TweakScale/issues/202).
	* Updates KSPe.Light.TweakScale to 2.4.0.3
		+ **ATTENTION!!** : Users of the following TweakScale Companions **must** update them _immediatelly_, as this release breaks binary compatibility (i.e. they will not load!!):
			- [TSCo_FS](https://github.com/net-lisias-ksp/TweakScaleCompanion_FS/releases/tag/RELEASE%2F1.1.0.0)
			- [TSCo_KIS](https://github.com/net-lisias-ksp/TweakScaleCompanion_KIS/releases)
			- [TSCo_PKMC](https://github.com/net-lisias-ksp/TweakScaleCompanion_PKMC/releases)
			- [TSCo_Visuals](https://github.com/net-lisias-ksp/TweakScaleCompanion_Visuals/releases/tag/PRERELEASE%2F0.2.0.0)  
* 2021-0927: 2.5.0.37 **BETA** (Lisias) for KSP >= 1.3
	+ Maintenance release
		- Declares the Ground Anchor as Experimental
		- Updates the code base to the latest KSPe release (2.4.0.1 at this moment)
* 2021-0925: 2.4.5.9 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Adds (missing) patches for 3 parts that I left behind:
		+ LV-T30 Reliant V2
		+ LV-T45 Swivel V2
		+ Ground Anchor (in Experimental yet)
	+ Ongoing savegames still using the 'V1' parts didn't noticed the bork, because these parts still exists on the game (they are only hidden) and are being scaled normally. Only new crafts and savegames were being hindered by the absence of these two patches.
* 2021-0922: 2.5.0.36 Beta (Lisias) for KSP >= 1.3.1
	+ Recompiled against the new KSPe 2.4.0.0
		- [Update before using](https://github.com/net-lisias-ksp/KSPAPIExtensions/releases/tag/RELEASE%2F2.4.0.0), it's a hard dependency on the new Version. 
* 2021-0913: 2.4.5.8 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Fixes (again) the Decluttering thingy. 
	+ Closes Issues:
		- [#201](https://github.com/net-lisias-ksp/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
* 2021-0913: 2.5.0.35 Beta (Lisias) for KSP >= 1.3.1
	+ Fixes (again) the Decluttering thingy. 
	+ Closes Issues:
		- [#201](https://github.com/net-lisias-ksp/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
* 2021-0912: 2.4.5.7 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Better compatibility with 3rd Party Add'Ons when Decluttering. 
	+ Closes Issues:
		- [#201](https://github.com/net-lisias-ksp/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
* 2021-0912: 2.5.0.34 Beta (Lisias) for KSP >= 1.3.1
	+ Catches up all fixes already published on mainstream.
	+ Implements support for **ALL** KSP versions downto 1.3.1 !!! #HURRAY!!
		- Specialised DLLs loaded under demand, thanks KSPe!
		- 1.2.2, however, will need more work. Unsure if it worths it.
	+ Better compatibility with 3rd Party Add'Ons when Decluttering. 
	+ Closes Issues:
		- [#201](https://github.com/net-lisias-ksp/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
		- [#198](https://github.com/net-lisias-ksp/TweakScale/issues/198) Breakdown Scaling support for each major KSP version in specialised DLLs
* 2021-0912: 2.5.0.33 Beta (Lisias) for KSP >= 1.3.1
	+ Ditching this version as a new release was made on the same day. 
* 2021-0911: 2.4.5.5 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Re-release for Spacedock, as I managed to upload the wrong package. 
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
* 2021-0823: 2.4.5.3 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ **DITCHED**
	+ Since I borked (again) the release on the Spacedock, I'm jumping this version to prevent playing havoc with CKAN.
		- Isso tá ficando chato, já. :(
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
* 2021-0327: 2.4.4.6 (Lisias) for 1.4.4 <= KSP <= 1.11.2
	+ Allows running on KSP 1.11.2 without an warning
	+ Warns the user to install KSP Recall on KSP 1.11.x too.
	+ Closes Issues:
		- [#165](https://github.com/net-lisias-ksp/TweakScale/issues/165) Issue with TweakScale instal 
			- Related to CurseForge. 
* 2021-0410: 2.5.0.32 Beta (Lisias) for KSP >= 1.4.4
	+ Declutters craft files, preventing TweakScale MODULE nodes from begin written on it when the part is not scaled or it's deactivated.
		- Now your unscaled crafts can be shared on KerbalX *et all* without being tagged as using TweakScale.
		- And you can play Challenges where TweakScale is not allowed without deinstalling TweakScale - or creating a new installment just because of it.
	+ Closes Issues:
		- [#85](https://github.com/net-lisias-ksp/TweakScale/issues/85) Clean TweakScale's Module from unchanged parts on save 
* 2021-0403: 2.5.0.31 Beta (Lisias) for KSP >= 1.4.4
	+ Implements `Active` and `Available` properties for runtime, part by part, control of availability of TweakScale features.
		- `Active` controls if TweakScale will be activated for a given part.
			- Inactivated Tweakscale will revert to the default scale, and the user will not be able to rescale the part unless he/she reactivate it using the PAW. No Scaling controls are enabled when inactivated.
		- `Available` controls if TweakScale widgets on PAW will be available for the user, being it active or not.
			- This can lock the current TweakScale state, as the user will not be able to change it.
			- Can only be set (or reset) programatically or by patches.   
		- Perfect for Challenges and Custom, dedicated Savegames. 
	+ Recognises KSP 1.11.2 as a supported version.
* 2021-0212: 2.5.0.30 Beta (Lisias) for KSP >= 1.4.4
	+ Back to the beta program!
	+ Consolidates all changes up to Release 2.4.4.5
	+ Some minor code compliance and preemptive bug fixes
	+ **Ressurrects** the long gone **AutoScale** #HURRAY!!!!
* 2021-0120: 2.4.4.5 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ Fix some somewhat embarrassing bugs on the default patching.
		- Thank you very much, [AccidentalDisassembly](https://forum.kerbalspaceprogram.com/index.php?/profile/110936-accidentaldisassembly/)!
	+ Closes Issues:
		- [#158](https://github.com/net-lisias-ksp/TweakScale/issues/158) Duplicate TS patches in provided .cfgs
		- [#157](https://github.com/net-lisias-ksp/TweakScale/issues/157) Missing or extra curly braces
* 2021-0108: 2.4.4.4 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ Fix a nasty regression on scaling drag cubes. Sorry.
	+ Closes Issues:
		- [#155](https://github.com/net-lisias-ksp/TweakScale/issues/155) Drag is being terribly scaled!
* 2021-0105: 2.4.4.3 R2 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ Same old 2.4.4.3 release, but with some additional dummy patches to prevent FATALities when updating over a dirty TweakScale folder.
	+ Fix some regressions from 2.4.4.2 that passed through.
	+ Updates KSPe.Light for TweakScale
	+ Closes Issues:
		- [#154](https://github.com/net-lisias-ksp/TweakScale/issues/154) Problems on loading crafts with parts with variants that are surface attached
		- [#143](https://github.com/net-lisias-ksp/TweakScale/issues/143) Wait for KSP Recall to close its issue #9 *AND* #11
* 2021-0104: 2.4.4.3 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ ***ditched***
* 2020-1228: 2.4.4.2 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ Fixes a NRE that passed through unattended for some time since last refactoring.
	+ **FINALLY** identified the merge error that leaked a duplicated patch file into the distribution stream!
	+ Closes issues:
		- [#152](https://github.com/net-lisias-ksp/TweakScale/issues/152) Unexpected NRE while initialising scaled parts on launch
* 2020-1228: 2.4.4.1 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ ***DITCHED***
* 2020-1226: 2.4.4.0 (Lisias) for 1.4.4 <= KSP <= 1.11.1
	+ (Temporarily) Raises the bar to KSP 1.4.4 due the Variant stunt.
		- Support for KSP down to 1.2.2 is still on the back log, but I need to finish support for modern KSP first!
	+ Guaranteed support from 1.4.4 and newer.
	+ From now on, tweakScale is licensed under [SKP 1.0](https://ksp.lisias.net/SKL-1_0.txt) **or** [GPL 2.0](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html).
		- See [KNOWN ISSUES](./KNOWN_ISSUES.md) if you think this can affect you somehow.
	+ Closes issues:
		- [#141](https://github.com/net-lisias-ksp/TweakScale/issues/141) Latest beta - interaction with Ground Construction...
		- [#126](https://github.com/net-lisias-ksp/TweakScale/issues/126) Changing the scale of an attached part doesn't update the Craft Cost on the spot.
	+ Formally closes the following issues, backporting (almost) all fixes from the Beta Releases up to 2.5.0.27
		- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
		- [#142](https://github.com/net-lisias-ksp/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
		- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
		- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
		- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
		- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
		- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
		- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
		- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
		- [#87](https://github.com/net-lisias-ksp/TweakScale/issues/87) Wrong default scales (partial)
		- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
		- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
		- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
		- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs
		- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches (concluded)
		- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
		- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
		- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
		- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
		- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
		- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
		- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
			- Added scaling to Proppelers 
		- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
		- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
* 2020-1125: 2.5.0.29 Beta (Lisias) for KSP >= 1.4.4
	+ I missed a detail, I didn't accounted for the `attPos` atribute (moving an attached part with the "Move" tool). Fixed.
* 2020-1123: 2.5.0.28 Beta (Lisias) for KSP >= 1.4.4
	+ Scaled parts with Variants now correctly translates the attached part when applying variants #HURRAY
		- As long the part has no symmetry, when things get completely screwed up...
		- See [this comment](https://github.com/net-lisias-ksp/TweakScale/issues/42#issuecomment-732321477) on [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) for details.
* 2020-1113: 2.5.0.27 Beta (Lisias) for KSP >= 1.4.4
	+ Fixes a regression on Chain Scaling introduced on .25 and passed undetected on .26.
	+ (Almost) implements changing variants on scaled part
		+ There's something missing yet that affects the repositioning, specially on the Mastodon. 
	+ Known Issues:
		- Scaling parts with variants that change attachment nodes have a glitch, affecting:
			- The Mastodon engine
			- The Structural Tubes
				- T-12
				- T-18
				- T-25
				- T-37
				- T-50
			- And probably more, as Add'Ons starts to use such feature.
			- See [this comment](https://github.com/net-lisias-ksp/TweakScale/issues/42#issuecomment-726428889) on [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) for details.
		- Detaching and reattaching the Mastodon work arounds the problem on the engine.
		- Detaching and reattaching the parts attached to a scaled tube work arounds the problem with the tubes.
		- Things on KSP 1.9 are yet more problematic. [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall/issues/9) will tackle this down.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.23
			- [#142](https://github.com/net-lisias-ksp/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
			- [#87](https://github.com/net-lisias-ksp/TweakScale/issues/87) Wrong default scales (partial)
		- 2.5.0.21
			- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
		- Bug reports for this release **should be issued on the [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) only**, as development problems are not considered "bugs" and should not clutter the back log where real issues happening the field need to be tackled down
* 2020-1112: 2.5.0.26 Beta (Lisias) for KSP >= 1.4.4
	+ (Almost) implements changing variants on scaled part
		+ There's something missing yet that affects the repositioning, specially on the Mastodon. 
	+ Known Issues:
		- Scaling parts with variants that change attachment nodes have a glitch, affecting:
			- The Mastodon engine
			- The Structural Tubes
				- T-12
				- T-18
				- T-25
				- T-37
				- T-50
			- And probably more, as Add'Ons starts to use such feature.
			- See [this comment](https://github.com/net-lisias-ksp/TweakScale/issues/42#issuecomment-726428889) on [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) for details.
		- Detaching and reattaching the Mastodon work arounds the problem on the engine.
		- Detaching and reattaching the parts attached to a scaled tube work arounds the problem with the tubes.
		- Things on KSP 1.9 are yet more problematic. [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall/issues/9) will tackle this down.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.23
			- [#142](https://github.com/net-lisias-ksp/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
			- [#87](https://github.com/net-lisias-ksp/TweakScale/issues/87) Wrong default scales (partial)
		- 2.5.0.21
			- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
		- Bug reports for this release **should be issued on the [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) only**, as development problems are not considered "bugs" and should not clutter the back log where real issues happening the field need to be tackled down
* 2020-1023: 2.5.0.25 Beta (Lisias) for KSP >= 1.4.4
	+ Correctly implements scaling Mass and Cost.
		- Again. 
	+ Fixes a regression where symmetry counterparts were not being scaled.
		- Thanks for the [report](https://github.com/net-lisias-ksp/TweakScale/issues/42#issuecomment-703414755), AccidentalDisassembly! 
	+ Fixes the Scale Chaining when non scalable parts are present.
	+ Known Issues:
		- Scaling parts with variants that change attachment nodes are still problematic at this moment affecting:
			- The Mastodon engine
			- The Structural Tubes
				- T-12
				- T-18
				- T-25
				- T-37
				- T-50
			- And probably more, as Add'Ons starts to use such feature.
		- Detaching and reattaching the Mastodon work arounds the problem on the engine.
		- Detaching and reattaching the parts attached to a scaled tube work arounds the problem with the tubes.
		- Things on KSP 1.9 are yet more problematic. [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall/issues/9) will tackle this down.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.23
			- [#142](https://github.com/net-lisias-ksp/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
			- [#87](https://github.com/net-lisias-ksp/TweakScale/issues/87) Wrong default scales (partial)
		- 2.5.0.21
			- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
		- Bug reports for this release **should be issued on the [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) only**, as development problems are not considered "bugs" and should not clutter the back log where real issues happening the field need to be tackled down
* 2020-0918: 2.5.0.24 Beta (Lisias) for KSP >= 1.4.4
	+ Alleviates a bit the scaling issues on parts with Variants with Attachment Nodes introduced on .23
		- Still don't works as it should, but now the nodes are not messed up, so savegames will be good now.
	+ Known Issues:
		- Scaling parts with variants that change attachment nodes is problematic at this moment affecting:
			- The Mastodon engine
			- The Structural Tubes
				- T-12
				- T-18
				- T-25
				- T-37
				- T-50
			- And probably more, as Add'Ons starts to use such feature.
		- Detaching and reattaching the Mastodon work arounds the problem on the engine.
		- Detaching and reattaching the parts attached to a scaled tube work arounds the problem with the tubes.
		- Things on KSP 1.9 are yet more problematic. [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall/issues/9) will tackle this down.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.23
			- [#142](https://github.com/net-lisias-ksp/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
			- [#87](https://github.com/net-lisias-ksp/TweakScale/issues/87) Wrong default scales (partial)
		- 2.5.0.21
			- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0917: 2.5.0.23 Beta (Lisias) for KSP >= 1.4.4
	+ Fixes the DryCost problem introduced on .22
	+ Known Issues:
		- Scaling parts with variants that change attachment nodes is problematic at this moment, affecting:
			- The Mastodon engine
			- The Tubes
		- Detaching and reattaching the Mastodon work arounds the problem on the engine, but the Tubes are really problematic and I don't have a workaround for it by now.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.23
			- [#142](https://github.com/net-lisias-ksp/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
			- [#87](https://github.com/net-lisias-ksp/TweakScale/issues/87) Wrong default scales (partial)
		- 2.5.0.21
			- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0905: 2.5.0.22 Beta (Lisias) for KSP >= 1.4.4
	+ Maintenance release, fixing some badly implemented features on the .21 one.  
* 2020-0905: 2.5.0.21 Beta (Lisias) for KSP >= 1.4.4 
	+ Raises the bar to KSP 1.4.4, as Variant with Mass and Costs started to be supported only from this version.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.21
			- [#138](https://github.com/net-lisias-ksp/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/net-lisias-ksp/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)	
* 2020-0830: 2.5.0.20 Beta (Lisias) for KSP >= 1.4.1 
	+ Updating KSPe.Light for TweakScale
		- Needed as some Companions will need it soon. 
	+ Fixed mishaps on withdrawing `TweakScale` from insane parts
	+ Lifting the ban on KSP 1.10.1
	+ Fixing an error on handling Warnings and Houstons where a warning would occlude a Houston!
	+ New helpers for the Companions.
	+ Better (and safer) deactivation code using info gathered from [TweakScale](https://github.com/net-lisias-ksp/TweakScale/issues/125).
	+ Updating the Houston for KSP-Recall
		- Only KSP 1.9.x needs it, currently. 
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.20
			- [#137](https://github.com/net-lisias-ksp/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/net-lisias-ksp/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/net-lisias-ksp/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)	
* 2020-0830: 2.5.0.19 Beta (Lisias) for KSP >= 1.4.1 
	+ **Withdrawn**
		- Superseded by 2.5.0.20. 
* 2020-0829: 2.5.0.18 Beta (Lisias) for KSP >= 1.4.1 
	+ Updating KSPe.Light for TweakScale
		- Needed as some Companions will need it soon. 
* 2020-0822: 2.5.0.17 Beta (Lisias) for KSP >= 1.4.1 
	+ Fixed mishaps on withdrawing `TweakScale` from insane parts
	+ Lifting the ban on KSP 1.10.1
	+ Fixing an error on handling Warnings and Houstons where a warning would occlude a Houston!
* 2020-0804: 2.4.3.21 (Lisias) for 1.4.1 <= KSP <= 1.10.1
	+ A new **FINAL** release for the 2.4.3.x series. :P
	+ A mishap on handling rogue duplicates was detected and fixed
		- This was triggering false alarms, raising suspicions on legit use cases on third-parties add'ons.
* 2020-0720: 2.4.3.20 (Lisias) for 1.4.1 <= KSP <= 1.10.1
	+ Yet another **FINAL** release for the 2.4.3.20. :P 
	+ KSP 1.10.1 is considered supported. No more Houstons on it.
		- But anything later will trigger a Houston again.
* 2020-0718: 2.4.3.19 (Lisias) for 1.4.1 <= KSP <= 1.10.0
	+ **FINAL** release for the 2.4.3.x series.
		- Honest this time. I hope... :)
	+ KSP 1.10 is considered supported. No more Houstons on it.
		- But anything later will trigger a Houston again.
	+ Some new Parts are now scalable.
		- [Thanks](https://github.com/net-lisias-ksp/TweakScale/pull/122), [7ranceaddic7](https://github.com/net-lisias-ksp/TweakScale/pull/123)!
	+ [KSP-Recall](https://github.com/net-lisias-ksp/KSP-Recall/releases) is only pushed on KSP 1.9.x now.
	+ A somewhat stupid mistake on handling `GameEvents.onEditorShipModified` [was fixed](https://github.com/net-lisias-ksp/TweakScale/issues/119).
	+ A pretty stupid mistake on handling `OnDestroy` was fixed. Thanks again, [7ranceaddic7](https://github.com/net-lisias-ksp/TweakScale/issues/124)
	+ A [weird mistake](https://github.com/net-lisias-ksp/TweakScale/issues/125) on withdrawing "insane" parts were detected and fixed.
		- This time. (sigh) 
* 2020-0715: 2.5.0.16 Beta (Lisias) for KSP >= 1.4.1 
	+ New helpers for the Companions.
	+ Better (and safer) deactivation code using info gathered from [TweakScale](https://github.com/net-lisias-ksp/TweakScale/issues/125).
	+ Updating the Houston for KSP-Recall
		- Only KSP 1.9.x needs it, currently. 
* 2020-0713: 2.4.3.18 (Lisias) for 1.4.1 <= KSP <= 1.10.0
	+ **Withdrawn**
		- I refuse to acknowledge the existence of this thing.  
* 2020-0711: 2.5.0.15 Beta (Lisias) for KSP >= 1.4.1 
	+ Huge, comprehensive overhaul of the whole patching system.
		- This will make my life **way** easier when adding support for new parts and detecting merge errors that started to plague my pull requests lately. 
	+ Added support for some missed parts from Stock and MH (see the previous point)
	+ Added support for the simplest parts from Serenity (no robotics yet)
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.15
			- [#124](https://github.com/net-lisias-ksp/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/net-lisias-ksp/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0710: 2.4.3.17 (Lisias) for 1.4.1 <= KSP <= 1.10.999
	+ **Withdrawn**
		- I refuse to acknowledge the existence of this thing.  
* 2020-0708: 2.4.3.16 (Lisias) for KSP >= 1.4.1
	+ **Withdrawn**
		- I refuse to acknowledge the existence of this thing.  
* 2020-0625: 2.5.0.14 Beta (Lisias) for KSP >= 1.4.1 
	+ Module Manager is not distributed anymore.
		- A [Watch Dog](https://github.com/net-lisias-ksp/ModuleManagerWatchDog) is being distributed instead.
		- This will prevent users from running older versions of MM unattendly. Full history on [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-141-tweakscale-under-lisias-management-24314-2020-0519/&do=findComment&comment=3798088).
	+ Fixed a pretty dumb mistake on a TWEAKSCALEBEHAVIOUR (ModuleGeneratorExtended).
		- Don't have a clue when I messed up, the first release of that file was working. :(
	+ Some smarter logging and warnings
		- The MM cache is now checked, and Warnings only popup when it is newer than 1 hour.
		- No more Dialog spamming.
		- Sorry being late on this.
	+ New "Houston" when running on KSP >= 1.9 without KSP Recall
		- It's the only way to prevent KSP to reset the Resources to the `prefab` state after scaling the parts! 
	+ New "Advise" when running on KSP >= 1.10
		- Given the numerous problems I still have to handle from KSP 1.8 and 1.9, I don't think it's wise to use TweakScale on KSP 1.10 without a lot of testing from my side first. Proceed with caution, and use [S.A.V.E.](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-*) just in case.
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.14
			- [#115](https://github.com/net-lisias-ksp/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/net-lisias-ksp/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)* 2020-0623: 2.4.3.15 (Lisias) for KSP >= 1.4.1
	+ Module Manager is not distributed anymore.
		- A [Watch Dog](https://github.com/net-lisias-ksp/ModuleManagerWatchDog) is being distributed instead.
		- This will prevent users from running older versions of MM unatendly. Full history on [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-141-tweakscale-under-lisias-management-24314-2020-0519/&do=findComment&comment=3798088).
	+ Fixed a pretty dumb mistake on a TWEAKSCALEBEHAVIOUR (ModuleGeneratorExtended).
		- Don't have a clue when I messed up, the first release of that file was working. :(
	+ Some smarter logging and warnings
		- The MM cache is now checked, and Warnings only popup when it is newer than 1 hour.
		- No more Dialog spammimg.
		- Sorry being late on this.
	+ New "Houstons"
		- When running on KSP >= 1.9 without KSP Recall
			- It's the only way to prevent KSP to reset the Resources to the `prefab` state after scaling the parts! 
		- When running on KSP >= 1.10
			- Given the numerous problems I still have to handle from KSP 1.8 and 1.9, I don't think it's wise to use TweakScale on KSP 1.10 without a lot of testing from my side first.
* 2020-0531: 2.5.0.13 Beta (Lisias) for KSP >= 1.4.1 
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0519: 2.4.3.14 (Lisias) for KSP >= 1.4.1 
	+ Closes issue:
		- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
* 2020-0505: 2.5.0.12 Beta (Lisias) for KSP >= 1.4.1 
	+ Changes
		- Fixed a problem on KSP 1.9 Editor with Attachments on Parts with Variants. 
		- Fixed a pretty stupid mistake on handling TweakScaleRogueDuplicates
		- Refactoring code
		- Refactoring filesystem.
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.12
			- [#110](https://github.com/net-lisias-ksp/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0518: 2.4.3.13 (Lisias) for KSP >= 1.4.1 
	+ **DITCHED**
* 2020-0505: 2.5.0.11 Beta (Lisias) for KSP >= 1.4.1 
	+ Some new methods on `TweakScale Module` to make life easier to the TweakScale Companions.
	+ All non Squad related patches are now on EoL, and are expected to be deprecated soon.
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.11
			- [#106](https://github.com/net-lisias-ksp/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0426: 2.4.3.12 (Lisias) for KSP >= 1.4.1 
	+ Some more care on supporting Stock and DLC parts
	+ KIS and KAS patches is now on EoL, and are expected to be deprecated soon.
	+ Closes issues:
		- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
		- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
		- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
		- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
* 2020-0326: 2.5.0.10 Beta (Lisias) for KSP >= 1.4.1 
	+ Some more care on supporting Stock and DLC parts
	+ KIS and KAS patches is now on EoL, and are expected to be deprecated soon.
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.10
			- [#103](https://github.com/net-lisias-ksp/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/net-lisias-ksp/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/net-lisias-ksp/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2020-0303: 2.4.3.11 (Lisias) for KSP >= 1.4.1
	* Adding support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall), allowing TweakScale to be used on KSP 1.9.x due a new glitch introduced by these KSP versions.
		- Closes issue [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98)
* 2020-0302: 2.5.0.9 Beta (Lisias) for KSP >= 1.4.1 
	+ Some care on documentation
	+ Some care on support Stock parts
	+ Fixing a nasty mistake on the default scale of the Mk3 Engine Mount (`adapterEngine`)
		- This can break existing savegames!
	+ NFS patches is now on EoL, and are expected to be deprecated soon.
	+ Some resilience on scaling parts
	+ Added Localisation Suport for EN-US and ZH-CN
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/net-lisias-ksp/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-1104: 2.5.0.8 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/net-lisias-ksp/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/net-lisias-ksp/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/net-lisias-ksp/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-1030: 2.4.3.10 (Lisias) for KSP >= 1.4.1
	+ Lifting the ban on running on KSP 1.8, as the glitch detected proved inoffensive (beside annoying) and was fixed on KSP 1.8.1
	+ Changing install layout for `Scale_Dist.dll` .
		- Needed to prevent being annoyed by Add'On Binder errors.
		- Please read [KNOWN ISSUES](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) for further information.
* 2019-1024: 2.4.3.9 (Lisias) for 1.4.1 <= KSP < 1.8
	+ Repacking of 2.4.3.8 to fix a mistake on the files needed by CKAN.
* 2019-1018: 2.4.3.8 (Lisias) for 1.4.1 <= KSP < 1.8
	+ This Release locks TweakScale to run only on KSP versions greater or equal 1.4.1 and **less** then 1.8 .
	+ Updated KSPe Light for TweakScale:
		- Checking against incompatible Unity Versions
		- And this thing worked fine on Unity 2019.2 ! :)
	+ Closing or reworking the following issues:
		- [#79](https://github.com/net-lisias-ksp/TweakScale/issues/79) Prevent TweakScale from running on Incompatible Unity versions
* 2019-1011: 2.4.3.7 (Lisias) for KSP >= 1.4.1
	+ Updated KSPe Light for TweakScale:
		- Standard Installation Check
			- Working on Windows this time! 
		- Common Dialogs
		- Internal routines updated to understand Unity 2019. 
			- KSP 1.8 Ready, baby! ;)
	+ Closing or reworking the following issues:
		- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
		- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- And correctly cleaning up this time. 
		-[#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
* 2019-1011: 2.4.3.6 (Lisias) for KSP >= 1.4.1
	+ **DITCHED** 
* 2019-1011: 2.4.3.5 (Lisias) for KSP >= 1.4.1
	+ **DITCHED** 
* 2019-0903: 2.4.3.4 (Lisias) for KSP >= 1.4.1
	+ Closing or reworking the following issues:\
		- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used
		- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- Thanks to [Dizor](https://forum.kerbalspaceprogram.com/index.php?/profile/161502-dizor/). I'm still [laughing](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-14-tweakscale-under-lisias-management-2433-2019-0814/page/33/&tab=comments#comment-3666432)! :D
	+ New hotfixes:
		- Contares ([old](https://forum.kerbalspaceprogram.com/index.php?/topic/122102-13x-contares-189-closed/) and [new](https://forum.kerbalspaceprogram.com/index.php?/topic/171305-17x-csa-contares-core-2012/)) breaking TweakScale.
* 2019-0903: 2.5.0.6 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ Added support for hot-fixes - handcrafted patches to brute force a correct path when the normal way is not possible - as when an unmaintained ARR Add'On is involved on the mess.
		- New hot fix for [CxAerospace:Station Parts](https://forum.kerbalspaceprogram.com/index.php?/topic/138910-dev-halted13-cxaerospace-stations-parts-pack-v162-2017-5-24/page/31/) breaking [Bluedog_DB](https://forum.kerbalspaceprogram.com/index.php?/topic/122020-16x-bluedog-design-bureau-stockalike-saturn-apollo-and-more-v152-бруно-8feb2019/). 
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.6 	
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-1010: 2.5.0.7 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.7
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/net-lisias-ksp/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/net-lisias-ksp/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/net-lisias-ksp/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/net-lisias-ksp/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-0814: 2.4.3.3 (Lisias) for KSP >= 1.4.1
	+ Added support for hot-fixes - handcrafted patches to brute force a correct path when the normal way is not possible - as when an unmaintained ARR Add'On is involved on the mess.
		- New hot fix for [CxAerospace:Station Parts](https://forum.kerbalspaceprogram.com/index.php?/topic/138910-dev-halted13-cxaerospace-stations-parts-pack-v162-2017-5-24/page/31/) breaking [Bluedog_DB](https://forum.kerbalspaceprogram.com/index.php?/topic/122020-16x-bluedog-design-bureau-stockalike-saturn-apollo-and-more-v152-бруно-8feb2019/). 
* 2019-0813: 2.5.0.5 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ Added support for hot-fixes - handcrafted patches to brute force a correct path when the normal way is not possible - as when an unmaintained ARR Add'On is involved on the mess.
		- New hot fix for [CxAerospace:Station Parts](https://forum.kerbalspaceprogram.com/index.php?/topic/138910-dev-halted13-cxaerospace-stations-parts-pack-v162-2017-5-24/page/31/) breaking [Bluedog_DB](https://forum.kerbalspaceprogram.com/index.php?/topic/122020-16x-bluedog-design-bureau-stockalike-saturn-apollo-and-more-v152-бруно-8feb2019/). 
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
		- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
		- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
		- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
		- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
		- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
		- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
		- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
		- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
		- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-0084: 2.5.0.4 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
		- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
		- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
		- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
		- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
		- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
		- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
		- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
		- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
		- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
		- [#65](https://github.com/net-lisias-ksp/TweakScale/issues/65) Support for new Nertea's Cryo Engines
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-0721: 2.5.0.3 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues:
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
		- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
		- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- [#47](https://github.com/net-lisias-ksp/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
		- [#48](https://github.com/net-lisias-ksp/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
		- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
		- [#50](https://github.com/net-lisias-ksp/TweakScale/issues/50) Check the patches for currently supported Add'Ons
		- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
		- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
		- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
		- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- [#58](https://github.com/net-lisias-ksp/TweakScale/issues/58) Mk4 System Patch (addendum)
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
* 2019-0622: 2.5.0.2 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues (some already fixed, others not yet):
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
		- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
		- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- [#49](https://github.com/net-lisias-ksp/TweakScale/issues/49) Check the Default patches for problems due wildcard!
		- [#51](https://github.com/net-lisias-ksp/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
		- [#54](https://github.com/net-lisias-ksp/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
		- [#56](https://github.com/net-lisias-ksp/TweakScale/issues/56) "Breaking Parts" patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods ***WiP***
		- [#57](https://github.com/net-lisias-ksp/TweakScale/issues/57) Implement Warning Dialogs ***WiP***
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
* 2019-0508: 2.5.0.1 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, merging the latest release fixes and aiming to test solutions and check stability issues related to the following issues (some already fixed, others not yet):
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties]
		- [#41](https://github.com/net-lisias-ksp/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
		- [#42](https://github.com/net-lisias-ksp/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us, please use this only under my instructions, and only if I ask you to do so! Twice. :)
* 2019-0608: 2.4.3.0 (Lisias) for KSP >= 1.4.1
	+ This is an emergencial Release due a Show Stopper issue (see Issue #34 below) with some new features.
	+ Adding features:
		- [#7](https://github.com/net-lisias-ksp/TweakScale/issues/7) Adding support for new Parts from KSP 1.5 and 1.6 (and Making History)! (**finally!**)
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Checking for new Parts on KSP 1.7 (none found)
			- (Serenity is Work In Progress)
		- Adding KSPe.Light support for some UI features. 
	+ Fixing bugs:
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties
	+ [Known Issues](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) update:
		- A new and definitively destructive interaction was found due some old or badly written patches ends up injecting TweakScale properties **twice** on the Node.
* 2019-0508: 2.5.0.0 Beta (Lisias) for KSP >= 1.4.1 TEST RELEASE
	+ This is a beta release, aiming to test solutions and check stability issues related to the following issues (some already fixed, others not yet):
		- [#07](https://github.com/net-lisias-ksp/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
		- [#10](https://github.com/net-lisias-ksp/TweakScale/issues/10) Weird late ADDON-Binder issue
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts.
		- [#21](https://github.com/net-lisias-ksp/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
		- [#31](https://github.com/net-lisias-ksp/TweakScale/issues/31) Preventing being ran over by other mods
		- [#34](https://github.com/net-lisias-ksp/TweakScale/issues/34) New Sanity Check: duplicated properties
		- [#35](https://github.com/net-lisias-ksp/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them.
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us, please use this only under my instructions, and only if I ask you to do so! Twice. :)
* 2019-0505: 2.4.2.0 (Lisias) for KSP >= 1.4.1
	+ Adding features:
		- [#32](https://github.com/net-lisias-ksp/TweakScale/issues/32) Near Future Aeronautics Patches
	+ Fixing bugs:
		- [#20](https://github.com/net-lisias-ksp/TweakScale/issues/20)	Duplicated TweakScale support on some parts
		- [#23](https://github.com/net-lisias-ksp/TweakScale/issues/23) Unhappy merge on TweakScale exponents
		- [#24](https://github.com/net-lisias-ksp/TweakScale/issues/24) Fix that duplicated support on some parts
		- [#30](https://github.com/net-lisias-ksp/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used
	+ [Known Issues](https://github.com/net-lisias-ksp/TweakScale/blob/master/KNOWN_ISSUES.md) update:
		- Users of "Classic" [Infernal Robotics](https://github.com/MagicSmokeIndustries/InfernalRobotics) should avoid scaling parts to "Small -" or Krakens will be released.
			- [Infernal Robotics/Next](https://github.com/meirumeiru/InfernalRobotics) fixes this issue.   
* 2019-0216: 2.4.1.0 (Lisias) for KSP >= 1.4.1
	+ Adding 1.875 scale as default (being now a Stock size on MH, it makes sense to properly acknowledge it). Suggested by Tyko.
		- Closing issue [#3](https://github.com/net-lisias-ksp/TweakScale/issues/3)
	+ Adding support for Stock Alike Station Parts. Courtesy of Speadge.
		- Closing issue [#8](https://github.com/net-lisias-ksp/TweakScale/issues/8)
	+ Fixed a critical craft corruption (even flying ones) as TweakScale is sometimes being injected twice (or even more) into a part. This patch does not fix the duplicity, but prevent your crafts from being corrupted once a fix is applied (yeah - fixing the bug would cause craft corruption without this patch!)
		- Closing issue [#20](https://github.com/net-lisias-ksp/TweakScale/issues/20)
* 2018-1229: 2.4.0.7 (Lisias) for KSP >= 1.4.1
	+ KSP 1.6 (partial) support certified.
	+ Actively reverting support in runtime for parts with problematic or unsupported modules.
		- Closing issue [#9](https://github.com/net-lisias-ksp/TweakScale/issues/9)
		- Closing issue [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11)
		- Closing issue [#12](https://github.com/net-lisias-ksp/TweakScale/issues/12)
	+ Lifting the Max KSP restriction on the `.version` file.
	+ Updating Module Manager to 3.1.1
* 2018-1027: 2.4.0.6 (Lisias) for KSP 1.4.1+; 1.5
	+ KSP 1.5 support certified.
	+ Reverting some misunderstood versioning.
	+ Moving the repository to the Official Headquarters
	+ Some performance (and type safety) enhancements
	+ Fixes on the MX-3L Hermes (NFT) as proposed by NachtRaveVL
	+ Fixed an issue when Making History is present.
		- With additional failsafe measure 
	+ Bumping Release to assume official maintenance and mark the URL change.
	+ **Properly** Reverting KSPe dependency (for while, at least).
		- Unholy user settings files are back to the Sacred Land of GameData. Repent, Sinner!!
* 2018-1027: 2.4.0.5 (Lisias) for KSP 1.4.1+; 1.5
	* Move on, nothing to see here! =P 
* 2018-1025: 2.4.0.4 (Lisias) for KSP 1.4
	+ DITCHED
* 2018-1019: 2.4.0.3 (Lisias) for KSP 1.4
	+ DITCHED
* 2018-1016: 2.4.0.2 (Lisias) for KSP 1.4
	+ DITCHED
* 2018-1016: 2.4.0.1 (Lisias) for KSP 1.4
	+ DITCHED
* 2018-0816: 2.3.12.1 (Lisias) for KSP 1.4.x
	+ Saving xml config files under <KSP_ROOT>/PluginData Hierarchy
		- Added hard dependency for [KSP API Extensions/L](https://github.com/net-lisias-ksp/KSPAPIExtensions). 
	+ Removed deprecated DLLs
		- Needs TweakableEverything installed now
		- A small hack:
			- one DLL was moved to a new Plugin directory inside the dependency to overcome the loading order problem.
			- a better solution is WiP for the next release
	+ Removed Support code for deleted KSP functionalities
		- Not needed anymore? (RiP) 
* 2018-0416: 2.3.12 (Pellinor) for KSP 1.4.2
	+ configs for new parts
	+ fix for exceptions
	+ fix for solar panels
	+ recompile for KSP 1.4.2
* 2018-0314: 2.3.10 (Pellinor) for KSP 1.4.1
	+ don't overwrite stuff if the exponent is zero
	+ recompile against KSP 1.4.1
* 2018-0309: 2.3.9 (Pellinor) for KSP 1.4.0
	+ fix interaction with stock ModulePartVariants
* 2018-0307: 2.3.8 (Pellinor) for KSP 1.4.0
	+ [TweakScale-v2.3.8.zip](https://github.com/pellinor0/TweakScale/files/1790799/TweakScale-v2.3.8.zip)
		- recompile for KSP 1.4
		- a few patches for new parts
	+ Known issues:
		- the new stock texture switch messes up attachment nodes on scaled parts
		- (first switching and then scaling seems to work)
* 2017-1013: 2.3.7 (Pellinor) for KSP 1.3.1
	+ recompile for KSP 1.3.1
	+ only complain about negative dry mass if the number is significant
* 2017-0527: 2.3.6 (Pellinor) for KSP 1.3.0
	+ recompile for KSP 1.3
	+ lots of player-submitted patches (thanks eberkain, mikeloeven, OliverPA77)
	+ set SRB thrust exponent to 3 (so both TWR and burntime are preserved now)
	+ added a few null checks
	+ scaling support for resource lists (for drills and converters)
	+ Fix: don't scale mass if the part has a MFT module
	+ TWEAKSCALEEXPONENTS should now affect not only the mentioned module but also derived modules
	+ (e.g. the exponent for ModuleRCS also applies for ModuleRCSFX)
* 2017-0123: 2.3.4 (Pellinor) for KSP 1.2.2
	+ fix exponent for stock ModuleGenerator
	+ found a way to write dryCost of the prefab early enough (fixes a cost issue with KIS)
	+ functions to ask the prefab about stats of scaled parts (meant for KIS)
* 2016-1217: 2.3.3 (Pellinor) for KSP 1.2.2
	+ recompile for KSP 1.2.2
	+ fix cost bug with fsfuelswitch
	+ (ignore resource cost because FSfuelSwitch takes it into account)
	+ added a bit of documentation
* 2016-1102: 2.3.2 (Pellinor) for KSP 1.2
	+ recompile for KSP1.2.1
	+ update patches for RLA-Stockalike, OPT
	+ exponents for ModuleRCSFX
	+ remove an obsolete exponent
	+ remove patches for relay antennas since scaling of their main function does not work
	+ (which is relaying signals when the antenna is part of an unloaded vessel)
* 2016-1021: 2.3.1 (Pellinor) for KSP 1.2
	+ exponent for groundHeightOffset (fixes landing gears clipping the runway at launch)
	+ fix for node positions reverting when cloning a part
	+ antennas: refresh range display
	+ antennas: tweak exponent (so that downscaled antennas are a bit less overpowered)
* 2016-1015: 2.3 (Pellinor) for KSP 1.2
	+ fix for wheel colliders
	+ fix wheelMotor torque and ec consumption
	+ CrewCapacity: has a configurable exponent now
	+ CrewCapacity: additional seats are not shown in the editor (stock limitation)
	+ scaling support for FloatCurve (for wheel torque)
	+ scaling support for and int values (for CrewCapacity)
	+ fix scaling of input/outputResources (new stock "resHandler", e.g. consumption of reaction wheels)
	+ move workaround for stock UI_ScaleEdit bug into the plugin
* 2016-0624: 2.2.13 (Pellinor) for KSP 1.1.3
	+ recompile for KSP 1.1.3
	+ fix for solar panels
	+ rewrite of chainScaling: propagate relative scaling factor to child parts
* 2016-0519: 2.2.12 (Pellinor) for KSP 1.1.2
	+ scaling of crew capacity (hardcoded to use the mass exponent for now)
	+ Fix patches scale crewed parts with an exponent of 2 for crew and mass
	+ (not realistic but fits better to stock balance than 3). In any case mass/kerbal is hardcoded to be preserved.
	+ scaling of the IVA overlay
	+ support for new firespitter biplane
	+ support for HLAirships module (thanks SpannerMonkey)
	+ some missing patches for B9 (thanks BlowFish)
	+ (did some reordering but still need to sort through the content)
	+ fixed a few patches
	+ reorganize some patches into their own folder
	+ (B9, Squad including nasa and spacePlanePlus)
	+ small optimisation: disable partModule in flight if not scaled
* 2016-0512: 2.2.11 (Pellinor) for KSP 1.1.2
	+ remove obsolete IFS exponents
	+ fix bug in partMessage for MFT
	+ expose API via the part message system
	+ fix for mirrored parts
	+ workaround for tweakable bug: extra scaleFactor 500% for the free scaletypes
	+ (so the range from 200-400% is usable again)
	+ don't interfere if other mods illegaly write part.mass. Print a warning in this case
	+ fix thrust for moduleRCS (new maxFuelFlow exponent like for moduleEngines)
* 2016-0505: 2.2.10 (Pellinor) for KSP 1.1.2
	+ basic wheel scaling: scaled wheels work but still behave strange. They roll, are pretty close to touching the ground, and are able to bounce.
	+ make sure the exponents are applied before notifying other mods through the API (needed for interaction with FAR)
	+ MFT support changed to TweakScale using their API (instead of the other way round)
	+ tweaked downscaled science parts to be a little more expensive
* 2016-0430: 2.2.9 (Pellinor) for KSP 1.1.2
	+ fix for drag cube scaling
	+ update right click menu after rescale
	+ recompile for 1.1.2
	+ cleanup for mass scaling
* 2016-0424: 2.2.7.2 (Pellinor) for KSP 1.1
	+ fix scaling of the root part
* 2016-0423: 2.2.7.1 (Pellinor) for KSP 1.1
	+ update for 1.1
	+ Workaround for the camera breaking (root part scaling is still broken)
	+ support for new stock parts
	+ shrinking science and probe cores makes them more expensive
	+ (only changed for stock so far)
	+ update for the OPT v1.8 test release
* 2016-0102: 2.2.6 (Pellinor) for KSP 1.0.5
	+ Support for NF-Construction
	+ update for NFT Octo-Girders
	+ fix for infinite loop between TweakScale and MFT
	+ fix for engineer's report mass display
* 2015-1109: 2.2.5 (Pellinor) for KSP 0.90
	+ recompile for KSP 1.0.5 (still using the old KspApiExtensions)
	+ update MM
	+ patches for the new parts
* 2015-1030: 2.2.4 (Pellinor) for KSP 0.90
	+ Fix for scaling of lists. This should fix the trouble with cost of FSFuelSwitch parts.
	+ Partial fix for editor mass display not updating
	+ new file Examples.cfg with frequently used custom patches
	+ Removed MM switch for scaleable crew pods
	+ update of NFT patches
	+ Fix scaling of resource lists
	+ support for a few missing stock parts
	+ partial support for a few other mods
	+ stock radiator support
	+ Scale ImpactRange for stock drill modules (this is what determines if the drill has ground contact or not)
	+ scale captureRange for claw (this should fix 3.75m claws not grappling)
	+ removed brakingTorque exponent (not needed and breaks stock tweakable)
* 2015-0626: 2.2.1 (Pellinor) for KSP 0.90
	+ update for KSP 1.0.4
	+ KSP 1.0 support: scaling of dragCubes
	+ exponent -0.5 for heatProduction
	+ support for HX parts from B9-Aerospace
	+ support for firespitter modules: FSEngine, FSPropellerTweak, FSAlternator
	+ remove support for KAS connector port so it stays stackable in KIS
	+ a few missing part patches
	+ update NF-Solar patches (some parts were renamed)
	+ catch exceptions on rescale
	+ survive duplicate part config
* 2015-0502: 2.1 (Pellinor) for KSP 0.90
	+ recompile for KSP 1.0.2
	+ new stock part
* 2015-0501: 2.0.1 (Pellinor) for KSP 0.90
	+ restored maxThrust exponent to fix the editor CoT display
	+ added patch for new KIS container
	+ survive mistyped scaleTypes
* 2015-0430: 2.0 (Pellinor) for KSP 0.90
	+ recompile for KSP 1.0
	+ new TWEAKSCALEBEHAVIOR nodes (engines, decouplers, boosters)
	+ scale DryCost with the mass exponent if there is no DryCost exponent defined
	+ fuel fraction of tanks is now preserved [breaking]
	+ move part patches into their own directory
	+ KIS support
	+ proper MM switches for mod exponents
	+ removed KSPI support (will be distributed with KSPI)
	+ scaleExponents for NF-electrical capacitors
	+ cleanup of stock scaleExponents
	+ support for the new stock modules
	+ support for the changed engine modules
* 2015-0420: 1.53 (Pellinor) for KSP 0.90
	+ download address for version file
	+ added missing RLA configs
	+ only touch part cost of the part is rescaled
	+ fix for repairing incomplete scaletypes
	+ support for stock decoupling modules
	+ OPT support
	+ remove RF scale exponents (RF does its own support)
* 2015-0310: 1.52.1 (Pellinor) for KSP 0.90
	+ No changelog provided
* 2015-0308: 1.52 (Pellinor) for KSP 0.90
	+ New Tweakable with more flexible intervals.
	+ All scaletypes use scaleFactors now, max/minScale is obsolete.
	+ Better handling of incomplete or inconsistent scaletype configs.
	+ Vessels now survive a change of defaultScale.
	+ less persistent data
* 2015-0226: 1.51.1 (Pellinor) for KSP 0.90
	+ added KSP-AVC support
	+ freescale slider Increments are now part of the scaletype config
	+ added stock mk3 configs
	+ auto- and chain scaling off by default (the hotkeys are leftCtrl-L and leftCtrl-K)
	+ auto- and chain scaling restricted to parts of the same scaletype
	+ Changed the 'stack' scaletype to free scaling
	+ Moved stock adapters to stack scaletype
	+ Changed surface scaletype to free scaling
	+ added an example discrete scaletype for documentation, because there is none left
	+ fixed error spam with regolith & KAS
	+ removed duplicate MM patch for IntakeRadialLong
	+ hopefully restricted the camera bug to scaled root parts
* 2015-0225: 1.51 (Pellinor) for KSP 0.90
	+ added KSP-AVC support
	+ added stock mk3 configs
	+ autoscaling
		- auto- and chain scaling off by default
		- auto- and chain scaling restricted to parts of the same scaletype
		- rewrote GetRelativeScaling based on the nodes of the prefab part
	+ scaletypes
		- freescale slider Increments are now part of the scaletype config
		- Change the 'stack' scaletype to free scaling
		- Move stock adapters to stack scaletype
		- Change surface scaletype to free scaling
		- added an example discrete scaletype for documentation, because there is none left in the default configs
		- if min/maxScale are missing in a free scaletype take min/max of the scaleFactors list
	+ fixes
		- fixed error spam with scalable parts from KAS containers
		- removed duplicate MM patch for IntakeRadialLong
		- hopefully restricted the camera bug to scaled root parts
* 2014-1224: 1.50 (Biotronic) for KSP 0.24
	+ Fixed erroneous placement of attach nodes when duplicating parts.
* 2014-1218: 1.49 (Biotronic) for KSP 0.24
	+ Now saving hotkey states correctly
	+ 'Free' scaletype actually free
	+ Fixed bug in OnStart
	+ First attempt at scaling offsets
* 2014-1216: 1.48 (Biotronic) for KSP 0.24
	+ Added .90 support! (screw Curse for not having that option yet)(Admin Edit: Curse added it! File updated to reflect the proper version)
	+ Cleaned up autoscale and chain scaling!
* 2014-1117: 1.47 (Biotronic) for KSP 0.24
	+ Removed [RealChute](http://forum.kerbalspaceprogram.com/threads/57988) support
	+ Fixed a bug where TweakScale would try to set erroneous values for some fields and properties, which notably affected [Infernal Robotics](http://forum.kerbalspaceprogram.com/threads/37707)
	+ Fixed a bug where cloned fuel tanks would have erroneous volumes.
* 2014-1116: 1.46 (Biotronic) for KSP 0.24
	+ Fixed an issue where features were incorrectly scaled upon loading a ship,
	+ Scaling a part now scales its children if they have the same size.
	+ Parts now automatically guess which size they should be.
* 2014-1115: 1.45 (Biotronic) for KSP 0.24
	+ New Features:
		- Now updating UI sliders for float values.
		- Better support for [KSP Interstellar](http://forum.kerbalspaceprogram.com/threads/43839).
		- Added support for [TweakableEverything](http://forum.kerbalspaceprogram.com/threads/64711).
		- Added support for [FireSpitter](http://forum.kerbalspaceprogram.com/threads/24551)'s FSFuelSwitch.
* 2014-1010: 1.44 (Biotronic) for KSP 0.24
	+ Version 1.44:
		- Updated for KSP 0.25
		- Added ability to not update certain properties when a specific partmodule is on the part.
	+ Thanks a lot to NathanKell, who did all the work on this release!
* 2014-0824: 1.43 (Biotronic) for KSP 0.24
	+ Version 1.43:
		- Added licence file (sorry, mods!)
		- No longer chokes on null particle emitters.
* 2014-0815: 1.41 (Biotronic) for KSP 0.24
	+ Version 1.41:
		- Fixed scaling of Part values in unnamed TWEAKSCALEEXPONENTS blocks.
* 2014-0813: 1.40 (Biotronic) for KSP 0.24
	+ Version 1.40:
		- Removed [Karbonite](http://forum.kerbalspaceprogram.com/threads/89401) cfg, since that project does its own TweakScale config.
* 2014-0812: 1.39 (Biotronic) for KSP 0.24
	+ Version 1.39:
		- Fixed cost calculation for non-full tanks.
* 2014-0812: 1.38 (Biotronic) for KSP 0.24
	+ Version 1.38:
		- Added scaling of FX.
		- Added support for [Banana for Scale](http://forum.kerbalspaceprogram.com/threads/89570).
		- Updated [Karbonite](http://forum.kerbalspaceprogram.com/threads/87335) support.
		- Fixed a bug where no scalingfactors available due to tech requirements would lead to unintended consequences.
* 2014-0805: 1.37 (Biotronic) for KSP 0.24
	+ Version 1.37:
		- Updated cost calculation.
* 2014-0804: 1.36 (Biotronic) for KSP 0.24
	+ Version 1.36:
		- Updated [Real Fuels](http://forum.kerbalspaceprogram.com/threads/64118) and [Modular Fuel Tanks](http://forum.kerbalspaceprogram.com/threads/64117) support.
		- Added [KSPX](http://forum.kerbalspaceprogram.com/threads/30472) support.
* 2014-0803: 1.35 (Biotronic) for KSP 0.24
	+ Corrected cost calculation.
	+ Updated to [KSPAPIExtensions 1.7.0](http://forum.kerbalspaceprogram.com/threads/81496)
* 2014-0802: 1.34 (Biotronic) for KSP 0.24
	+ Version 1.34:
		- Fixed a bug where repeated scaling led to inaccurate placing of child parts.
		- Added [Karbonite](http://forum.kerbalspaceprogram.com/threads/87335) support.
* 2014-0728: 1.33 (Biotronic) for KSP 0.24
	+ Updated RealFuels support for 7.1
* 2014-0725: 1.32 (Biotronic) for KSP 0.24
	+ Version 1.32:
		- Updated KSPAPIExtension for 0.24.2 support.
* 2014-0725: 1.31 (Biotronic) for KSP 0.24
	+ Fixed a bug where parts with defaultScale inaccessible due to tech requirements were incorrectly scaled.
* 2014-0725: 1.30 (Biotronic) for KSP 0.24
	+ Updated KSPAPIExtensions with 24.1 support.
	+ Re-enabled Real Fuels support.
	+ Added support for IPartCostModifier.
* 2014-0724: 1.29 (Biotronic) for KSP 0.24
	+ Fixed Modular Fuel Tanks support.
* 2014-0724: 1.28 (Biotronic) for KSP 0.24
	+ Fixed more cross-platform bugs.
	+ Added [Tantares Space Technologies](http://forum.kerbalspaceprogram.com/threads/80550).
* 2014-0723: 1.27 (Biotronic) for KSP 0.24
	+ Fixed a bug on non-Windows platforms.
* 2014-0723: 1.26 (Biotronic) for KSP 0.24
	+ Version 1.26:
		- Fixed typo in DefaultScales.cfg that caused som parts to baloon ridiculously.
		- Added support for updated NFT and KW Rocketry
* 2014-0723: 1.25 (Biotronic) for KSP 0.24
	+ Version 1.25:
		- Modular Fuel Tanks](http://forum.kerbalspaceprogram.com/threads/64117) yet again supported! (Still waiting for Real Fuels)
		- Refactored IRescalable system to be easier for mod authors.
		- Fixed a bug where one field could have multiple exponents, and thus be rescaled multiple times.
* 2014-0721: 1.23 (Biotronic) for KSP 0.24
	+ Version 1.23:
		- Duplicate TweakScale dlls no longer interfere.
* 2014-0720: 1.22 (Biotronic) for KSP 0.24
	+ Version 1.22
		- Fixed tanks that magically refill.
		- Fixed technology requirements are too slow.
		- Updated KSPAPIExtensions to make TweakScale play nicely with other mods.
* 2014-0718: 1.21 (Biotronic) for KSP 0.24
	+ Version 1.21:
		- Updated for 0.24
		- Now supports global, per-part, and per-scaletype scaling of features (like mass, buoyancy, thrust, etc)
* 2014-0613: 1.20 (Biotronic) for KSP 0.23.5
	+ Version 1.20:
		- New algorithm for rescaling attach nodes. Tell me what you think!
		- Added [Deadly Reentry Continued](http://forum.kerbalspaceprogram.com/threads/54954) and [Large Structural/Station Components](http://forum.kerbalspaceprogram.com/threads/34664).
* 2014-0606: 1.19 (Biotronic) for KSP 0.23.5
	+ Version 1.19:
		- Added support for tech requirements for non-freescale parts.
* 2014-0606: 1.18 (Biotronic) for KSP 0.23.5
	+ Version 1.18
		- Factored out Real Fuels and Modular Fuel Tanks support to separate dlls.
* 2014-0603: 1.17 (Biotronic) for KSP 0.23.5
	+ Version 1.17:
		- Fixed bug where attachment nodes were incorrectly scaled after reloading. This time with more fix!
		- Added support for [Near Future Technologies](http://forum.kerbalspaceprogram.com/threads/52042).
* 2014-0603: 1.16 (Biotronic) for KSP 0.23.5
	+ Version 1.16:
		- Fixed bug where attachment nodes were incorrectly scaled after reloading.
* 2014-0603: 1.15 (Biotronic) for KSP 0.23.5
	+ Version 1.15:
		- Finally squished the bug where crafts wouldn't load correctly. This bug is present in 1.13 and 1.14, and affects certain parts from Spaceplane+, MechJeb, and KAX.
* 2014-0603: 1.14 (Biotronic) for KSP 0.23.5
	+ Version 1.14:
		- Fixed a bug where nodes with the same name were moved to the same location regardless of correct location. (Only observed with KW fairing bases, but there could be others)
* 2014-0602: 1.13 (Biotronic) for KSP 0.23.5
	+ Version 1.13:
		- Added support for [MechJeb](http://forum.kerbalspaceprogram.com/threads/12384), [Kerbal Aircraft eXpanion](http://forum.kerbalspaceprogram.com/threads/76668), [Spaceplane+](http://forum.kerbalspaceprogram.com/threads/80796), [Stack eXTensions](http://forum.kerbalspaceprogram.com/threads/79542), [Kerbal Attachment System](http://forum.kerbalspaceprogram.com/threads/53134), [Lack Luster Labs](http://forum.kerbalspaceprogram.com/threads/24906), [Firespitter](http://forum.kerbalspaceprogram.com/threads/24551), [Taverio's Pizza and Aerospace](http://forum.kerbalspaceprogram.com/threads/15348), [Better RoveMates](http://forum.kerbalspaceprogram.com/threads/75873), and [Sum Dum Heavy Industries Service Module System](http://forum.kerbalspaceprogram.com/threads/48357).
		- Fixed a bug where Modular Fuel Tanks were not correctly updated.
* 2014-0602: 1.12 (Biotronic) for KSP 0.23.5
	+ Version 1.12:
		- Added support for [КОСМОС](http://forum.kerbalspaceprogram.com/threads/24970).
		- No longer scaling heatDissipation, which I was informed was a mistake.
* 2014-0601: 1.11 (Biotronic) for KSP 0.23.5
	+ 1.11:
		- Removed silly requirement of 'name = *' for updating all elements of a list.
		- Added .cfg controlled scaling of Part fields.
* 2014-0601: 1.10 (Biotronic) for KSP 0.23.5
	+ 1.10:
		- Added support for nested fields.
* 2014-0531: 1.9 (Biotronic) for KSP 0.23.5
	+ Version 1.9
		- Fixed bugs in 1.8 where duplication of parts caused incorrect scaling.
* 2014-0530: 1.8 (Biotronic) for KSP 0.23.5
	+ Version 1.8:
		- Fixed a bug where rescaleFactor caused incorrect scaling.
		- Added (some) support for [Kethane](http://forum.kerbalspaceprogram.com/threads/23979-Kethane-Pack-0-8-5-Find-it-mine-it-burn-it!-0-23-5-\(ARM\)-compatibility-update) parts.
* 2014-0522: 1.7 (Biotronic) for KSP 0.23.5
	+ No changelog provided
* 2014-0522: 1.6 (Biotronic) for KSP 0.23.5
	+ Version 1.6:
		- Fixed a problem where parts were scaled back to their default scale after loading, duplicating and changing scenes.
* 2014-0520: 1.5.0.1 (Biotronic) for KSP 0.23.5
	+ Version 1.5.0.1:
		- Fixed a bug in 1.5
		- Changed location of KSPAPIExtensions.dll
* 2014-0520: 1.5 (Biotronic) for KSP 0.23.5
	+ Version 1.5
		- Changed from hardcoded updaters to a system using cfg files.
* 2014-0520: 1.4 (Biotronic) for KSP 0.23.5
	+ Version 1.4
		- Fixed incompatibilities with GoodspeedTweakScale
* 2014-0519: 1.3 (Biotronic) for KSP 0.23.5
	+ Version 1.3
		- Fixed a bug where parts would get rescaled to stupid sizes after loading.
		- Breaks compatibility with old version of the plugin (pre-1.0) and GoodspeedTweakScale. :(
* 2014-0518: 1.2 (Biotronic) for KSP 0.23.5
	+ Version 1.2 (2014-05-18, 22:00 UTC):
		- Fixed default scale for freeScale parts.
		- Fixed node sizes, which could get absolutely redonkulous. Probably not perfect now either.
		- B9, Talisar Cargo Transportation Solutions, and NASA Module Manager configs.
		- Now does scaling at onload, removing the problem where the rockets gets embedded in the ground and forcibly eject at launch.
		- Fixed a silly bug in surface scale type.
* 2014-0516: 1.1 (Biotronic) for KSP 0.23.5
	+ Version 1.1:
		- Added scaling support for [B9 Aerospace ](http://forum.kerbalspaceprogram.com/threads/25241)and [Talisar's Cargo Transportation Solutions](http://forum.kerbalspaceprogram.com/threads/77505)
		- Will now correctly load (some) save games using an older version of the plugin.
* 2014-0516: 1.0 (Biotronic) for KSP 0.23.5
	+ No changelog provided

- - -
	WiP : Work In Progress
	RiP : Research In Progress

