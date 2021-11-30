# TweakScale :: Changes

* 2021-1130: 2.5.0.40 (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#219](https://github.com/net-lisias-ksp/TweakScale/issues/219) Apparently, OnCopy parts is misbehaving on Parts with Variants
		- [#86](https://github.com/net-lisias-ksp/TweakScale/issues/86) When a root part is scaled, a part attached to it is displaced on the Y axis!
* 2021-1120: 2.5.0.39 (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#211](https://github.com/net-lisias-ksp/TweakScale/issues/211) Mitigate the HotKeys being hijacked by 3rd-parties
		- [#209](https://github.com/net-lisias-ksp/TweakScale/issues/209) TweakScale not installed on wrong directory
		- [#197](https://github.com/net-lisias-ksp/TweakScale/issues/197) Flags (the parts attachable) are losing the attaching points when its parent is duplicated
		- [#167](https://github.com/net-lisias-ksp/TweakScale/issues/167) Mirror Symmetry is displacing some (all?) parts when scaled
		- [#139](https://github.com/net-lisias-ksp/TweakScale/issues/139) Scaling Part with Variants that change attachment nodes is not working as expecnted
* 2021-0927: 2.5.0.38 (Lisias) for KSP >= 1.3
	+ Maintenance Release.
	+ Closes Issues:
		- [#208](https://github.com/net-lisias-ksp/TweakScale/issues/208) Chain Scaling Parts with variants are borking when the parent part is "inverted"
		- [#207](https://github.com/net-lisias-ksp/TweakScale/issues/207) The Upgrade Pipeline thingy (or something else?) is playing havoc with TweakScale 
		- [#175](https://github.com/net-lisias-ksp/TweakScale/issues/175) Wrong displacement of the attached part placed inverted when scaling its parent
		- [#163](https://github.com/net-lisias-ksp/TweakScale/issues/163) Radial Symmetry (when using variants) are misplacing parts.
		- [#131](https://github.com/net-lisias-ksp/TweakScale/issues/131) Chain Scaling parts is playing havoc with the Radial Attachment Positions. 
		- [#36](https://github.com/net-lisias-ksp/TweakScale/issues/36) [TweakScale Warning] Exception during ModulePartVariants interaction
* 2021-0927: 2.5.0.37 (Lisias) for 1.4.4 <= KSP <= 1.12.2
	+ Maintenance release
		- Declares the Ground Anchor as Experimental		- Updates the code base to the latest KSPe release (2.4.0.1 at this moment)
* 2021-0922: 2.5.0.36 Beta (Lisias) for KSP >= 1.3.1
	+ Recompiled against the new KSPe 2.4.0.0
		- [Update before using](https://github.com/net-lisias-ksp/KSPAPIExtensions/releases/tag/RELEASE%2F2.4.0.0), it's a hard dependency on the new Version. 
* 2021-0913: 2.5.0.35 Beta (Lisias) for KSP >= 1.3.1
	+ Fixes (again) the Decluttering thingy. 
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
* 2021-0912: 2.5.0.33 Beta (Lisias) for KSP >= 1.3.1
	+ Catches up all fixes already published on mainstream.
	+ Implements support for **ALL** KSP versions downto 1.3.1 !!! #HURRAY!!
		- Specialised DLLs loaded under demand, thanks KSPe!
		- 1.2.2, however, will need more work. Unsure if it worths it.
	+ Closes Issues:
		- [#198](https://github.com/net-lisias-ksp/TweakScale/issues/198) Breakdown Scaling support for each major KSP version in specialised DLLs
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
