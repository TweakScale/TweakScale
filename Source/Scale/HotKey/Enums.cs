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
using System;
using System.Collections.Generic;

namespace TweakScale
{
    internal abstract class Enums<T> where T : class
    {
        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="TEnumType">The type of the enumeration.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <exception cref="System.ArgumentNullException">value is null</exception>
        /// <exception cref="System.ArgumentException">value is either an empty string or only contains white space.-or- value is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="System.OverflowException">value is outside the range of the underlying type of EnumType</exception>
        /// <returns>An object of type enumType whose value is represented by value.</returns>
        static public TEnumType Parse<TEnumType>(string value) where TEnumType : T
        {
            return (TEnumType)Enum.Parse(typeof(TEnumType), value);
        }
    }

    abstract class Enums : Enums<Enum>
    {
    }

    static partial class ExtensionMethods
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> list)
        {
			IEnumerator<T> enumerator = list.GetEnumerator();
			if (!enumerator.MoveNext())
				yield break;
			T curr = enumerator.Current;
            while (enumerator.MoveNext())
            {
                yield return curr;
                curr = enumerator.Current;
            }
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> list, int n)
        {
			IEnumerator<T> enumerator = list.GetEnumerator();
			T[] buffer = new T[n];
			int idx = 0;
            while (enumerator.MoveNext() && idx < n)
            {
                buffer[idx] = enumerator.Current;
                idx++;
            }
            idx = 0;
            do
            {
                yield return buffer[idx];
                buffer[idx] = enumerator.Current;
                idx++;
                if (idx >= n)
                {
                    idx = 0;
                }
            }
            while (enumerator.MoveNext());
        }
    }
}
