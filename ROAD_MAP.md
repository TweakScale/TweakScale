# TweakScale :: Road Map

Fellow Kerbonauts,

Due some changes on the Status Quo, mainly due some RL issues "infecting" my life (but not only), the TweakScale Road map was updated.

The TweakScale 2.4.3.x series is, **FINALLY**, EoL.  `#HURRAY` (I will not miss these ones. :P )

I want to thank every one of you that helped me to diagnose all that glitches, bugs and misbehaviours in which TweakScale was, directly or indirectly, involved. Every bug report, every complain, every log, had helped me to detect, diagnose and fix a huge amount of bugs and misconfigurations on the whole eco system, what ends up being good for everybody.

I'm pretty happy with the ending results besides the trouble, we have a way more stable gaming instalment nowadays. As nothing good comes cheap, we have a somewhat whiny game setup too. :P 

Oh well. :) 

What I want to share with you now is what to expect from the next two minor TweakScale versions, 2.4.4.x and the yet somewhat far 2.5.x .

## 2.4.4.x : Le Roi est mort, Vive le Roi.

The whole 2.4.4.x releases will be focused on properly supporting what's now unsupported from Stock and KSP 1.9.x (except new Modules and Serenity).

The main purpose of the 2.4.4 series is to **pave the way** to a lean and clean TweakScale, using and abusing from the new Concept of [TweakScale Companion Program](https://forum.kerbalspaceprogram.com/index.php?/topic/192216-*/) (see [github](https://github.com/net-lisias-ksp/TweakScaleCompanion) too).

Every effort to **AVOID** breaking backwards compatibility will be applied. 

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

## 2.5.1.x : Old parts, New tricks

Renewed support for currently (partially) supported parts will be updated - in special, I want to make Wheels correctly scalable again. This, as usual, will not be free - by correctly scaling up the Wheels, we end up correctly scaling down them too - and so things can break on games with tiny little wheels that now are too strong for they sizes and will became weaker as they should.

Serenity Parts will be dealt on this series too. Could not do it before because a lot of new Modules would demand a **lot** of research, and, frankly, KSP 1.9.x broke my legs on this task.

Finally, some nasty errors from the legacy patches will be fixed.

Let's see what happens...

<iframe width="560" height="315" src="https://www.youtube.com/embed/H8_SOAVNVj0" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

While one or another mistake will probably bite our SAS, I expect a very smooth transition for the whole series.

But, as already is usual, **no savegame will be left behind**.

The (current) [schedule](https://github.com/net-lisias-ksp/TweakScale/milestones) is here.

