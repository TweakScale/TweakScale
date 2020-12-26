/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
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
using System.Collections.Generic;
using KSP.IO;
using TweakScale.Annotations;
using UnityEngine;

namespace TweakScale
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    internal class HotkeyManager : SingletonBehavior<HotkeyManager>
    {
        private readonly OSD _osd = new OSD();
        private readonly Dictionary<string, Hotkeyable> _hotkeys = new Dictionary<string, Hotkeyable>();
        private /*readonly*/ PluginConfiguration _config;

		private new void Awake()
		{
            base.Awake();

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
            foreach (Hotkeyable key in _hotkeys.Values)
            {
                key.Update();
            }
        }

        public Hotkeyable AddHotkey(string hotkeyName, ICollection<KeyCode> tempDisableDefault, ICollection<KeyCode> toggleDefault, bool state)
        {
            if (_hotkeys.ContainsKey(hotkeyName))
                return _hotkeys[hotkeyName];
            return _hotkeys[hotkeyName] = new Hotkeyable(_osd, hotkeyName, tempDisableDefault, toggleDefault, state);
        }
    }
}
