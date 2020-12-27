# TweakScale :: Changes

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
