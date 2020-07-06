/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

		THIE FILE is licensed to you under:

		* WTFPL - http://www.wtfpl.net
			* Everyone is permitted to copy and distribute verbatim or modified
 			    copies of this license document, and changing it is allowed as long
				as the name is changed.

		THIE FILE is distributed in the hope that it will be useful,
		but WITHOUT ANY WARRANTY; without even the implied warranty of
		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/
namespace TweakScale
{
    /// <summary>
    /// Converts from Gaius' GoodspeedTweakScale to updated TweakScale.
    /// </summary>
    public class GoodspeedTweakScale : TweakScale
    {
        private bool _updated;

        protected override void Setup()
        {
            base.Setup();
            if (_updated)
                return;
            tweakName = (int)tweakScale;
            tweakScale = ScaleFactors[tweakName];
            _updated = true;
        }
    }
}
