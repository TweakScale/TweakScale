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
using UnityEngine;

namespace TweakScale.Features
{
	internal static class AutoScale
	{
		internal static Hotkeyable hotkeyable { get; private set; }
		public static bool Available => (hotkeyable != null);
		public static bool Enabled => (Available && Active);
		public static bool Active
		{
			get => hotkeyable.State;
			set
			{
				if (value == hotkeyable.State) return;
				hotkeyable.State = value;
			}
		}

		public static void Init()
		{
			if (null != hotkeyable) return;
			hotkeyable = HotkeyManager.Instance.AddHotkey(
				"Autoscale", new[] {KeyCode.LeftShift},	new[] {KeyCode.LeftControl, KeyCode.L}, false
				);
		}

		public static void DeInit()
		{
			if (null == hotkeyable) return;
			HotkeyManager.Instance.RemoveHotkey(hotkeyable);
			hotkeyable = null;
		}

		private static AttachNode FindAttachNodeByPart(Part a, Part b)
		{
			foreach (AttachNode an in a.attachNodes)
				if (an.attachedPart == b) return an;
			return null;
		}

		/// <summary>
		/// Find the Attachnode that fastens <paramref name="a"/> to <paramref name="b"/> and vice versa.
		/// </summary>
		/// <param name="a">The source part (often the parent)</param>
		/// <param name="b">The target part (often the child)</param>
		/// <returns>The AttachNodes between the two parts.</returns>
		private static Tuple<AttachNode, AttachNode>? NodesBetween(Part a, Part b)
		{
			// This whole stunt is not working!!
			// the Part.FindAttachNodeByPart on NodesBetween() is always returning NULL.
			// This probably got broken on KSP 1.4.0, and never fixed.
			AttachNode nodeA = FindAttachNodeByPart(a,b);
			AttachNode nodeB = FindAttachNodeByPart(b,a);

			Log.dbg("AutoScale.NodesBetween {0} {1}", nodeA==null, nodeB==null);
			if (nodeA == null || nodeB == null)
				return null;

			return Tuple.Create(nodeA, nodeB);
		}

		/// <summary>
		/// Calculate the correct scale to use for scaling a part relative to another. NO NULL CHECK is done.
		/// </summary>
		/// <param name="a">Source part, from which we get the desired scale.</param>
		/// <param name="b">Target part, which will potentially be scaled.</param>
		/// <returns>The difference in scale between the (scaled) attachment nodes connecting <paramref name="a"/> and <paramref name="b"/>, or null if somethinng went wrong.</returns>
		private static float GetRelativeScaling(TweakScale a, TweakScale b)
		{
			// Let's hope things match when freeScaling.
			// The user would have to scale the thing manually anyway.
			if (a.ScaleType.IsFreeScale) return a.currentScale;

			Tuple<AttachNode, AttachNode>? nodes = NodesBetween(a.part, b.part);

			if (!nodes.HasValue)
				return 1f;

			AttachNode nodeA = nodes.Value.Item1;
			AttachNode nodeB = nodes.Value.Item2;

			int aIdx = a.scaler.prefab.attachNodes.FindIndex(t => t.id == nodeA.id);
			int bIdx = b.scaler.prefab.attachNodes.FindIndex(t => t.id == nodeB.id);
			if (aIdx < 0 || bIdx < 0
					|| aIdx >= a.scaler.prefab.attachNodes.Count
					|| aIdx >= a.scaler.prefab.attachNodes.Count
				)
				return 1f;

			float sizeA = (float)a.scaler.prefab.attachNodes[aIdx].size;
			float sizeB = (float)b.scaler.prefab.attachNodes[bIdx].size;

			if (sizeA == 0) sizeA = 0.5f;
			if (sizeB == 0) sizeB = 0.5f;

			return (sizeA * a.tweakScale / a.defaultScale) / (sizeB * b.tweakScale / b.defaultScale);
		}

		/// <summary>
		/// Automatically scale part to match other part, if applicable. NO NULL CHECK is done.
		/// </summary>
		/// <param name="a">Source part, from which we get the desired scale.</param>
		/// <param name="b">Target part, which will potentially be scaled.</param>
		public static void Execute(TweakScale a, TweakScale b)
		{
			Log.dbg("AutoScale.Execute {0} {1}", a, b);

			float factor = GetRelativeScaling(a,b);

			b.tweakScale = factor;
			Log.dbg("AutoScale.Execute factor.value {0} tweakScale {1}", factor, b.tweakScale);
			if (!b.isFreeScale && (b.ScaleFactors.Length > 0))
			{
				b.tweakName = Tools.ClosestIndex(b.tweakScale, b.ScaleFactors);
				Log.dbg("AutoScale.Execute tweakName {0}", b.tweakName);
			}
			b.OnTweakScaleChanged();
		}
	}

}
