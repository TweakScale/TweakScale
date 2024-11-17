# TweakScale :: Changes

* 2024-1117: 2.4.8.7 (Lisias) for KSP >= 1.3
	+ Fixes (**AGAIN**) a regression on handling attachment nodes, thanks Kraken affecting **only** KSP 1.4.3 (and almost surely 1.4.0 to 1.4.2, but I didn't bored to check).
		- Special attention and caring were taken to **do not** change anything on support for any other KSP.
	+ **Finally** fixes an embarrassing bug that where `double`s were being squashed to `float`s. I do not expect any change the default scalings, but people using some dramatic customisations on really big and really small exponents should see some improvements.
	+ Removes some (TS unrelated) sanity checks when a CKAN managed installment is detected
		- TweakScale is, from now on, blindly trusting CKAN on keeping the running environment sane, only yelling when it's directly affected.
	+ Reworks Issues:
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant.
* 2024-0921: 2.4.8.6 (Lisias) for KSP >= 1.3
	+ Due recently realised changes on the way [CKAN handles alternate downloads](https://forum.kerbalspaceprogram.com/topic/225966-psa-update-your-ckan-clients-to-134/?do=findComment&comment=4421703), some safeties were implemented to alert the user if by some reason it was installed a non CKAN approved package on a CKAN managed installment.
	+ Closes Issues:
		- [#339](https://github.com/TweakScale/TweakScale/issues/339) Prevent non CKAN safe binaries from being used on CKAN managed installations.
* 2024-0914: 2.4.8.5 (Lisias) for KSP >= 1.3
	+ Fixes a long standing mishandling on the Life Cycle of the `SingletonBehaviour`'s extended classes.
	+ Closes Issues:
		- [#336](https://github.com/TweakScale/TweakScale/issues/336) Finally Diagnosed a Memory Leak on `EditorHelper`
* 2024-0831: 2.4.8.4 (Lisias) for KSP >= 1.3
	+ Somewhat better error messages, in a (futile, probably) attempt to prevent this [kind of crap](https://www.reddit.com/r/KerbalAcademy/comments/1ejaf9b/houstonerror_contradiction/).
	+ Updates MMWD to 1.1.2.1
* 2024-0525: 2.4.8.3 (Lisias) for KSP >= 1.3
	+ Fixes a (yet another :P) major screwup of mine, this one while handling systems without `ModuleManagerWatchDog` installed - exactly the situation the `TweakScale.WatchDog` was born to handle on 2.4.8.0...
		- Yep, sometimes I'm just overloaded by Real Life©...
	+ Reworks Issues:
		- [#312](https://github.com/TweakScale/TweakScale/issues/312) Write an internal Self Check Mechanism	 
* 2024-0524: 2.4.8.2 (Lisias) for KSP >= 1.3
	+ Detected and fixed a borderline situation in which the `IPartCostModifier` from the TweakScale's `PartModule` was being called **before** the `OnLoad` while merging a craft with scaled cockpit - unsure if the cockpit made any difference, but whatever.
		- I'm shooting first and making questions later - I will left a proper diagnose to be tackled down on the Beta 2.5.
* 2024-0524: 2.4.8.1 (Lisias) for KSP >= 1.3
	+ ***WITHDRAWN***
	+ Let's pretended it never happened, please?
* 2024-0515: 2.4.8.0 (Lisias) for KSP >= 1.3
	+  This release **finally** solves some long standing problems caused by Editor when handling `ModulePartVariant` that I only managed to diagnose recently, see issues [#307](https://github.com/TweakScale/TweakScale/issues/307) and [#314](https://github.com/TweakScale/TweakScale/issues/314) *et all* for the full history for this one.
	+  I **hope** that, **finally**, the 2.4.8.x series will put an end point on the legacy branch. I plan to do only minor adjustments (if needed) on the legacy from now on.
		- If I manage to do everything right (this time), the already legendary 2.5 series will be bought into the mainstream Soon™.
	+ **ATTENTION**: You need to update [KSP-Recall](https://github.com/net-lisias-ksp/KSP-Recall/releases) to 0.5.0.1 at least.
	+  Closes Issues:
		- [#325](https://github.com/TweakScale/TweakScale/issues/325) Cope with https://github.com/net-lisias-ksp/KSP-Recall/issues/73
		- [#312](https://github.com/TweakScale/TweakScale/issues/312) Write an internal Self Check Mechanism
		- [#307](https://github.com/TweakScale/TweakScale/issues/307) Attachment Points are not being scaled (or being reset) after changing the Variant
		- [#283](https://github.com/TweakScale/TweakScale/issues/283) New screw up from KSP 1.11.0 Editor was revealed by the 2.4.6.20 release
