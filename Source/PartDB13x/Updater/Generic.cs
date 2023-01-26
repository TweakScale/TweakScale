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
using System.Linq;

namespace TweakScale.Updater
{
	/// <summary>
	/// This class updates mmpfxField and properties that are mentioned in TWEAKSCALEEXPONENTS blocks in .cfgs.
	/// It does this by looking up the mmpfxField or property by name through reflection, and scales the exponentValue stored in the base part (i.e. prefab).
	/// </summary>
	public class GenericUpdater : AbstractWithLog, IPriorityRescalable
	{
		private readonly Part basePart;
		private readonly TweakScale ts;

		public GenericUpdater(Part part) : base(part)
		{
			this.basePart = PartLoader.getPartInfoByName(part.partInfo.name).partPrefab;
			this.ts = part.Modules.OfType<TweakScale>().First();
		}

		public void OnRescale(ScalingFactor factor)
		{
			Log.dbg("{0} OnRescale {1} {2}", this.GetType().FullName, this.InstanceID, factor);
			ScaleExponents.UpdateObject(this.part, this.basePart, this.ts.ScaleType.Exponents, factor);
		}
	}
}
