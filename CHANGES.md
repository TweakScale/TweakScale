# TweakScale :: Changes

* 2024-1117: 2.5.0.63 **BETA** (Lisias) for KSP >= 1.3
	+ Prevents living crafts on the savegame to lose the ScaleFactor!!
	+ Fixes some embarrassing screw-ups
	+ Survives hostile forks screwing up `Scale_Redist.dll`
	+ Enables rich text on dialog boxes
	+ Incepts the `TweakScale.WatchDog`
	+ Reworks scale support for KSP (1.4.x, 1.5.x and 1.9.x at this moment)
	+ Updates MMWD to 1.1.2.1
	+ Syncing fixes from `legacy`
		- 2.4.8.8
			- Fixes (**AGAIN**) a regression on handling attachment nodes, thanks Kraken affecting **only** KSP 1.4.3 (and almost surely 1.4.0 to 1.4.2, but I didn't bored to check).
				- Special attention and caring were taken to **do not** change anything on support for any other KSP.
			- **Finally** fixes an embarrassing bug that where `double`s were being squashed to `float`s. I do not expect any change the default scalings, but people using some dramatic customisations on really big and really small exponents should see some improvements.
			- Removes some (TS unrelated) sanity checks when a CKAN managed installment is detected
				- TweakScale is, from now on, blindly trusting CKAN on keeping the running environment sane, only yelling when it's directly affected.
		- 2.4.8.6
			- Due recently realised changes on the way [CKAN handles alternate downloads](https://forum.kerbalspaceprogram.com/topic/225966-psa-update-your-ckan-clients-to-134/?do=findComment&comment=4421703), some safeties were implemented to alert the user if by some reason it was installed a non CKAN approved package on a CKAN managed installment.
		- 2.4.8.5
			- Fixes a long standing mishandling on the Life Cycle of the `SingletonBehaviour`'s extended classes.
		- 2.4.8.4
			- Somewhat better error messages, in a (futile, probably) attempt to prevent this [kind of crap](https://www.reddit.com/r/KerbalAcademy/comments/1ejaf9b/houstonerror_contradiction/).
		- 2.4.8.3
			- Fixes a (yet another :P) major screwup of mine, this one while handling systems without `ModuleManagerWatchDog` installed - exactly the situation the `TweakScale.WatchDog` was born to handle on 2.4.8.0...
				- Yep, sometimes I'm just overloaded by Real Life©...
		- 2.4.8.2 (Lisias) for KSP >= 1.3
			- Detected and fixed a borderline situation in which the `IPartCostModifier` from the TweakScale's `PartModule` was being called **before** the `OnLoad` while merging a craft with scaled cockpit - unsure if the cockpit made any difference, but whatever.
			- I'm shooting first and making questions later - I will left a proper diagnose to be tackled down on the Beta 2.5.
		- 2.4.7.6
			- Found an idiocy of mine while trying to prevent a hypothetical problem - and ended up creating a concrete one instead.
			- If you know a priest in need to a job, [we are hiring](https://github.com/net-lisias-ksp/KSP-Recall/issues/61#issuecomment-2014430999)... :P
		- 2.4.7.5
			- An insidious bug screwing parts that use `techRequired` was fixed.
			- Thanks to [Turbo Ben](https://forum.kerbalspaceprogram.com/profile/193979-turbo-ben/) for the [work into zeroing](https://forum.kerbalspaceprogram.com/topic/179030-ksp-130-tweakscale-under-lisias-management-2474-2023-1007/?do=findComment&comment=4366095) into the exact root cause of the problem!
	+ Related Issues:
		- [#339](https://github.com/TweakScale/TweakScale/issues/339) Prevent non CKAN safe binaries from being used on CKAN managed installations.
		- [#336](https://github.com/TweakScale/TweakScale/issues/336) Finally Diagnosed a Memory Leak on `EditorHelper`
		- [#325](https://github.com/TweakScale/TweakScale/issues/325) Cope with https://github.com/net-lisias-ksp/KSP-Recall/issues/73
		- [#323](https://github.com/TweakScale/TweakScale/issues/323) Auto-Scale is screwed since 2.4.7.0
		- [#312](https://github.com/TweakScale/TweakScale/issues/312) Write an internal Self Check Mechanism
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant
		- [#283](https://github.com/TweakScale/TweakScale/issues/283) New screw up from KSP 1.11.0 Editor was revealed by the 2.4.6.20 release
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant.
* 2024-0213: 2.5.0.62 **BETA** (Lisias) for KSP >= 1.3
	+ An insidious bug screwing parts that use `techRequired` was fixed.
		- Thanks to [Turbo Ben](https://forum.kerbalspaceprogram.com/profile/193979-turbo-ben/) for the [work into zeroing](https://forum.kerbalspaceprogram.com/topic/179030-ksp-130-tweakscale-under-lisias-management-2474-2023-1007/?do=findComment&comment=4366095) into the exact root cause of the problem!
	+ Small typos fixed.
	+ Fixes [#307](https://github.com/TweakScale/TweakScale/issues/307). **AGAIN**, and restores the [#261](https://github.com/TweakScale/TweakScale/issues/261) that got broken.
	+ Reworks the support DLLs.
		- Reinstates the 1.8.x one;
		- Merges it with the 1.9.x, that became redundant.
	+ Closes Issues:
		- [#319](https://github.com/TweakScale/TweakScale/issues/319) Reinstate dedicated DLL support for KSP 1.8.x
		- [#307](https://github.com/TweakScale/TweakScale/issues/309) Create an option to fix the part's cost when calculating the DryCost.
* 2023-1106: 2.5.0.61 **BETA** (Lisias) for KSP >= 1.3
	+ A serious regression was detected on 2.4.7.3. The code intended to fix [Issue 307](https://github.com/TweakScale/TweakScale/issues/307) triggered ***Yet Another Bug on Editor™***, and had to be removed. This, unfortunately, resurrects #307. :(
	+ Refactoring: Simplifying internal interfaces
	+ Small enhancements on logging
	+ Fixing the Show Stopper handling (that wasn't stopping the show...)
	+ Closes Issues:
		- [#309](https://github.com/TweakScale/TweakScale/issues/309) Create an option to fix the part's cost when calculating the DryCost.
	+ ReOpen Issues:
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant  
* 2023-0803: 2.5.0.60 **BETA** (Lisias) for KSP >= 1.3
	+ Adds a hotfix for mispatchings adding `FSFuelSwitch` and `B9PS` on the same part on the `Extras` folder.
	+ Remove deprecated KSPe calls.
	+ TweakScale's runtime configuration file is now on the `<KSP_ROOT>PluginData` directory, and not on `GameData` anymore.
		- Not more polluting cloud backups with transitory data. 
	+ Cumulative catch up from the `legacy` branch.
	+ For the record, some work for [#297](https://github.com/TweakScale/TweakScale/issues/297) was done, but the thing ended up being postponed. Whoever, probing the solution's scaffolding on the field will make my task easier when I finally tackle this crap down. 
	+ Updates MMWD to 1.1.1.1
		- User will need to manually remove the `<KSP_ROOT>/GameData/666_ModuleManagerWatchDog.dll` file due a major screwup of mine on handling file updates on Windows.
		- Hopefully this will not happen again.
	+ Backport the Unity's `Update` Life Cycle fix from [Aviation Lights #4](https://github.com/net-lisias-ksp/AviationLights/issues/4)
		- Hopefully preventing some shitstorm on users running KSP on Hybrid CPUs (P-Cores, E-Cores, that crap).
	+ Closes Issues:
		- [#308](https://github.com/TweakScale/TweakScale/issues/308) Insidious NRE on changing scenes 
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant
	+ Related Issues:
		- [Aviation Lights #4](https://github.com/net-lisias-ksp/AviationLights/issues/4) Aviation Lights *may* be involved on a weird bug report on Forum
* 2023-0324: 2.5.0.59 **BETA** (Lisias) for KSP >= 1.3
	+ Updates the Companions' definition file to mark KIS (and some others) deprecated add'ons as... deprecated!
		- Thanks for the [heads up](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-130-tweakscale-under-lisias-management-24625-2023-0304/&do=findComment&comment=4264686), [@ngx](https://forum.kerbalspaceprogram.com/index.php?/profile/184821-ngx/)!
	+ New checking to detect deprecated Companions forgotten on the `GameData`
* 2023-0313: 2.5.0.58 **BETA** (Lisias) for KSP >= 1.3
	+ I finally diagnosed and fixed a pretty stupid mistake on new `TweakScale.Updater.IVA`
	+ A Checker for the need specific TweakScale Companions was added.
		- Your `GameData` is checked for 3rd party add'ons currently supported by a Companion and a Dialog Box is displayed listing the ones you need to install.  
	+ Implements a missing use case when scaling IVAs and switching vessels. 
	+ Removes deprecated calls to `KSPe` from the codebase
		- (shame on me, these things are deprecated for months!)
	+ More robust ScaleType Migration Code.
		- Indirectly related to issue [#285](https://github.com/TweakScale/TweakScale/issues/285), as the Beta branch **does not** presented the misbehaviour.
	+ Catches up with the main stream
		- [#268](https://github.com/TweakScale/TweakScale/issues/268) Misbehaviour related to Taking Data from a Pod when it's scaled.
		- [#261](https://github.com/TweakScale/TweakScale/issues/261) Misbehaviour (again) while scaling parts with VARIANT
		- [#252](https://github.com/TweakScale/TweakScale/issues/252) Scale the Buoyance so the scaled parts has a similar floating capabilities as the original.
		- [#246](https://github.com/TweakScale/TweakScale/issues/246) New bug related to IVA and Cameras when TweakScale is installed.
		- [#238](https://github.com/TweakScale/TweakScale/issues/238) TweakScale is failing to consistently resize the Attachment Node's sizes.
	+ Closes Issues:
		- [#290](https://github.com/TweakScale/TweakScale/issues/290) Regression on handling the TweakScaleRogueDuplicate patching problem.
		- [#289](https://github.com/TweakScale/TweakScale/issues/289) Yet another unexpected Misbehaviour, this time on `PartModule.OnSave`.
		- [#287](https://github.com/TweakScale/TweakScale/issues/286) Misunderstanding (?) on how `PartModule.OnLoad(ConfigNode)` really works.
		- [#286](https://github.com/TweakScale/TweakScale/issues/286) `PartModule.OnLoad` **is not** called with `node` as null on Flight Scene!
		- [#280](https://github.com/TweakScale/TweakScale/issues/280) **UNDO** the :FOR[TWEAKSCALE] on Default TweakScale Patching...
		- [#279](https://github.com/TweakScale/TweakScale/issues/279) Über refactoring on `IRescalable`
		- [#276](https://github.com/TweakScale/TweakScale/issues/276) Update Scale_Redist Version to 1.2
		- [#195](https://github.com/TweakScale/TweakScale/issues/195) Remove the stub CFG files for deprecated patches
* 2023-0304: 2.5.0.57 **BETA** (Lisias) for KSP >= 1.3
	+ ***DITCHED***
* 2023-0304: 2.5.0.56 **BETA** (Lisias) for KSP >= 1.3
	+ ***WITHDRAWN*** due a lame mistake on the configuration files. 
* 2023-0212: 2.5.0.55 **BETA** (Lisias) for KSP >= 1.3 
	+ ***DITCHED***
* 2023-0212: 2.5.0.54 **BETA** (Lisias) for KSP >= 1.3 
	+ **WITHDRAWN** as a new release were made in less than 24 hours. 
* 2023-0207: 2.5.0.53 **BETA** (Lisias) for KSP >= 1.3 
	+ ***DITCHED***
* 2023-0204: 2.5.0.52 **BETA** (Lisias) for KSP >= 1.3 
	+ ***DITCHED***
* 2023-0202: 2.5.0.51 **BETA** (Lisias) for KSP >= 1.3 
	+ ***WITHDRAWN***
* 2023-0126: 2.5.0.50 **BETA** (Lisias) for KSP >= 1.3 
	+ ***WITHDRAWN***
* 2022-0716: 2.5.0.49 **BETA** (Lisias) for KSP >= 1.3 
	+ Mitigates an undesired collateral effect from the symlink handling on C#'s runtime on MacOS and Linux.
		- Updates KSPe.Light to 2.4.1.21
		- Preload the TweakScale's toolbar Icons on the Space Center scene, where mysteriously they are loaded without nasty delays.
	+ Closes or Rework Issues:
		- [#187](https://github.com/TweakScale/TweakScale/issues/187) Check and implement all Modules left behind up to 1.3.1
		- [#184](https://github.com/TweakScale/TweakScale/issues/184) Scale some unsupported parts on EXPERIMENTAL status
		- [#46](https://github.com/TweakScale/TweakScale/issues/46) Feasibility Studies for Serenity 
* 2022-0710: 2.5.0.48 **BETA** (Lisias) for KSP >= 1.3 
	+ Bug fixes and improvements on the features implemented on the last pre-release.
	+ Adds new FIXes and CHECKs for known problems
		- Blue Dog Design Bureau
		- Tantares
		- Configurable Containers 
	+ Closes Issues:
		- [#260](https://github.com/TweakScale/TweakScale/issues/260) Preventing Configurable Containers from being used without proper Companion Support
		- [#258](https://github.com/TweakScale/TweakScale/issues/258) TASK: Code a withdraw list of Parts, patchable by ModuleManager
		- [#34](https://github.com/TweakScale/TweakScale/issues/34) Sanity Check: duplicated properties Support page
		- [#31](https://github.com/TweakScale/TweakScale/issues/31) Preventing being ran over by other mods
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
		- [#256](https://github.com/TweakScale/TweakScale/issues/256) Move the DryCostWriter (and Sanity Checkes) out of the Main Menu startup
		- [#255](https://github.com/TweakScale/TweakScale/issues/255) Special Deployment for Curseforge due Scale_Redist
* 2022-0620: 2.5.0.45 (Lisias) for KSP >= 1.3
	+ **HUGE** refactoring on the Sanity Checks, "exporting" the checks into a dedicated DLL (`Scale_Sanitizer.dll`) at the same time allowing 3rd parties to include their own checks on it.
	+ Some extra precautions on `EditorHelper`
	+ Some less (useless) precautions on the Variant Support.
	+ Closes Issues:
		- [#254](https://github.com/TweakScale/TweakScale/issues/254) Extract the Sanity Checks in their own DLL
* 2022-0415: 2.5.0.44 (Lisias) for KSP >= 1.3
	+ Well, it's a bit embarrassing but I finally detected and fixed a regression on legacy support I inadvertently did when I removed the kludges I made on TweakScale when KSP 1.9.0 was launched.
		- Long history made short, when I added that kludge, I broke support for [1.4.4 <= KSP <= 1.7.3] and then added another kludge to counter act the first kludge.
		- Once I removed the 1.9.x kludge and moved it as a proper work around into KSP-Recall, I forgot to remove the second kludge...
		- As a side effect, less Scaling Engines are needed now, so we have one less DLL to worry about.
	+ Closes Issues:
		- [#249](https://github.com/TweakScale/TweakScale/issues/249) Reorganize the Scaling Engines
* 2022-0415: 2.5.0.43 (Lisias) for KSP >= 1.3
	+ Fixes a subtile and insidious problem [reported by BTAxis](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-130/&do=findComment&comment=4117283). Thanks, dude!
	+ Closes Issues:
		- [#244](https://github.com/TweakScale/TweakScale/issues/244) Reactivating TweakScale is disabling the scaling feature for good
* 2022-0326: 2.5.0.42 (Lisias) for KSP >= 1.3
	+ Removes an (now) unnecessary "gambiarra", as KSP-Recall is now fixing the mess on KSP >= 1.9 editor.
		- A small (and 3rd party safe) fraction of it remains to cover what may be a missing use-case on KSP-Recall, or a fishy code on TweakScale itself. 
	+ Implements a new Sanity Check against a worrisome situation where a Part is given to TweakScale **without the partInfo**!!!
		- I don't have the slightest idea about how in hell this can happen, but I got confirmation of this problem from reliable sources.
	+ **Finally** implementing a full-fledged "TweakScale Upgrade Pipeline", allowing run-time, on-the-fly conversions between ScaleTypes and DefaultScales.
		- No more worries about installing or updating Add'Ons that changes the TweakScale patches.
		- [All Tweak!!!](https://forum.kerbalspaceprogram.com/index.php?/topic/182700-111x-all-tweak-07-23rdoctober2019/) users, this one is dedicated to you! :) 
	+ Closes Issues:
		- [#237](https://github.com/TweakScale/TweakScale/issues/237) New Sanity Check: parts without partInfo!!!
		- [#236](https://github.com/TweakScale/TweakScale/issues/236) Extent the Scale migration feature to allow switching ScaleTypes and DefaultScales!
		- [#218](https://github.com/TweakScale/TweakScale/issues/218) Implement GetInfo on TweakScale's Part Module
* 2022-0322: 2.5.0.41 (Lisias) for KSP >= 1.3
	+ ***DITCHED*** because I screwed the pooch on a merge **after testing the thing**, and ended up publishing a crappy release. :(
		- I need some rest from dayjob. :/ 
* 2021-1130: 2.5.0.40 (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#237](https://github.com/TweakScale/TweakScale/issues/237) New Sanity Check: parts without partInfo!!!
		- [#236](https://github.com/TweakScale/TweakScale/issues/236) Extent the Scale migration feature to allow switching ScaleTypes and DefaultScales!
		- [#218](https://github.com/TweakScale/TweakScale/issues/218) Implement GetInfo on TweakScale's Part Module
* 2021-1120: 2.5.0.39 (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#211](https://github.com/TweakScale/TweakScale/issues/211) Mitigate the HotKeys being hijacked by 3rd-parties
		- [#209](https://github.com/TweakScale/TweakScale/issues/209) TweakScale not installed on wrong directory
		- [#197](https://github.com/TweakScale/TweakScale/issues/197) Flags (the parts attachable) are losing the attaching points when its parent is duplicated
		- [#167](https://github.com/TweakScale/TweakScale/issues/167) Mirror Symmetry is displacing some (all?) parts when scaled
		- [#139](https://github.com/TweakScale/TweakScale/issues/139) Scaling Part with Variants that change attachment nodes is not working as expecnted
* 2021-0927: 2.5.0.38 (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#208](https://github.com/TweakScale/TweakScale/issues/208) Chain Scaling Parts with variants are borking when the parent part is "inverted"
		- [#207](https://github.com/TweakScale/TweakScale/issues/207) The Upgrade Pipeline thingy (or something else?) is playing havoc with TweakScale 
		- [#175](https://github.com/TweakScale/TweakScale/issues/175) Wrong displacement of the attached part placed inverted when scaling its parent
		- [#163](https://github.com/TweakScale/TweakScale/issues/163) Radial Symmetry (when using variants) are misplacing parts.
		- [#131](https://github.com/TweakScale/TweakScale/issues/131) Chain Scaling parts is playing havoc with the Radial Attachment Positions. 
		- [#36](https://github.com/TweakScale/TweakScale/issues/36) [TweakScale Warning] Exception during ModulePartVariants interaction
* 2021-0927: 2.5.0.37 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Maintenance release
		- Declares the Ground Anchor as Experimental		- Updates the code base to the latest KSPe release (2.4.0.1 at this moment)
* 2021-0922: 2.5.0.36 Beta (Lisias) for KSP >= 1.3.1
	+ Recompiled against the new KSPe 2.4.0.0
		- [Update before using](https://github.com/net-lisias-ksp/KSPAPIExtensions/releases/tag/RELEASE%2F2.4.0.0), it's a hard dependency on the new Version. 
* 2021-0913: 2.5.0.35 Beta (Lisias) for KSP >= 1.3.1
	+ Fixes (again) the Decluttering thingy. 
	+ Closes Issues:
		- [#201](https://github.com/TweakScale/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
* 2021-0912: 2.5.0.34 Beta (Lisias) for KSP >= 1.3.1
	+ Catches up all fixes already published on mainstream.
	+ Implements support for **ALL** KSP versions downto 1.3.1 !!! #HURRAY!!
		- Specialised DLLs loaded under demand, thanks KSPe!
		- 1.2.2, however, will need more work. Unsure if it worths it.
	+ Better compatibility with 3rd Party Add'Ons when Decluttering. 
	+ Closes Issues:
		- [#201](https://github.com/TweakScale/TweakScale/issues/201) The "Decluttering" Feature from 2.5.4.4 is breaking KCT
		- [#198](https://github.com/TweakScale/TweakScale/issues/198) Breakdown Scaling support for each major KSP version in specialised DLLs
* 2021-0912: 2.5.0.33 Beta (Lisias) for KSP >= 1.3.1
	+ Ditching this version as a new release was made on the same day. 
* 2021-0912: 2.5.0.33 Beta (Lisias) for KSP >= 1.3.1
	+ Catches up all fixes already published on mainstream.
	+ Implements support for **ALL** KSP versions downto 1.3.1 !!! #HURRAY!!
		- Specialised DLLs loaded under demand, thanks KSPe!
		- 1.2.2, however, will need more work. Unsure if it worths it.
	+ Closes Issues:
		- [#198](https://github.com/TweakScale/TweakScale/issues/198) Breakdown Scaling support for each major KSP version in specialised DLLs
* 2021-0410: 2.5.0.32 Beta (Lisias) for KSP >= 1.4.4
	+ Declutters craft files, preventing TweakScale MODULE nodes from begin written on it when the part is not scaled or it's deactivated.
		- Now your unscaled crafts can be shared on KerbalX *et all* without being tagged as using TweakScale.
		- And you can play Challenges where TweakScale is not allowed without deinstalling TweakScale - or creating a new installment just because of it.
	+ Closes Issues:
		- [#85](https://github.com/TweakScale/TweakScale/issues/85) Clean TweakScale's Module from unchanged parts on save 
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
* 2020-1125: 2.5.0.29 Beta (Lisias) for KSP >= 1.4.4
	+ I missed a detail, I didn't accounted for the `attPos` atribute (moving an attached part with the "Move" tool). Fixed.
* 2020-1123: 2.5.0.28 Beta (Lisias) for KSP >= 1.4.4
	+ Scaled parts with Variants now correctly translates the attached part when applying variants #HURRAY
		- As long the part has no symmetry, when things get completely screwed up...
		- See [this comment](https://github.com/TweakScale/TweakScale/issues/42#issuecomment-732321477) on [Issue #42](https://github.com/TweakScale/TweakScale/issues/42) for details.
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
			- See [this comment](https://github.com/TweakScale/TweakScale/issues/42#issuecomment-726428889) on [Issue #42](https://github.com/TweakScale/TweakScale/issues/42) for details.
		- Detaching and reattaching the Mastodon work arounds the problem on the engine.
		- Detaching and reattaching the parts attached to a scaled tube work arounds the problem with the tubes.
		- Things on KSP 1.9 are yet more problematic. [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall/issues/9) will tackle this down.
	+ This is a beta release, merging the latest fixes and aiming to test solutions and check stability issues related to the following issues:
		- 2.5.0.23
			- [#142](https://github.com/TweakScale/TweakScale/issues/142) Add ignoreResourcesForCost to the TweakScale module attributes
			- [#87](https://github.com/TweakScale/TweakScale/issues/87) Wrong default scales (partial)
		- 2.5.0.21
			- [#138](https://github.com/TweakScale/TweakScale/issues/138) Expand TweakScaleCompanion_NF#2 (suppress warnings due empty configs)
			- [#13](https://github.com/TweakScale/TweakScale/issues/13) Properly support ModulePartVariants #HURRAY
		- 2.5.0.20
			- [#137](https://github.com/TweakScale/TweakScale/issues/137) Prevent havoc from patches that changed the scaling on the prefab.
			- [#136](https://github.com/TweakScale/TweakScale/issues/136) Config getting skipped during creation. 
		- 2.5.0.16
			- [#125](https://github.com/TweakScale/TweakScale/issues/125) The new deactivation process (due sanity checks) is preventing parts with TweakScale deactivated to be attached 
		- 2.5.0.15
			- [#124](https://github.com/TweakScale/TweakScale/issues/124) Script error (TweakScale): OnDestroy() can not take parameters.
			- [#119](https://github.com/TweakScale/TweakScale/issues/119) Remove TweakScale's handler from the onEditorShipModified when the part is Destroyed
		- 2.5.0.14
			- [#115](https://github.com/TweakScale/TweakScale/issues/115) KSP 1.10 Support Status
			- [#114](https://github.com/TweakScale/TweakScale/issues/114) KSP 1.8 (and 1.9) rendered the Sanity Checks useless.
		- 2.5.0.13
			- [TSC_FS#1](https://github.com/TweakScale/TweakScaleCompantion_FS/issues/1) Weird issue with SXT parts using `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/TweakScale/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
			- [TSC_FS#2](https://github.com/TweakScale/TweakScaleCompantion_FS/issues/2) Properly Support `FSBuoyancy`.
		- 2.5.0.12
			- [#110](https://github.com/TweakScale/TweakScale/issues/110) Revert to Vehicle Assembly and Loading Craft are mangling the part attachments.
		- 2.5.0.11
			- [#106](https://github.com/TweakScale/TweakScale/issues/106) Deprecate everything and the kitchen's sink (but Stock and DLC)
			- [#95](https://github.com/TweakScale/TweakScale/issues/95) Give some care to the Warnings system (rework) 
		- 2.5.0.10
			- [#103](https://github.com/TweakScale/TweakScale/issues/103) Implement KSP Recall :: Attachment support
			- [#7](https://github.com/TweakScale/TweakScale/issues/7) Update some patches to KSP 1.5 and 1.6 (rework)
			- [#35](https://github.com/TweakScale/TweakScale/issues/35) Check for new parts on KSP 1.7 (rework)
			- [#73](https://github.com/TweakScale/TweakScale/issues/73) Support the new parts for KSP 1.8 (rework)
			- [#95](https://github.com/TweakScale/TweakScale/issues/95) Give some care to the Warnings system
			- [#101](https://github.com/TweakScale/TweakScale/issues/101) Add Support for KSP 1.9
		- 2.5.0.9
			- KSP 1.9 Compliance
				- Delegated to [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
			- [#98](https://github.com/TweakScale/TweakScale/issues/98) Added support for [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall).
		- 2.5.0.8
			- KSP 1.8 Compliance
				- Compatibility check updated
				- Changing `Scale_Redist.dll` deployment model. See [KNOWN_ISSUES](https://github.com/TweakScale/TweakScale/blob/master/KNOWN_ISSUES.md) for details.
			- [#46](https://github.com/TweakScale/TweakScale/issues/46) Feasibility Studies for Serenity
				- Added scaling to Proppelers 
			- [#73](https://github.com/TweakScale/TweakScale/issues/73) Support the new parts for KSP 1.8 
			- [#74](https://github.com/TweakScale/TweakScale/issues/74) Check (and fix if needed) a possible misbehaviour on handling Events on Scale
		- 2.5.0.7
			- [#21](https://github.com/TweakScale/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
				- Some entries for NFT were missing the fix 
			- [#26](https://github.com/TweakScale/TweakScale/issues/26) Document the patches
			- [#69](https://github.com/TweakScale/TweakScale/issues/69) Act on deprecated or misplaced patches
			- [#76](https://github.com/TweakScale/TweakScale/issues/76) Prevent KSP from running if TweakScale is installed on the wrong place!
		- 2.5.0.6
			- [#71](https://github.com/TweakScale/TweakScale/issues/71) Check for typos on the _V2 parts from patches for Squad's revamped parts
			- [#30](https://github.com/TweakScale/TweakScale/issues/30) Prevent incorrectly initialized Modules to be used (reopened)
		- 2.5.0.4
			- [#65](https://github.com/TweakScale/TweakScale/issues/65) Support for new Nertea's Cryo Engines
		- 2.5.0.3
			- [#47](https://github.com/TweakScale/TweakScale/issues/47) Count failed Sanity Checks as a potential problem. Warn user
			- [#48](https://github.com/TweakScale/TweakScale/issues/48) Backport the Heterodox Logging system into Orthodox (using KSPe.Light
			- [#49](https://github.com/TweakScale/TweakScale/issues/49) Check the Default patches for problems due wildcard!
			- [#50](https://github.com/TweakScale/TweakScale/issues/50) Check the patches for currently supported Add'Ons
			- [#58](https://github.com/TweakScale/TweakScale/issues/58) Mk4 System Patch (addendum)
		- 2.5.0.2
			- [#51](https://github.com/TweakScale/TweakScale/issues/51) Implement a "Cancel" button when Actions are given to MessageBox
			- [#54](https://github.com/TweakScale/TweakScale/issues/54) [ERR \*\*FATAL\*\* link provided in KSP.log links to 404
			- [#56](https://github.com/TweakScale/TweakScale/issues/56) "Breaking Parts" patches
			- [#57](https://github.com/TweakScale/TweakScale/issues/57) Implement Warning Dialogs (concluded)
		- 2.5.0.1
			- [#07](https://github.com/TweakScale/TweakScale/issues/7)	Update some patches to KSP 1.5 and 1.6 bug
			- [#41](https://github.com/TweakScale/TweakScale/issues/41) TweakScale is being summoned to scale parts without TweakScale module info?
			- [#42](https://github.com/TweakScale/TweakScale/issues/42) Crash Test for TweakScale - the Ground Breaking tests
		- 2.5.0.0
			- [#10](https://github.com/TweakScale/TweakScale/issues/10) Weird late ADDON-Binder issue
			- [#11](https://github.com/TweakScale/TweakScale/issues/11) Negative mass on parts.
			- [#21](https://github.com/TweakScale/TweakScale/issues/21) Check that :FOR[TWEAKSCALE] thingy on the patches
			- [#31](https://github.com/TweakScale/TweakScale/issues/31) Preventing being ran over by other mods
			- [#34](https://github.com/TweakScale/TweakScale/issues/34) New Sanity Check: duplicated properties]
			- [#35](https://github.com/TweakScale/TweakScale/issues/35) Check for new parts on KSP 1.7 (with Making History!) and add support to them
	+ **WARNING**
		- This can break your KSP, ruin your Windows, kill your pet, offend your mom  and poison your kids. :D
		- By the Holy Kerbol that enlighten us all, please use this only under my instructions, and only if I ask you to do so! Twice. :)
		- TweakScale **strongly** advises you to use [S.A.V.E](https://forum.kerbalspaceprogram.com/index.php?/topic/94997-171-save-automatic-backup-system-155-3121/) for regular backups of your savegames. Really. :)
		- Bug reports for this release **should be issued on the [Issue #42](https://github.com/TweakScale/TweakScale/issues/42) only**, as development problems are not considered "bugs" and should not clutter the back log where real issues happening the field need to be tackled down
