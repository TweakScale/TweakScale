/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
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
	along with TweakScale /L. If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TweakScale.Updater
{
	public class AbstractWithLog : Abstract
	{
		public AbstractWithLog(Part part) : base(part)
		{
			Log.dbg("{0} Ctor {1}", this.GetType().FullName, this.InstanceID);
		}

		public AbstractWithLog(PartModule partModule) : base(partModule)
		{
			Log.dbg("{0} Ctor {1}", this.GetType().FullName, this.InstanceID);
		}

		private string _InstanceID = null;
		public string InstanceID => this._InstanceID ?? (this._InstanceID = string.Format("{0}:{1:X}", this.part.name, this.part.GetInstanceID()));
	}

	internal static class Registry
	{
		public static void Init()
		{
			Log.detail("TweakScale.Updater.Registry.Init()");
			IEnumerable<Type> genericRescalable = KSPe.Util.SystemTools.Type.Search.By(typeof(IRescalable));

			foreach(Type gen in genericRescalable)
			{
				Log.dbg("TweakScale.Updater.Registry.Init {0}", gen);
				if (IsGenericRescalable(gen))
				{
					Type t = gen.GetInterfaces()
						.First(a => a.IsGenericType &&
						a.GetGenericTypeDefinition() == typeof(IRescalable<>));

					RegisterGenericRescalable(gen, t.GetGenericArguments()[0]);
				}
				else
				{
					RegisterRescalable(gen);
				}
			}
		}

		private static void RegisterGenericRescalable(Type resc, Type arg)
		{
			ConstructorInfo c = resc.GetConstructor(new[] { arg });
			if (c == null) return;
			Log.dbg("TweakScale.Updater.Registry.RegisterGenericRescalable {0}, {1}", resc, arg);

			IRescalable creator(PartModule pm) => (IRescalable)c.Invoke(new object[] { pm });
			Factory.RegisterUpdater(arg, creator);
		}

		private static void RegisterRescalable(Type resc)
		{
			ConstructorInfo c = resc.GetConstructor(new[] { typeof(Part) });
			if (c == null) return;

			Log.dbg("TweakScale.Updater.Registry.RegisterGenericRescalable {0}", resc);

			IRescalable creator(Part part) => (IRescalable)c.Invoke(new object[] { part });
			Factory.RegisterUpdater(creator);
		}

		private static bool IsGenericRescalable(Type t)
		{
			return !t.IsGenericType && t.GetInterfaces()
				.Any(a => a.IsGenericType &&
				a.GetGenericTypeDefinition() == typeof(IRescalable<>));
		}
	}

	internal static class Factory
	{
		// Every kind of updater is registered here, and the correct kind of updater is created for each PartModule.
		private static readonly Dictionary<Type, Func<PartModule, IRescalable>> partModuleCtors = new Dictionary<Type, Func<PartModule, IRescalable>>();
		// And here go the Part Updaters
		private static readonly List<Func<Part, IRescalable>> partCtors = new List<Func<Part, IRescalable>>();

		/// <summary>
		/// Registers an updater for partmodules of type <paramref name="pm"/>.
		/// </summary>
		/// <param name="pm">Type of the PartModule type to update.</param>
		/// <param name="creator">A function that creates an updater for this PartModule type.</param>
		public static void RegisterUpdater(Type pm, Func<PartModule, IRescalable> creator)
		{
			partModuleCtors[pm] = creator;
		}

		/// <summary>
		/// Registers an updater for parts..
		/// </summary>
		/// <param name="pm">Type of the PartModule type to update.</param>
		/// <param name="creator">A function that creates an updater for this PartModule type.</param>
		public static void RegisterUpdater(Func<Part, IRescalable> creator)
		{
			partCtors.Add(creator);
		}

		// Creates an updater for each modules attached to destination part.
		public static IEnumerable<IRescalable> CreateUpdaters(Part part)
		{
			{
				IEnumerable<IRescalable> updaters = part
					.Modules.Cast<PartModule>()
					.Select(CreateUpdater)
					.Where(updater => updater != null);
				foreach(IRescalable updater in updaters)
					yield return updater;
			}
			{
				foreach(Func<Part, IRescalable> updater in partCtors)
					yield return updater(part);
			}
		}

		private static IRescalable CreateUpdater(PartModule module)
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			if(module is IRescalable updater)
			{
				return updater;
			}
			return partModuleCtors.ContainsKey(module.GetType()) ? partModuleCtors[module.GetType()](module) : null;
		}
	}
}