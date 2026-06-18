/*
	This file is part of TweakScale™ /L
		© 2018-2026 LisiasT
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

namespace TweakScale.Lib
{
	public static class GameDatabase
	{
		public static ConfigNode GetPart(string name)
		{
			Log.dbg("Lib.GameDatabase.GetPart {0}", name);
			UrlDir.UrlConfig[] nodes = global::GameDatabase.Instance.GetConfigs("PART");
			for (int i = 0; i < nodes.Length; ++i) if (name.Equals(nodes[i].name.Replace('_', '.')))
				return nodes[i].config;
			return null;
		}

		public static ConfigNode GetPartModule(string partName, string moduleName)
		{
			Log.dbg("Lib.GameDatabase.GetPartModule {0} {1}", partName, moduleName);
			ConfigNode partNode = GetPart(partName);
			ConfigNode[] nodes = partNode.GetNodes("MODULE");
			for (int i = 0; i < nodes.Length; ++i)
			{
				string name = nodes[i].GetValue("name");
				if (moduleName.Equals(name))
					return nodes[i];
			}
			return null;
		}

		public static TweakScale GetTweakScaleModules(global::Part part)
		{
			System.Collections.Generic.List<TweakScale> list = part.Modules.GetModules<TweakScale>();
			if (0 == list.Count) return null;
			return list[0];
		}
	}
}
