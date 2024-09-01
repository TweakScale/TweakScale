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
using System.Collections.Generic;
using UnityEngine;
using KSP.IO;
using KSPe.Annotations;

namespace TweakScale
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	internal class HotkeyManager : SingletonBehavior<HotkeyManager>
	{
		private readonly object MUTEX = new object();
		private readonly OSD _osd = new OSD();
		private readonly Dictionary<string, Hotkeyable> _hotkeys = new Dictionary<string, Hotkeyable>();
		private /*readonly*/ PluginConfiguration _config;

		[UsedImplicitly]
		private new void Awake()
		{
			Log.dbg("HotkeyManager.Awake");
			base.Awake();
			DontDestroyOnLoad(this);

			_config = PluginConfiguration.CreateForType<TweakScale>();
		}

		public PluginConfiguration Config => _config;

		[UsedImplicitly]
		private void OnGUI()
		{
			_osd.Update();
		}

		[UsedImplicitly]
		private void Update()
		{
			foreach (Hotkeyable key in _hotkeys.Values) {
				key.Update();
			}
		}

		public Hotkeyable AddHotkey(string hotkeyName, ICollection<KeyCode> tempDisableDefault, ICollection<KeyCode> toggleDefault, bool state)
		{
			lock (MUTEX) {
				if (_hotkeys.ContainsKey(hotkeyName))
					return _hotkeys[hotkeyName];
				return _hotkeys[hotkeyName] = new Hotkeyable(_osd, hotkeyName, tempDisableDefault, toggleDefault, state);
			}
		}

		public void RemoveHotkey(Hotkeyable hotKey) => this.RemoveHotkey(hotKey.Name);
		public void RemoveHotkey(string hotkeyName)
		{
			lock (MUTEX)
			{
				if (_hotkeys.ContainsKey(hotkeyName))
					_hotkeys.Remove(hotkeyName);
			}
		}
	}
}
