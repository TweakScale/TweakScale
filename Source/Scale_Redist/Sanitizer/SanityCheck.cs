/*
	This file is part of TweakScale /L
		© 2018-2022 LisiasT
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
namespace TweakScale.Sanitizer
{
	public enum Priority
	{
		ShowStopper = -1,	// Show Stoppers need special handling
		Critical = 0,
		High = 333,
		Normal = 666,
		Low = 999,
		__SIZE = 1000,	// Placeholder. NOT TO BE USED
		__MIN = 0		// Placeholder. NOT TO BE USED
	}

	public interface SanityCheck
	{
		Priority Priority { get ; }
		string Summary { get; }
		int Ocurrences { get; }	// How many ocurrences of the problem were detected
		int Unscalable { get; }	// From that occurrences, how many parts lost the TweakScale module
		int Failures { get; }	// How many failures (on checking the damned thing) happened
		bool Check(AvailablePart p, Part prefab);		// Returns true if checks should stop for this part, false otherwise
		bool EmmitMessageIfNeeded(bool showMessageBox);	// Returns true if a message was displayed (or would had)
	}
}
