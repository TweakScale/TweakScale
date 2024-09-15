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
using KSPe.Annotations;
using System;

namespace TweakScale
{
	internal abstract class SingletonBehavior<T> : MonoBehaviour where T : SingletonBehavior<T>
	{
		// NOTE: THIS IS NOT THREAD SAFE!
		private static T instance = null;
		public static T Instance {
			get {
				return instance;
			}
		}

		protected abstract void DoAwake();
		protected abstract void DoStart();
		protected abstract void DoDestroy();


		[UsedImplicitly]
		protected void Awake()
		{
			Log.dbg("SingletonBehavior.Awake: {0}", this.GetType().FullName);
			instance = (T)this;
			this.DoAwake();
		}

		[UsedImplicitly]
		protected void Start()
		{
			Log.dbg("SingletonBehavior.Start: {0}", this.GetType().FullName);
			// Guarantees that OnDestroy will be called.
			this.enabled = true;
			this.DoStart();
		}

		[UsedImplicitly]
		protected void OnDestroy()
		{
			Log.dbg("SingletonBehavior.OnDestroy: {0}", this.GetType().FullName);
			this.DoDestroy();
			instance = null;
		}
	}
}
