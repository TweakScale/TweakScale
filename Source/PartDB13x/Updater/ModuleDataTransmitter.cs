/*
	This file is part of TweakScale /L
		© 2018-2023 LisiasT
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
	along with TweakScale /L. If not, see <https://www.gnu.org/licenses/>.
*/
using System;

namespace TweakScale.Updater
{
	public class DataTransmitter : AbstractWithLog, IRescalable<ModuleDataTransmitter>
	{
		private readonly ModuleDataTransmitter module;

		public DataTransmitter(PartModule partModule) : base(partModule)
		{
			this.module = (ModuleDataTransmitter)partModule;
		}

		void IRescalable.OnRescale(ScalingFactor factor)
		{
			Log.dbg("{0} OnRescale {1} {2}", this.GetType().FullName, this.InstanceID, factor);
			double p = this.module.antennaPower / 1000;
			char suffix = 'k';
			if(p >= 1000)
			{
				p /= 1000f;
				suffix = 'M';
				if(p >= 1000)
				{
					p /= 1000;
					suffix = 'G';
				}
			}
			p = Math.Round(p, 2);
			string str = p.ToString() + suffix;
			if (this.module.antennaCombinable) { str += " (Combinable)"; }
			this.module.powerText = str;
		}

	}
}
