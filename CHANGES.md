# TweakScale :: Changes

* 2022-1116: 2.4.6.18 (Lisias) for KSP >= 1.3
	+ A merge error was detected, affecting the KSP dependencies checks, and fixed.
* 2022-1113: 2.4.6.17r2 (Lisias) for KSP >= 1.3
	+ (Finally) Solves a long standing scaling problem related to Stock Buoyancy.
	+ Solves the problem related to retrieving/storing Science from scaled Pods.
	+ Makes some error messages easier to understand, as well fixes some pathnames to be useable on Windows. Thanks, [@Hebarusan](https://githu
b.com/HebaruSan)!
	+ Updates KSPe.Light to 2.4.1.23
		- (Hopefully) Mitigates KSP being fired up with the wrong `pwd` - not that KSP will behave as expected, but at least I will not take the blame for it.
	+ Closes Issues:
		- [#268](https://github.com/net-lisias-ksp/TweakScale/issues/268) Misbehaviour related to Taking Data from a Pod when it's scaled.
		- [#252](https://github.com/net-lisias-ksp/TweakScale/issues/252) Scale the Buoyance so the scaled parts has a similar floating capabilities as the original.
* 2022-1112: 2.4.6.17 (Lisias) for KSP >= 1.3
	+ ***DITCHED***
* 2022-0713: 2.4.6.16 (Lisias) for KSP >= 1.3
	+ Mitigates an undesired collateral effect from the symlink handling on C#'s runtime on MacOS and Linux.
		- Updates KSPe.Light to 2.4.1.21
		- Preload the TweakScale's toolbar Icons on the Space Center scene, where mysteriously they are loaded without nasty delays.
		- Thanks for the [heads up](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-130-tweakscale-under-lisias-management-24615-2022-0523/&do=findComment&comment=4153928), [revuwution](https://forum.kerbalspaceprogram.com/index.php?/profile/213676-revuwution/)!
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
* 2022-0508: 2.4.6.13 (Lisias) for KSP >= 1.3
	+ Fixes a nasty bug about scaling down crewed parts, reported by [obi_243](https://forum.kerbalspaceprogram.com/index.php?/profile/221308-robi_243/) on [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/208059-tweakscale-problem/#comment-4128151). Thanks, dude!!
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
* 2022-0326: 2.4.6.10 (Lisias) for KSP >= 1.3
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
* 2022-0322: 2.4.6.9 (Lisias) for KSP >= 1.3
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
* 2021-1016: 2.4.6.1 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Rolls back an incomplete localization issue that passed through while merging features from the development branch.
* 2021-1010: 2.4.6.0 (Lisias) for 1.3.0 <= KSP <= 1.12.2
	+ Breaks the 1.4.4 barrier! Now TweakScale supports from KSP 1.3.0 to the latest! **#HURRAY!!**
	+ Resurrects the AutoScale feature. Use `CTRL-L` to activate/deactivate
		- A HotKey collision with a 3rd-party add'on is a Known Issue. This will be tackled down on [Issue #202](https://github.com/net-lisias-ksp/TweakScale/issues/202).
	+ Updates KSPe.Light.TweakScale to 2.4.0.4
		- **ATTENTION!!** : Users of the following TweakScale Companions **must** update them _immediatelly_, as this release breaks binary compatibility (i.e. they will not load!!):
			- [TSCo_FS](https://github.com/net-lisias-ksp/TweakScaleCompanion_FS/releases/tag/RELEASE%2F1.1.0.0)
			- [TSCo_KIS](https://github.com/net-lisias-ksp/TweakScaleCompanion_KIS/releases)
			- [TSCo_PKMC](https://github.com/net-lisias-ksp/TweakScaleCompanion_PKMC/releases)
			- [TSCo_Visuals](https://github.com/net-lisias-ksp/TweakScaleCompanion_Visuals/releases/tag/PRERELEASE%2F0.2.0.0)  
open %f
