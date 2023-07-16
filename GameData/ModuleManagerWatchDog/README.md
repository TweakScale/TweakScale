# Module Manager Watch Dog

A Watch Dog for Module Manager.


## In a Hurry

* [Latest Release](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/releases)
	+ [Binaries](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/tree/Archive)
* [Source](https://github.com/net-lisias-ksp/ModuleManagerWatchDog)
* [Issue Tracker](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/issues)
* Documentation	
	+ [Homepage](http://ksp.lisias.net/add-ons/ModuleManagerWatchDog) on L Aerospace KSP Division
	+ [Project's README](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/blob/master/README.md)
	+ [Install Instructions](https://github.com/net-lisias-ksp/ModuleManagerWatchDog/blob/master/INSTALL.md)
	+ [Change Log](./CHANGE_LOG.md)
	+ [Known Issues](./KNOWN_ISSUES.md)
	+ [TODO](./TODO.md) list
* Official Distribution Sites:
	+ [Homepage](http://ksp.lisias.net/add-ons/ModuleManagerWatchDog) on L Aerospace
	+ [Source and Binaries](https://github.com/net-lisias-ksp/ModuleManagerWatchDog) on GitHub.


## Description

*The factory of the future will have only two employees, a man and a dog.*

*The man will be there to feed the dog.*

*The dog will be there to keep the man from touching the equipment.*

**Warren Bennis**
- - - 

`MMWD` is a Watch Dog to monitor Module Manager, preventing known (and unsolved) bugs on KSP 1.8 and newer that affects it.

Additional services for updating specific DLLs on `GameData.` are now part of the solution, making it useful even for KSP <= 1.7.3 .

The following services are provided:

### Updating critical DLLs on `GameData`.

Some 3rd party Add'Ons and this tool itself need critical DLLs to be present directly into `GameData`, however manual installers usually forget to do it.

Additionally, CurseForge - at least at the moment of this release - doesn't have a mechanism to allow updating files on it, triggering bug reports that will be easily avoided by deploying this tool.

This tool checks for need for installing (or updating) such critical DLLs automatically for selected Add'Ons.

The feature is implemented on a discrete DLL, `WatchDogInstallChecker.dll`, and can be easily removed from the distribution without colateral effects (other than losing the functionality).

**It's not advisable to use it on installments managed by CKAN**, as it manages itself these DLLs.

The following Add'Ons are currently managed:

* The tool itself (`666_ModuleManagerWatchDog.dll`)
* ModuleManager /L (`ModuleManager.dll`)
* TweakScale /L (`999_Scale_Redist.dll`)

This list can be expanded as needed.

### Checking Module Manager

Historically, managing multiple Module Manager versions was problematic on KSP - to say the least.

Before KSP 1.8, duplicated Assemblies were being loaded on dedicated App Domains, and so calling them would incur in marshalling (essentially, RPC but on the same process...) with significantly performance issues.

But on KSP 1.8 (and until 1.12.0), while trying to tackle down the performance problem by short circuiting all the duplicated Assemblies to the first one loaded, Squad inadvertently caused the oldest MM being elected to be used (as DLLs are loaded in alphabetical order), with pretty serious consequences.

On KSP 1.12.0 things changed again. Now the DLL version is used as criteria, and the highest version found is the one elected to be used. **However**, a new bug was introduced, playing havoc on the system if more than one DLL **has the same filename**.

So, different KSP releases will need different MM handling:

* On KSP \< 1.8, the tool complains if MM is not installed.
* On KSP \>= 1.8 and \< 1.12, the tool will yell on any duplicated Module Manager on the rig - *There can be only one!*
* On KSP \>= 1.12, the tool prevents installing MM/L together Forum's one as an extra safety measure.
	+ MM/L behaves and it's 100% compatible, being a drop in replacement - it's safe to switch MM at any time.
	+ But yet is advisable to avoid having both installed at the same time.

The Author strongly advises to edit the `WatchDog.cfg` file and select enforcing the "1.8 rules". It's far the safest option - besides risking being a bit annoying sometimes.

### Checking TweakScale

Due the general mess that handling duplicated DLLs are on KSP, it was choose to install the formerly named `Scale_Redist.dll` file into `GameData` as `999_Scale_Redist.dll` . This aims to ensure the canon Redist is the first one to be loaded, as well to avoid eventual naming collisions with the few Add'Ons that used to have it embedded.

This way, we can be ensure the best performance on older KSPs as well a safest environment on newer ones.

The tool checks for known `Scale_Redist` clients and yells if they are present but not the Redist. It also yells if there're more than one copy of `Scale_Redist` are installed, and also enforce that the only one installed are the `GameData/999_Scale_Redist.dll` .

### Checking KSP Interstellar Extended

Similar handling are applied to `Interstellar_Redist.dll` from KSPIE.

### Final Considerations

This tool was originally aimed to be redistributed embedded on Add'Ons that used to redistribute Module Manager themselves in the past, unintentionally triggering the problems this tool aims to detect, but evolved to an Add'On  de jure and de facto. 


## Installation

Detailed installation instructions are now on its own file (see the [In a Hurry](#in-a-hurry) section) and on the distribution file.

### Licensing

* Module Manager Watch Dog is licensed as follows:
	+ [SKL 1.0](https://ksp.lisias.net/SKL-1_0.txt). See [here](./LICENSE.KSPe.SKL-1_0)
		+ You are free to:
			- Use : unpack and use the material in any computer or device
			- Redistribute : redistribute the original package in any medium
		+ Under the following terms:
			- You agree to use the material only on (or to) KSP
			- You don't alter the package in any form or way (but you can embedded it)
			- You don't change the material in any way, and retain any copyright notices
			- You must explicitly state the author's Copyright, as well an Official Site for downloading the original and new versions (the one you used to download is good enough)

Please note the copyrights and trademarks in [NOTICE](./NOTICE).


## UPSTREAM

There's no upstream, **I am (g)ROOT** :)
