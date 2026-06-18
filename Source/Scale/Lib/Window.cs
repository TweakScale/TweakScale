/*
	This file is part of TweakScale™ /L
		© 2018-2026 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

	TweakScale™ /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScale™ /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScale™ /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScale™ /L. If not, see <https://www.gnu.org/licenses/>.
*/

namespace TweakScale.Lib
{
	public static class Window
	{
		/// <summary>
		/// Marks the right-click window as dirty (i.e. tells it to update).
		/// </summary>
		public static void MarkDirty(global::Part part) // redraw the right-click window with the updated stats
		{
			UIPartActionWindow[] list = UnityEngine.Object.FindObjectsOfType<UIPartActionWindow>();
			for (int i = 0; i < list.Length; ++i) if (list[i].part == part)
			{
				// This causes the slider to be non-responsive - i.e. after you click once, you must click again, not drag the slider.
				list[i].displayDirty = true;
			}
		}
	}
}
