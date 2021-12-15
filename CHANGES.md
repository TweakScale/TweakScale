# TweakScale :: Changes

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
