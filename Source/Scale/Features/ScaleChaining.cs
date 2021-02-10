/*
		This file is part of TweakScale /L
		© 2018-2021 LisiasT
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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TweakScale.Features
{
	internal static class ScaleChaining
	{
		private static Hotkeyable hotkeyable;
		public static bool Enabled => (hotkeyable != null && hotkeyable.State);
		private static int counter = 0;

		public static void Init()
		{
			++counter;
			if (counter > 1) return;
			hotkeyable = HotkeyManager.Instance.AddHotkey(
				"Scale chaining", new[] {KeyCode.LeftShift}, new[] {KeyCode.LeftControl, KeyCode.K}, false
				);
		}

		public static void DeInit()
		{
			--counter;
			if (counter < 1)
			{
				// ????
			}
		}

		public static void Execute(TweakScale father)
		{
			HandleChildrenScaling(father.part, father.ScalingFactor.relative.linear);
		}

		/// <summary>
		/// Propagate relative scaling factor to children.
		/// </summary>
		private static void HandleChildrenScaling(Part father, float factor)
		{
			HashSet<Part> rejected = new HashSet<Part>();
			HashSet<Part> parts = new HashSet<Part>();
			{
				int len = father.children.Count;
				for (int i = 0; i < len; ++i) parts.Add(father.children[i]);
			}
			while (parts.Count > 0) {
				foreach (Part p in parts) {
					if (!HandleChildScaling(p, factor)) {
						// if we get here, the part is not scalable. So we need to handle their children ourselves
						int len = p.children.Count;
						for (int i = 0; i < len; ++i) rejected.Add(p.children[i]);
					}
				}
				parts.Clear();
				parts.UnionWith(rejected);
				rejected.Clear();
			}
		}

		private static bool HandleChildScaling(Part child, float factor)
		{
			TweakScale b = child.GetComponent<TweakScale>();
			if (null == b) {
				Log.dbg("Ignoring child scaling {0}:{1}", child.name, child.persistentId);
				return false;
			}
			Log.dbg("Handling child scaling {0}:{1}", child.name, child.persistentId);
			if (Math.Abs(factor - 1) <= 1e-4f) return true;

			b.tweakScale *= factor;
			if (!b.isFreeScale && (b.ScaleFactors.Length > 0)) {
				b.tweakName = Tools.ClosestIndex(b.tweakScale, b.ScaleFactors);
			}
			b.OnTweakScaleChanged();
			return true;
		}
	}
}
