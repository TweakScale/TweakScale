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
using UnityEngine;

namespace TweakScale.Features
{
	public static class ResetOnNew
	{
		private static bool _active = true;
		public static bool Active
		{
			get => _active;
			set
			{
				if (value == _active) return;
				_active = value;
				save();
			}
		}

		public static void Init() { }

		public static void DeInit() { }

		public static void Execute()
		{
			AutoScale.Active = false;
			ScaleChaining.Active = false;
		}

		private static void save()
		{
			KSP.IO.PluginConfiguration config = HotkeyManager.Instance.Config;
			config.SetValue("Reset on New or Load", _active);
			config.save();
		}
	}
}
