/*
	This file is part of TweakScale /L
		© 2018-2022 LisiasT

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
namespace TweakScale.Sanitizer
{
	public abstract class Abstract : SanityCheck
	{
		public abstract Priority Priority { get; }
		public abstract string Summary { get; }
		public abstract int Ocurrences { get; }
		public abstract int Unscalable { get; }
		public abstract int Failures { get; }

		protected abstract bool DoCheck(AvailablePart p, Part prefab);
		public bool Check(AvailablePart p, Part prefab)
		{
			Log.trace("{0} is checking {1}...", this.GetType().Name, p.name);
			bool r = this.DoCheck(p, prefab);
			Log.trace("{0} was checked and it should{1} stop the chain.", p.name, r ? "" : " not");
			return r;
        }

		public abstract bool EmmitMessageIfNeeded(bool showMessageBox);

		protected Abstract() {
			Log.detail("{0} instantiated.", this.GetType().Name);
		}

		protected static ConfigNode GetMeThatConfigNode(Part p)
		{
			AvailablePart ap = p.partInfo;
			if (null == ap)
			{
				Log.warn("NULL partInfo for {0}!! Something is *really* messed up with this part!!", p.partName);
				return null;
			}

			// Check the forum for the rationale:
			//	* https://forum.kerbalspaceprogram.com/index.php?/topic/7542-the-official-unoffical-quothelp-a-fellow-plugin-developerquot-thread/&do=findComment&comment=3631853
			//	* https://forum.kerbalspaceprogram.com/index.php?/topic/7542-the-official-unoffical-quothelp-a-fellow-plugin-developerquot-thread/&do=findComment&comment=3631908
			//	* https://forum.kerbalspaceprogram.com/index.php?/topic/7542-the-official-unoffical-quothelp-a-fellow-plugin-developerquot-thread/&do=findComment&comment=3632139

			// First try the canonnical way - there must be a config file somewhere!
			ConfigNode r = GameDatabase.Instance.GetConfigNode(p.partInfo.partUrl);
			if (null == r)
			{
				// But if that doesn't works, let's try the partConfig directly.
				//
				// I have reasons to believe that partConfig may not be an identical copy from the Config since Making History
				// (but I have, by now, no hard evidences yet) - but I try first the config file nevertheless. There's no point]
				// on risking pinpointing something that cannot be found on the config file.
				//
				// What will happen if the problems start to appear on the partConfig and not in the config file is something I
				// don't dare to imagine...
				Log.warn("NULL ConfigNode for {0} (unholy characters on the name?). Trying partConfig instead!", p.partInfo.partUrl);
				r = p.partInfo.partConfig;
			}

			return r;
		}
	}
}
