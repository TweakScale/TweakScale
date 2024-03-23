# TweakScale :: Changes

* 2024-0322: 2.4.7.6 (Lisias) for KSP >= 1.3
	+ Backports from **Beta** a long standing bug on AutoScale when attaching parts with different scaling methods. 
	+ Found an idiocy of mine while trying to prevent a hypothetical problem - and ended up creating a concrete one instead.
		- If you know a priest in need to a job, [we are hiring](https://github.com/net-lisias-ksp/KSP-Recall/issues/61#issuecomment-2014430999)... :P
	+ Closes Issues:
		- [#323](https://github.com/TweakScale/TweakScale/issues/323) Auto-Scale is screwed since 2.4.7.0
* 2024-0213: 2.4.7.5 (Lisias) for KSP >= 1.3
	+ An insidious bug screwing parts that use `techRequired` was fixed.
		- Thanks to [Turbo Ben](https://forum.kerbalspaceprogram.com/profile/193979-turbo-ben/) for the [work into zeroing](https://forum.kerbalspaceprogram.com/topic/179030-ksp-130-tweakscale-under-lisias-management-2474-2023-1007/?do=findComment&comment=4366095) into the exact root cause of the problem!
* 2023-1007: 2.4.7.4 (Lisias) for KSP >= 1.3
	+ A serious regression was detected on 2.4.7.3. The code intended to fix [Issue 307](https://github.com/TweakScale/TweakScale/issues/307) triggered ***Yet Another Bug on Editor™***, and had to be removed. This, unfortunately, resurrects #307. :(
	+ Closes Issues:
		- [#314](https://github.com/TweakScale/TweakScale/issues/314) The Editor is screwing with me again when scaling Parts with `PartModuleVariant`
	+ ReOpen Issues:
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant  
* 2023-0805: 2.4.7.3 (Lisias) for KSP >= 1.3
	+ Backport the Unity's `Update` Life Cycle fix from [Aviation Lights #4](https://github.com/net-lisias-ksp/AviationLights/issues/4)
		- Hopefully preventing some shitstorm on users running KSP on Hybrid CPUs (P-Cores, E-Cores, that crap).
	+ Closes Issues:
		- [#308](https://github.com/TweakScale/TweakScale/issues/308) Insidious NRE on changing scenes 
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant
* 2023-0716: 2.4.7.2 (Lisias) for KSP >= 1.3
	+ A new Feature was introduced that automatically deactivates the **Auto Scale** and the **Chain Scale** features every time the user enters the Editor, creates a new Craft or loads one.
		- Aims to minimize support tickets opened by users that forget the features active and then thinks it's a bug on TweakScale
		- Default is On, can be turned off on the TweakScale Settings dialog.
	+ Updates KNOWN ISSUES with workarounds for the following Work In Progress bugs:
		- https://github.com/TweakScale/TweakScale/issues/297
		- https://github.com/TweakScale/TweakScale/issues/283
* 2023-0324: 2.4.7.1 (Lisias) for KSP >= 1.3
	+ Updates the Companions' definition file to mark KIS (and some others) deprecated add'ons as... deprecated!
		- Thanks for the [heads up](https://forum.kerbalspaceprogram.com/index.php?/topic/179030-ksp-130-tweakscale-under-lisias-management-24625-2023-0304/&do=findComment&comment=4264686), [@ngx](https://forum.kerbalspaceprogram.com/index.php?/profile/184821-ngx/)!
	+ New checking to detect deprecated Companions forgotten on the `GameData`
* 2023-0321: 2.4.7.0 (Lisias) for KSP >= 1.3
	+ Better TweakScale Companion checkings.
