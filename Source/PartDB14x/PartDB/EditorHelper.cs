/*
	This file is part of TweakScale /L
		© 2018-2022 LisiasT
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
			GameEvents.onEditorVariantApplied.Add(this.EditorVariantAppliedHandler);
		}

		[UsedImplicitly]
		private void Destroy()
		{
			GameEvents.onEditorVariantApplied.Remove(this.EditorVariantAppliedHandler);
			this.listeners.Clear();
		}

		[UsedImplicitly]
		internal void EditorVariantAppliedHandler(Part part, PartVariant partVariant)
		{
			foreach (VariantPartScaler ps in this.listeners) if (ps.enabled && ps.IsMine(part))
				ps.OnEditorVariantApplied(part, partVariant);
		}
	}
}
