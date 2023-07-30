# TweakScale :: Road Map

Fellow Kerbonauts,

Due recent developments on the KSP, culminating on the current release 1.12.2, the Road Map were (again) heavily reworked.

Oh well. :)

The 2.4.5 series are over. I didn't managed to accomplish what I wanted from it due (not so) unexpected difficulties that arose from the 1.12.x series that, summed to the current ones (specifically, Serenity), suggest a change on the order that things need to be done as well shrinking the scope of each milestone to a more manageable workload.

What I want to share with you now is what to expect from the next minor TweakScale versions:

## 2.4.6.x : The hunt for missing support

While the 2.4.5.x series focused on missing support on patches, the 2.4.6.x will focus on missing support from code.

KSP downto 1.3.1 will be supported again, and the bug hunt will focus on bugs that affect the majority of KSP versions - due the way the code is being designed, everything affecting 1.3.1 will affect also up to 1.12.2, so it makes sense to hunt down these bugs first.

Every effort to **AVOID** breaking backwards compatibility will be applied - exception made by the `TweakScaleExperimental` patches, that can be changed at anytime without notice. **DO NOT** activate the Experimental Patches on your "production" savegames - but they should be OK for short-living installents (and videomaking)

## 2.4.7.x : Technical Debts on Scalings

With the "legacy" support finished (and remembering that these artefacts are also used on modern KSP!), the hunt for technical debts plaguing the most recent KSPs will be carried out.

## 2.4.8.x : The Big Patches Clean Up

This one will be another one focused on Patching.

The `TweakScaleExperimental` program will be finished, with all scalable parts still on Experimental status being formally supported (or just ditched - not everything needs to be scaled, after all).

The long due Localisation efforts will be finally carried out.

## 2.4.9.x : The Patches Big Clean Up

Some new features implementing some recently detected use cases while supporting KIS and Stock Inventory.

Efforts to use the new widgets from KSP 1.8 and forward.

## 2.4.10.x : Serenity

Researches suggest that supporting Serenity will be a hell of a task. So I focused all the related efforts on this series.


## 2.5.0.x : "My Kraken…. It's full of ":FOR"s….

This one can be troublesome again. My apologies.

The root cause of some of the worse problems that plagued parts using TweakScale in the last years (yeah… **years**) is rogue patches. However, TweakScale also didn't did its part of the bargain to help the fellow Add'Ons Authors - currently, it's not possible to safely use `:BEFORE` and `:AFTER` on TweakScale, as it's still on the "Legacy" patching support.

A lot of mishaps would had been prevented by using that two directives. However, they :NEEDS :P TweakScale using `:FOR` on its patches, what would remove the TweakScale from the Legacy patching - and this is where things start to go through the tubes.

Some Third Party Add'On on the wild, still, relies on TweakScale being in the Legacy with the Add'Ons ending up, after some blood, sweat and tears, reaching a fragile equilibrium on the patching - as an airplane flying in its absolute ceiling. A Kerbal farts somewhere in the plane, the thing stalls. This aphorism describes pretty well the current status quo, by the way. :P

There's no easy way out of this mess:

* I don't do it, we will live with patching problems for the rest of our lives - on every install of a new Add'On. And sooner or later we will need another round of a new incarnation of the 2.4.3.x series. Not funny.
* I do it, and we will have a new flood of KSP.logs around here. :P

So, in the end, it's a matter of choosing the KSP eco system we want to have - and I have a hard time believing that KSP players like their rockets anchored in the 3D space, or having the statics exploding for no reason. :) 

A important change is due to happen on 2.5.0.x to protect my SAS and to help me to keep the TweakScale /L eco system healthy. Rest assured that current Add'Ons will be able to use and embed TweakScale as they always did no matter what.


## *status quo*

The (current) schedule [is here](https://github.com/TweakScale/TweakScale/milestones).

