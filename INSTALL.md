# TweakScale /L : Under New Management

**TweakScale** lets you change the size of a part.

**TweakScale /L** is TweakScale under Lisias' management.


## Installation Instructions

To install, place the GameData folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
	+ Delete `<KSP_ROOT>/GameData/TweakScale`
* Extract the package's `GameData/` folder into your KSP's as follows:
	+ `<PACKAGE>/GameData/TweakScale` --> `<KSP_ROOT>/GameData`
	+ `<PACKAGE>/GameData/999_Scale_Redist.dll` --> `<KSP_ROOT>/GameData`
		- Overwrite any preexisting file.
* If you **have** installed TweakableEverything:
	+ `<PACKAGE>/GameData/TweakableEverything` --> `<KSP_ROOT>/GameData/TweakableEverything`
	+ **Warning**: By reinstalling (or later installing) TweakableEverything, you will need to update TweakScale again!
* Extract the included dependencies (optional)
	+ `<PACKAGE>/GameData/ModuleManagerWatchDog` --> `<KSP_ROOT>/GameData`
	+ `<PACKAGE>/GameData/666_ModuleManagerWatchDog.dll` --> `<KSP_ROOT>/GameData`
		- Overwrite any preexisting file.
	+ **NOTE**: If you are using CKAN, the following file **should not** be installed (remove it manually if needed):
		- `<KSP_ROOT>GameData/ModuleManagerWatchDog/Plugins/WatchDogInstallChecker.dll`
		- But if you are using CKAN, why in hell are you installing it manually? :)
* Install the remaining dependencies
	+ See below on **Dependencies** 

The following file layout must be present after installation:

```
<KSP_ROOT>
	[GameData]
		[ModuleManagerWatchDog]
			...
		[TweakScale]
			[Plugins]
				[PluginData]
					Scale.PartDB.13x.dll
					Scale.PartDB.14x.dll
					Scale.PartDB.15x.dll
					Scale.PartDB.18x.dll
					Scale_Sanitizer.dll
				KSPe.Light.TweakScale.dll
				Scale.dll
			[patches]
				...
			CHANGE_LOG.md
			DefaultScales.cfg
			Examples.cfg
			LICENSE
			NOTICE
			README.md
			ScaleExponents.cfg
			TweakScale.version
			documentation.txt
		666_ModuleManagerWatchDog.dll
		999_Scale_Redist.dll
		ModuleManager.dll
		...
	KSP.log
	PastDatabase.cfg
	...
```


### Dependencies

* KSPe Light for TweakScale
	+ Included for now due the Companion's dependency on it.
	+ Licensed to TweakScale under [SKL 1.0](https://ksp.lisias.net/SKL-1_0.txt)
+ [KSPe](https://github.com/net-lisias-ksp/KSPe/releases)
	+ **Not Included**
* [Module Manager Watch Dog](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/releases)
	+ Included
	+ Licensed to TweakScale under [SKL 1.0](https://ksp.lisias.net/SKL-1_0.txt)
* Module Manager 3.1.3 or later
	+ **Not Included**
