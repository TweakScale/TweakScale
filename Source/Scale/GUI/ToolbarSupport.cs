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

using KSP.UI.Screens;

using KSPe.Annotations;

using UnityEngine;

namespace TweakScale.GUI
{
	[KSPAddon(KSPAddon.Startup.EditorAny, false)]
	internal class ToolbarSupport : MonoBehaviour
	{
		private static ToolbarSupport instance = null;
		internal static ToolbarSupport Instance => instance;
		private ApplicationLauncherButton button;

		[UsedImplicitly]
		private void Awake() {
			instance = this;
		}

		[UsedImplicitly]
		private void Start() {
			GameEvents.onGUIApplicationLauncherReady.Add(this.OnGUIApplicationLauncherReady);
			GameEvents.onGUIApplicationLauncherDestroyed.Add(this.OnGUIApplicationLauncherDestroyed);
			GameEvents.onEditorPartPicked.Add(this.OnEditorPartPicked);
			GameEvents.onEditorPartPlaced.Add(this.OnEditorPartPlaced);
		}

		[UsedImplicitly]
		private void OnDestroy() {
			instance = null;
			this.OnGUIApplicationLauncherDestroyed();	// Force the destruction of the Current Button
			GameEvents.onEditorPartPlaced.Remove(this.OnEditorPartPlaced);
			GameEvents.onEditorPartPicked.Remove(this.OnEditorPartPicked);
			GameEvents.onGUIApplicationLauncherDestroyed.Remove(this.OnGUIApplicationLauncherDestroyed);
			GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIApplicationLauncherReady);
		}

		private void OnGUIApplicationLauncherReady() {
			if (null == this.button)
				this.button = ApplicationLauncher.Instance.AddModApplication(
							this.OnTrue, this.OnFalse
							, this.Dummy, this.Dummy
							, this.Dummy, this.Dummy
							, ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.VAB
							, GUI.Icons.ScaleOff
						)
					;
			this.UpdateIcon();
		}

		internal void UpdateIcon() {
			if (Features.AutoScale.Enabled || Features.ScaleChaining.Enabled)
				this.button.SetTexture(GUI.Icons.ScaleAuto);
			else
				this.button.SetTexture(GUI.Icons.ScaleOn);
		}

		internal void UpdateIcon(bool active, bool available) {
			if (active && available) this.UpdateIcon();
			else this.button.SetTexture(GUI.Icons.ScaleOff);
		}

		private void OnEditorPartPicked(Part part) {
			if (part.Modules.Contains<TweakScale>())
				this.UpdateIcon();
			else
				this.button.SetTexture(GUI.Icons.ScaleUnsupported);
		}

		private void OnEditorPartPlaced(Part part) {
			if (part.Modules.Contains<TweakScale>())
				this.UpdateIcon();
			else
				this.button.SetTexture(GUI.Icons.ScaleUnsupported);
		}

		private void OnGUIApplicationLauncherDestroyed() {
			if (null == this.button) return;
			ApplicationLauncher.Instance.RemoveModApplication(this.button);
			Destroy(this.button);
			this.button = null;
		}

		private void Dummy() {
			// Dummy Bears!!! :P
		}

		private void OnTrue() {
			if (null == SettingsGui.Instance) return; // Better safe than sorry.
			SettingsGui.Instance.Active = true;
		}

		private void OnFalse() {
			if (null == SettingsGui.Instance) return; // Better safe than sorry.
			SettingsGui.Instance.Active = false;
		}
	}
}
