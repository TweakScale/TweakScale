# TweakScale :: Known Issues

* As from 2.5.0.x, TweakScale is now **double licensed** under the SKL 1.0 and GPL 2.0.
	+ All the previous releases until 2.4.3.x are still licensed under the WTFPL license.
	+ All artefacts on the Extras directory are still licensed under the WTFPL.
	+ See the [README](./README.md) for details.
* Scaling parts with variants that change attachment nodes still have some glitches (but it's working way better now). See [this comment](https://github.com/net-lisias-ksp/TweakScale/issues/42#issuecomment-726428889) on [Issue #42](https://github.com/net-lisias-ksp/TweakScale/issues/42) for details.
	+ Known parts affected
		- The Mastodon engine
		- The Structural Tubes
			- T-12
			- T-18
			- T-25
			- T-37
			- T-50
		- And probably more, as Add'Ons starts to use such feature. 
	+ Detaching and reattaching the Mastodon work arounds the problem on the engine.
	+ Detaching and reattaching the parts attached to a scaled tube work arounds the problem with the tubes.
	+ Things on KSP 1.9 are yet more problematic. [KSP Recall](https://github.com/net-lisias-ksp/KSP-Recall/issues/9) will tackle this down.
* The new KSP 1.9.x FTE-1 Drain Vale (ReleaseValve) is not being properly scaled. Only the size (and Mass) are scalable, the functionality is not.
	+ See Issue [#102](https://github.com/net-lisias-ksp/TweakScale/issues/102) for details. 
* KSP 1.9.0 introduced a new glitch (still persisting on KSP 1.9.1) where any change on the Part's Resources are overridden on cloning.
	+ This affects every Add'On that changes the Part's Resource.
	+ This misbehaviour is fixed by installing [KSP Recall](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-*).
		- Users of TweakScale on KSP 1.9.x are advised to install KSP Recall immediately. 
* A change on the Add'On Binder demanded that only **one** Scale_Redist.dll be available on the whole installment.
	+ Delete every single file called `Scale_Redist.dll` from every Add'On you have installed
	+ Don't touch `999_Scale_Redist.dll` on the GameData. This one must stay.
* There're some glitches on KSP 1.8.0 that prevents TweakScale (and any other Add'On using `UI_ScaleEdit` and `UI_FloatEdit`) to correctly display the PAW.
	+ It's **strongly** advised to do not use TweakScale on 1.8.0
	+ But nothing bad will happen, other than a hard time trying to use the PAWs.
* A new and definitively destructive *"Unholly Interactions Between Modules"*, or as it's fondly known by its friends, **Kraken Food**, was found due some old or badly written patches ends up injecting TweakScale properties **twice** on the Node.
	+ This is particularly nasty as it corrupts a previously working GameDatabase that infects your savegames with corrupted part info. Once a new Add'On is installed, or the bad one is uninstalled, suddenly all your savegames with the old, corrupted part info became broken. See details on the [Issue #34](https://github.com/net-lisias-ksp/TweakScale/issues/34).
	+ This was considered **FATAL** as previously perfectly fine parts became corrupted by installing a rogue Patch, that can so be uninstalled later ruining savegames. By that reason, a very scaring warning are being issue in the Main Menu when the problem is detected.
* There's a crashing situation when using TweakScale and [Classic Infernal Robotics](https://github.com/MagicSmokeIndustries/InfernalRobotics).
	+ IR parts scaled down to "Small -" (small minus, the smallest of them) crashes the game when the craft is unpacked.
	+ Apparently quitting immediately KSP, restarting, reloading the game and recovering the vessel from the Track Station is enough to salvage the savegame - but more tests are needed to be sure of that.
	+ Related issues:
		- [#39](https://github.com/net-lisias-ksp/TweakScale/issues/39) Game Crash when scaling some third party parts to the minimum
		- [#40](https://github.com/net-lisias-ksp/TweakScale/issues/40) Feasibility Study for a runtime Sanity Check for issue #39
	+ TweakScale advises all IR users to update to [Infernal Robotics/Next](https://github.com/meirumeiru/InfernalRobotics) where this issue was solved.
* There's a potentially destructive problem happening due *"Unholly Interactions Between Modules"*, or as it's fondly known by its friends, **Kraken Food**. :)
	+ Due events absolutely beyond the TweakScale scope of actions,  some parts are being injected with more than one instance of TweakScale. This usually happens by faulty MM patches, but in the end this can happens by code or even my MM cache's editing.
		- Things appears to work fine, except by some double Tweakables on the UI. However, crafts and savagames get corrupted when loaded by sane KSP installments, as the duplicates now takes precedence on loading config data, overwriting the real ones.
		- **Things become very ugly when by absolutely any reason (new add-on installed or deleted, or even updated) the glitch is fixed on the MM cache. Now, your KSP installment is a sane one, and all your crafts (including the flying ones) will lose their TweakScale settings!**
	+ So, before any fix is attempted to the problem, TweakScale now is taking some measures to preserve your craft settings from being overwritten once the craft is loaded into a sane installment.
		- Keep in mind, however, that TweakScale acts on **SAVING** data. You need to load and save every craft and savegame using the latest TweakScale as soon as you can. 
	+ A proper fix to the root cause, now, is not only beyound the reach of TweakScale, **as it's also destructive**. Only after TweakScale 2.4.1 or beyound are mainstream for some time it will be safe to do something about - and by then, something else will probably be needed to rescue old crafts and savegames. 
* TweakScale 2.4.x is known to (purposely) withdraw support for some parts on runtime. This, unfortunately, damages crafts at loading (including from flying ones) as the TweakScale data plain vanishes and the part goes back to stock.
	+ Parts being deactivated are being logged into KSP.log, pinpointing to an URL where the issue it causes is described. TweakScale **does not** hides from you what it's being done.
	+ This is unavoidable, unfortunately, as the alternative is a fatal corruption of the game state (persisted on savegames) that leads to blowing statics and ultimately game crash.
	+ The Maintainer is terribly sorry for the mess (my savegames gone *kaput* too), but it's the less evil of the available choices.
	+ The proposed mitigation measure is to backup your savegames, try TweakScale 2.4.x and then decide if the damages (if any, only a few parts are affected) are bigger than the risks - but then, make **hourly** backups of your savegames as one the misbehaviour is triggered, your savegame can be doomed and forever leading to crashes.
	+ Related issues:
		- [#9](https://github.com/net-lisias-ksp/TweakScale/issues/9) Weird issue with SXT parts using FSBuoyancy
		- [#11](https://github.com/net-lisias-ksp/TweakScale/issues/11) Negative mass on parts
		- [#12](https://github.com/net-lisias-ksp/TweakScale/issues/12) Zero Mass on Parts
		- [#15](https://github.com/net-lisias-ksp/TweakScale/issues/15) Prevent B9PartSwitch to be handled when another Part Switch is active

- - -

* RiP : Research in Progress
* WiP : Work in Progress
