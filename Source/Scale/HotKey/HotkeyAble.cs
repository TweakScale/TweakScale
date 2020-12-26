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
using UnityEngine;
using KSP.IO;

namespace TweakScale
{
    class Hotkeyable
    {
        private readonly OSD _osd;
        private readonly string _name;
        private readonly Hotkey _tempDisable;
        private readonly Hotkey _toggle;
        private bool _state;
        private readonly PluginConfiguration _config;

        public bool State {
            get { return _state && !_tempDisable.IsTriggered; }
        }

        public Hotkeyable(OSD osd, string name, ICollection<KeyCode> tempDisableDefault, ICollection<KeyCode> toggleDefault, bool state)
        {
            _config = HotkeyManager.Instance.Config;
            _osd = osd;
            _name = name;
            _tempDisable = new Hotkey("Disable " + name, tempDisableDefault);
            _toggle = new Hotkey("Toggle " + name, toggleDefault);
            _state = state;
            Load();
        }

        public Hotkeyable(OSD osd, string name, string tempDisableDefault, string toggleDefault, bool state)
        {
            _config = HotkeyManager.Instance.Config;
            _osd = osd;
            _name = name;
            _tempDisable = new Hotkey("Disable " + name, tempDisableDefault);
            _toggle = new Hotkey("Toggle " + name, toggleDefault);
            _state = state;
            Load();
        }

        private void Load()
        {
            Log.dbg("Getting value. Currently: {0}", _state);
            _state = _config.GetValue(_name, _state);
            Log.dbg("New value: {0}", _state);

            _config.SetValue(_name, _state);
            _config.save();
        }

        public void Update()
        {
            if (!_toggle.IsTriggered)
                return;
            _state = !_state;
            _osd.Info(_name + (_state ? " enabled." : " disabled."));
            _config.SetValue(_name, _state);
            _config.save();
        }

        public static bool operator true(Hotkeyable a)
        {
            return a.State;
        }

        public static bool operator false(Hotkeyable a)
        {
            return !a.State;
        }
    }
}
