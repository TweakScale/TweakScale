/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

		TweakScale /L is double licensed, as follows:

		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

		And you are allowed to choose the License that better suit your needs.

		TweakScale /L is distributed in the hope that it will be useful,
		but WITHOUT ANY WARRANTY; without even the implied warranty of
		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

		You should have received a copy of the SKL Standard License 1.0
		along with TweakScale /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

		You should have received a copy of the GNU General Public License 2.0
		along with TweakScale /L If not, see <https://www.gnu.org/licenses/>.
*/
using TweakableEVA;
using TweakableEverything;

namespace TweakScale
{
    public static class TweakScaleTools
    {
        static public void RescaleFloatRange(PartModule pm, string name, float factor)
        {
			System.Reflection.FieldInfo field = pm.GetType().GetField(name);
			float oldValue = (float)field.GetValue(pm);
			UI_FloatRange fr = (UI_FloatRange)pm.Fields[name].uiControlEditor;
            fr.maxValue *= factor;
            fr.minValue *= factor;
            fr.stepIncrement *= factor;
            field.SetValue(pm, oldValue * factor);
        }
    }

    public class ModuleTweakableDockingNodeUpdater : IRescalable<ModuleTweakableDockingNode>
    {
        private ModuleTweakableDockingNode _module;

        public ModuleTweakableDockingNodeUpdater(ModuleTweakableDockingNode pm)
        {
            _module = pm;
        }

        public void OnRescale(ScalingFactor factor)
        {
            TweakScaleTools.RescaleFloatRange(_module, "acquireRange", factor.relative.linear);
            TweakScaleTools.RescaleFloatRange(_module, "acquireForce", factor.relative.quadratic);
            TweakScaleTools.RescaleFloatRange(_module, "acquireTorque", factor.relative.quadratic);
            TweakScaleTools.RescaleFloatRange(_module, "undockEjectionForce", factor.relative.quadratic);
            TweakScaleTools.RescaleFloatRange(_module, "minDistanceToReEngage", factor.relative.linear);
        }
    }

    public class ModuleTweakableEVAUpdater : IRescalable<ModuleTweakableEVA>
    {
        private ModuleTweakableEVA _module;

        public ModuleTweakableEVAUpdater(ModuleTweakableEVA pm)
        {
            _module = pm;
        }

        public void OnRescale(ScalingFactor factor)
        {
            TweakScaleTools.RescaleFloatRange(_module, "thrusterPowerThrottle", factor.relative.linear);
        }
    }

    public class ModuleTweakableReactionWheelUpdater : IRescalable<ModuleTweakableReactionWheel>
    {
        private ModuleTweakableReactionWheel _module;

        public ModuleTweakableReactionWheelUpdater(ModuleTweakableReactionWheel pm)
        {
            _module = pm;
        }

        public void OnRescale(ScalingFactor factor)
        {
            TweakScaleTools.RescaleFloatRange(_module, "RollTorque", factor.relative.cubic);
            TweakScaleTools.RescaleFloatRange(_module, "PitchTorque", factor.relative.cubic);
            TweakScaleTools.RescaleFloatRange(_module, "YawTorque", factor.relative.cubic);
        }
    }
}
