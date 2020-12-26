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
using System.Linq;
using UnityEngine;

using KSP.IO;

namespace TweakScale
{
    public class Hotkey
    {
        private readonly Dictionary<KeyCode, bool> _modifiers = new Dictionary<KeyCode, bool>();
        private KeyCode _trigger = KeyCode.None;
        private readonly string _name;
        private readonly PluginConfiguration _config;

        public Hotkey(string name, ICollection<KeyCode> defaultKey)
        {
            _config = HotkeyManager.Instance.Config;
            _name = name;
            if (defaultKey.Count == 0)
            {
                Log.warn("No keys for hotkey {0}. Need at least 1 key in defaultKey parameter, got none.", _name);
            }
            else
            {
                _trigger = defaultKey.Last();
                SetModifiers(defaultKey.SkipLast().ToList());
            }
            Load();
        }

        public Hotkey(string name, string defaultKey)
        {
            _config = HotkeyManager.Instance.Config;
            _name = name;
            ParseString(defaultKey);
            Load();
        }

        public void Load()
        {
            _config.load();
			string rawNames = _config.GetValue(_name, "");
            if (!string.IsNullOrEmpty(rawNames))
            {
                ParseString(rawNames);
            }
            Save();
        }

        private void ParseString(string s)
        {

			string[] names = s.Split('+');
			List<KeyCode> keys = names.Select(Enums.Parse<KeyCode>).ToList();
            _trigger = keys.Last();

            SetModifiers(keys.SkipLast().ToList());
        }

        private void Save()
        {
			string result = "";
            foreach (KeyValuePair<KeyCode, bool> kv in _modifiers)
                if (kv.Value)
                    result += kv.Key + "+";

            _config.SetValue(_name, result + _trigger);
            _config.save();
        }

        private void SetModifiers(ICollection<KeyCode> mods)
        {
            _modifiers[KeyCode.RightShift] = mods.Contains(KeyCode.RightShift);
            _modifiers[KeyCode.LeftShift] = mods.Contains(KeyCode.LeftShift);
            _modifiers[KeyCode.RightControl] = mods.Contains(KeyCode.RightControl);
            _modifiers[KeyCode.LeftControl] = mods.Contains(KeyCode.LeftControl);
            _modifiers[KeyCode.RightAlt] = mods.Contains(KeyCode.RightAlt);
            _modifiers[KeyCode.LeftAlt] = mods.Contains(KeyCode.LeftAlt);
            _modifiers[KeyCode.RightApple] = mods.Contains(KeyCode.RightApple);
            _modifiers[KeyCode.RightCommand] = mods.Contains(KeyCode.RightCommand);
            _modifiers[KeyCode.LeftApple] = mods.Contains(KeyCode.LeftApple);
            _modifiers[KeyCode.LeftCommand] = mods.Contains(KeyCode.LeftCommand);
            _modifiers[KeyCode.LeftWindows] = mods.Contains(KeyCode.LeftWindows);
            _modifiers[KeyCode.RightWindows] = mods.Contains(KeyCode.RightWindows);
        }

        public bool IsTriggered
        {
            get
            {
                return _modifiers.All(a => Input.GetKey(a.Key) == a.Value) && Input.GetKeyDown(_trigger);
            }
        }
    }
}
