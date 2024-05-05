/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
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

namespace TweakScale
{
	/// <summary>
	/// For Rescaling the <c>Part</c> itself or some of its attributes. It's usually better to avoid using this one when possible, as it will be injected on every part of the craft no matter what.
	///
	/// There's no guarantee about the order in which you will be called, other than after the <c>IPriorityRescalable</c> and before <c>ISecondaryRescalable</c> ones.
	/// </summary>
	public interface IRescalable
	{
		void OnRescale(ScalingFactor factor);
	}
	/// <summary>
	/// A <c>IRescalable</c> for a specific <c>PartModule</c>.
	///
	/// Should be used instead of the <c>Part</c> counterpart when possible, as it will be injected only when the target <c>PartModule</c> is present.
	/// 
	/// Prefferable method to support TweakScale.
	/// </summary>
	public interface IRescalable<T> : IRescalable { }

	/// <summary>
	/// For Priority Rescaling the <c>Part</c> itself or some of its attributes. Usually should not be used by 3rd Parties, unless they are sure the Rescalers implemented on Stock
	/// will not cause conflicts with the new code.
	///
	/// There's no way to tell if the Stock Priority Rescaling will be called after or before yours.
	/// </summary>
	public interface IPriorityRescalable : IRescalable { }
	/// <summary>
	/// Priority Rescaling Support.
	/// You should probably avoid this one, and if you think you need it, you <u><b>should not</b></u> access any of the `this.part` properties
	/// as there's no guarantee that they are already scaled.
	/// The reason for this IRescalable variant is to allow custom's PART scallers in the future.
	/// </summary>
	public interface IPriorityRescalable<T> : IPriorityRescalable { }

	/// <summary>
	/// A shortcut for <c>ISecondaryRescalable&lt;Part&gt;</c>
	/// </summary>
	public interface ISecondaryRescalable : IRescalable { }
	/// <summary>
	/// Secondary Rescaling Support.
	/// For scaling non physical atributes, as visuals (emitters) or anything that should be called <b>only</b> after all physical attributes
	/// were already scalled.
	/// </summary>
	public interface ISecondaryRescalable<T> : IPriorityRescalable { }

	/// <summary>
	/// For Rescallers that need to be reworked on every single Unity's Update.
	/// </summary>
	public interface IUpdateable
	{
		void OnUpdate();
	}

}