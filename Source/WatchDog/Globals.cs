﻿/*
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
using KSPe;

namespace TweakScale
{
	public class Globals
	{
		private static Globals INSTANCE = null;
		public static Globals Instance => INSTANCE ?? (INSTANCE = new Globals());

		public readonly bool curseforge_ready;
		public readonly bool ckan_ready;

		private Globals()
		{
			try
			{
				UrlDir.UrlConfig urlc = GameDatabase.Instance.GetConfigs("TWEAKSCALE")[0];
				ConfigNode cn = urlc.config.GetNode("STATUS");
				this.curseforge_ready = "True".Equals(cn.GetValue("curseforge_ready"),StringComparison.InvariantCultureIgnoreCase);
				this.ckan_ready = "True".Equals(cn.GetValue("ckan_ready"),StringComparison.InvariantCultureIgnoreCase);
			}
			catch (Exception)
			{
				this.curseforge_ready = false;
				this.ckan_ready = false;
			}
		}
	}
}
