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
using UnityEngine;
using UGUI = UnityEngine.GUI;
using KSPe.Annotations;

namespace TweakScale.GUI
{
	[KSPAddon(KSPAddon.Startup.EditorAny, false)]
	internal class SettingsGui : MonoBehaviour
	{
		private static SettingsGui instance = null;
		internal static SettingsGui Instance => instance;

		protected Rect windowPos = new Rect(Screen.width / 4, Screen.height / 4, 10f, 10f);
		private int windowID = 0;
		private bool showGUI = false;
		internal bool Active {
				get => this.enabled;
				set
				{
					if (value) ReadSettings();
					else ApplySettings();
					this.enabled = value;
				}
			}

		private bool autoScaleActive = Features.AutoScale.Active;
		private bool scaleChaining = Features.AutoScale.Active;
		private bool resetOnNew = Features.ResetOnNew.Active;

		[UsedImplicitly]
		private void Awake() {
			instance = this;
			this.windowID = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			Log.trace("SettingsGui awake - " + this.GetInstanceID());
		}

		[UsedImplicitly]
		private void Start()
		{
			this.enabled = false;
			this.showGUI = true;
			GameEvents.onShowUI.Add(this.OnShowUI);
			GameEvents.onHideUI.Add(this.OnHideUI);
		}

		[UsedImplicitly]
		private void OnDestroy() {
			instance = null;
			GameEvents.onHideUI.Remove(this.OnHideUI);
			GameEvents.onShowUI.Remove(this.OnShowUI);
		}

		[UsedImplicitly]
		private void OnShowUI() {
			this.showGUI = true;
		}

		[UsedImplicitly]
		private void OnHideUI() {
			this.showGUI = false;
		}

		[UsedImplicitly]
		private void OnGUI() {
			if (!(this.showGUI && this.Active)) return;
			windowPos = GUILayout.Window(this.windowID, windowPos, mainGUI, typeof(Version).Namespace + " " + Version.Text, GUILayout.Width(300), GUILayout.Height(200));
		}

		private void ApplySettings() {
			Features.AutoScale.Active = this.autoScaleActive;
			Features.ScaleChaining.Active = this.scaleChaining;
			Features.ResetOnNew.Active = this.resetOnNew;
		}

		private void ReadSettings() {
			this.autoScaleActive = Features.AutoScale.Active;
			this.scaleChaining = Features.ScaleChaining.Active;
			this.resetOnNew = Features.ResetOnNew.Active;
		}

		private void mainGUI(int windowID) {
			GUIStyle styleWindow = new GUIStyle(UGUI.skin.window);
			styleWindow.padding.left = 4;
			styleWindow.padding.top = 4;
			styleWindow.padding.bottom = 4;
			styleWindow.padding.right = 4;

			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
			GUILayout.Label("");
			GUILayout.EndHorizontal();

			//--- Features --------------------------------------------
			GUILayout.BeginVertical("Features", new GUIStyle(UGUI.skin.window));
			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
			this.autoScaleActive = GUILayout.Toggle(this.autoScaleActive, "Enable Auto Scale");
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
			this.scaleChaining = GUILayout.Toggle(this.scaleChaining, "Enable Chain Scale");
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();

			GUILayout.BeginVertical("General", new GUIStyle(UGUI.skin.window));
			GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
			this.resetOnNew = GUILayout.Toggle(this.resetOnNew, "Reset the Settings on New/Load Craft");
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();

			GUILayout.EndVertical();
			UGUI.DragWindow();
		}
	}
}
