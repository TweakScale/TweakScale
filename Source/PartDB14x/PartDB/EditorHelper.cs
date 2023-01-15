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
using System.Collections.Generic;
using UnityEngine;
using KSPe.Annotations;

namespace TweakScale.PartDB
{
	internal class GameEventEditorVariantAppliedListener : MonoBehaviour
	{
		private static GameEventEditorVariantAppliedListener instance;
		internal static GameEventEditorVariantAppliedListener Instance { get {
			if (null != instance) return instance;
			GameObject ob = new GameObject();
			instance = ob.AddComponent<GameEventEditorVariantAppliedListener>();
			return instance;
		} }

		private readonly HashSet<VariantPartScaler> listeners = new HashSet<VariantPartScaler>();

		private GameEventEditorVariantAppliedListener()
		{
			Log.dbg("GameEventEditorVariantAppliedListener was instanced.");
		}

		internal void Add(VariantPartScaler listener)
		{
			this.listeners.Add(listener);
		}

		internal void Remove(VariantPartScaler listener)
		{
			if (this.listeners.Contains(listener)) this.listeners.Remove(listener);
		}

		[UsedImplicitly]
		private void Awake()
		{
			Log.dbg("GameEventEditorVariantAppliedListener was awaken.");
			GameEvents.onEditorVariantApplied.Add(this.EditorVariantAppliedHandler);
		}

		[UsedImplicitly]
		private void Destroy()
		{
			Log.dbg("GameEventEditorVariantAppliedListener was destroyed.");
			GameEvents.onEditorVariantApplied.Remove(this.EditorVariantAppliedHandler);
			this.listeners.Clear();
			instance = null;
		}

		[UsedImplicitly]
		internal void EditorVariantAppliedHandler(Part part, PartVariant partVariant)
		{
			if (null == part || null == partVariant )
			{
				Log.dbg("part or partVariant is null! Aborting EditorVariantAppliedHandler. {0} {1}", part, partVariant);
				return;
			}
			Log.dbg("Variant {0} applied to {1}::{2:X}", partVariant.DisplayName, part.name, part.GetInstanceID());
			foreach (VariantPartScaler ps in this.listeners) if (ps.enabled && ps.IsMine(part))
				ps.OnEditorVariantApplied(part, partVariant);
		}
	}
}
