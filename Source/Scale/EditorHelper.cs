/*
	This file is part of TweakScale™ /L
		© 2018-2024 LisiasT
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
using System;

using KSP.UI.Screens;

using KSPe.Annotations;

namespace TweakScale
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	internal class GameEventEditorListener : SingletonBehavior<GameEventEditorListener>
	{
		[UsedImplicitly]
		new private void Awake()
		{
			base.Awake(); DontDestroyOnLoad(this);
			Log.dbg("GameEventEditorListener was awaken.");
			GameEvents.onEditorLoad.Add(this.OnEditorLoad);
			GameEvents.onEditorNewShipDialogDismiss.Add(this.OnEditorNewShipDialogDismiss);
		}

		[UsedImplicitly]
		private void Destroy()
		{
			Log.dbg("GameEventEditorVariantAppliedListener was destroyed.");
			GameEvents.onEditorNewShipDialogDismiss.Remove(this.OnEditorNewShipDialogDismiss);
			GameEvents.onEditorLoad.Remove(this.OnEditorLoad);
		}

		private void OnEditorLoad(ShipConstruct shipConstruct, CraftBrowserDialog.LoadType loadType) => ResetOnNew();
		private void OnEditorNewShipDialogDismiss() => this.ResetOnNew();

		private void ResetOnNew()
		{
			if (Features.ResetOnNew.Active)
				Features.ResetOnNew.Execute();
		}
	}
}
