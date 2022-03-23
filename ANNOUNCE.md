# ANNOUNCE

Release 2.4.6.9 is available for downloading, with the following changes:

+ Removes an (now) unnecessary gambiarra, as KSP-Recall is now fixing the mess on KSP >= 1.9 editor.
+ Implements a new Sanity Check against a worrisome situation where a Part is given to TweakScale without the `partInfo`!!!
	- I don’t have the slightest idea about how in hell this can happen, but I got confirmation of this problem from reliable sources.
+ **Finally** implementing a full-fledged "TweakScale Upgrade Pipeline", allowing run-time, on-the-fly conversions between ScaleTypes and DefaultScales.
	- No more worries about installing or updating Add'Ons that changes the TweakScale patches.
	+ [All Tweak!!!](https://forum.kerbalspaceprogram.com/index.php?/topic/182700-111x-all-tweak-07-23rdoctober2019/) users, this one is dedicated to you! :)
+ Closes Issues:
	- [#237](https://github.com/net-lisias-ksp/TweakScale/issues/237) New Sanity Check: parts without partInfo!!!
	- [#236](https://github.com/net-lisias-ksp/TweakScale/issues/236) Extent the Scale migration feature to allow switching ScaleTypes and DefaultScales!
	- [#218](https://github.com/net-lisias-ksp/TweakScale/issues/218) Implement GetInfo on TweakScale's Part Module

See OP for the links.

## Notes

I **finally** implemented a proper "TweakScale Upgrade Pipeline" :P for TweakScale patches. This means that your savegames and craft files will not be screwed anymore if:

* You install All Tweak!! and later install a [TweakScale Companion](https://forum.kerbalspaceprogram.com/index.php?/topic/192216-tweakscale-companion-program-2021-1016/).
* You install something that changes any pre-existing [TweakScale Support](https://forum.kerbalspaceprogram.com/index.php?/topic/192216-tweakscale-companion-program-2021-1016/).

**This** was the main block for the TweakScale Companion Program: I had noticed that some add'on Authors decided to second guess my patches on the Companions as soon as I publish a beta, and since they published them on SpaceDock and CKAN before my patches going gold, I had to consider them as pre-existent patches when publishing the better crafted, better curated and better implemented patches on the Companions (hey, **I AM** the TweakScale guy, I **know** how to write patches properly, how do properly write Scale Types and even new Standard Scalings matching the parts at hand!).

Now this problem is not more.

[All Tweak!!!](https://forum.kerbalspaceprogram.com/index.php?/topic/182700-111x-all-tweak-07-23rdoctober2019/) users, in special, are in a way better position than before - now you don't have to fear updates and new add'ons installing different scales for your flying parts! :)

However… Only the scalings are converted. I can't "fix" changes on Behaviours and PartModule's support: if an older part doesn't scale some attribute (or scale it with a different exponent), then the new patches will scale the part differently, and you will have crafts behaving differently from what you designed. By example, if you build an airplane using a patch that scales mass with a 2.5 exponent, and then install a new patch set that scales mass at 3.0 exponent, them your airplane will end up unbalanced. *"You can't have the cake and eat it too…"*

But at least your craft files will be editable. :)

Additionally, I'm starting to remove from TweakScale some "gambiarras" and shenanigans meant to cope with previous KSP's idiosyncrasies. [KSP-Recall](https://forum.kerbalspaceprogram.com/index.php?/topic/192048-18/) will, from now on, be the sole responsible for handling these unfortunate "features" introduced on previous KSP versions and, sadly, never fixed.

The reason for this is that I recently realised that by brute-forcing my way on TweakScale, I could be screwing up any hypothetical Add'On that would be processed after TweakScale (KSP appears to process things in the order they are found on the part's config section) the same way Editor 1.9 started to screw with TweakScale, and I found this **_unacceptable_**. Fixing things for yourself while pushing your weight on everybody else **is not being a Team Player**, and a Community is (or should be) about Team Players. I help you, you help me, and so we help others.

Some mishaps happened in the mean time, and I apologise for them - but in the process I found some borderline situations where that TweakScale's *gambiarra* could not even touch. The after math is a **way** more behaving KSP game from now on.

And I pushed forward a nice feature that will help users to check if a part have TweakScale support, and the supported sizes. 

![](https://user-images.githubusercontent.com/64334/159203682-5e46d0cf-b151-4574-8ad4-e1698d3c7420.png)

## Disclaimer

By last, but not the least...

I have this bitter and sad need to explain some unfortunate events, in the hope we **finally** manage to stop some slandering. I'm really saddened to have to disclose this, but pretending this was not happened didn't helped in the past. So… 

TweakScale (and KSP-Recal) were, recently, being targeted by slandering. Again.

I don't have the slightest idea **how in hell** someone can openly and bluntly make easily refutable affirmations about how TweakScale or myself behaves. I don't have the slightest idea **why, by Kraken's sake**, some very influential and important members of this Community allow themselves to be pushed on such egregious behaviour.

But yet, it was what happened.

So, for the sake of clarity and Trueness:

* Neither KSP-Recall neither TweakScale **have any hard requirement** for any specific version of Module Manager other than the Forum's one, (normally) available here on Forum.
	+ I have a personal fork of MM, [MM/L](https://github.com/net-lisias-ksp/ModuleManager), that I use for development and personal use.
	+ It's faster, it's way better on log management and it works on every KSP version since KSP 1.3.0 **flawlessly**, so I don't have to rely on buggy, older MM versions for playing my KSP of choice.
		- HELL. I have a KSP 1.2.2 binary for it too. 
	+ And it behaves **100%** like the Forum's one. Not a single patch is different, not a single functional MM feature is missing (*au contraire*, the /L fork is more compatible with classical MM than current MM itself).
	+ **And you DO NOT need it**. You can keep your playing using Forum's MM.
		- **And this will not change**.

Every time the Forum's MM was unreachable by a reason or another, **I always pinpointed the Forum's one on my personal repository** - had the MM author published MM on github as a backup plan for when his site is unreachable, most people would not even be aware of my fork.

The latest [MM/L](https://github.com/net-lisias-ksp/ModuleManager) release have about 300 downloads on github, while the previous TweakScale release has about 30K downloads on SpaceDock and 40K on CurseForge.

**How in hell** someone concludes that something those previous release have 70K downloads have a hard dependency on something else with only 300 **it's absolutely beyound by comprehension**.

I will save you from pinpointing the posts where this slandering happened, and I will not speculate about the motivations.

— — — — —

This Release will be published using the following Schedule:

* GitHub, reaching first manual installers and users of KSP-AVC. Right now.
* CurseForge. Soon™
* SpaceDock (and CKAN users). Not So Soon™

The reasoning is to gradually distribute a potentially Support Fest release in a way that would me allow to provide proper support if anything else goes wrong.
