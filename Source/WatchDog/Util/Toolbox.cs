/*
	This file is part of TweakScale Watch Dog
		© 2023 Lisias T : http://lisias.net <support@lisias.net>

	TweakScale Watch Dog is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	TweakScale Watchdog is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with Module Manager Watch Dog. If not, see
	<https://ksp.lisias.net/SKL-1_0.txt>.

*/
using System;
namespace TweakScale.WatchDog.Util
{
	internal static class Toolbox
	{
		// Boyer-Moore-Horspool Algorithm for All Matches (Find Byte array inside Byte array)
		public static Int64 IndexOf(this Byte[] value, Byte[] pattern)
		{
			if (null == value) return -1;
			if (null == pattern) return 0;

			Int64 valueLength = value.LongLength;
			Int64 patternLength = pattern.LongLength;

			if ((valueLength == 0) || (patternLength == 0) || (patternLength > valueLength))
				return -1;

			Int64[] badCharacters = new Int64[256];

			for (Int64 i = 0; i < 256; ++i)
				badCharacters[i] = patternLength;

			Int64 lastPatternByte = patternLength - 1;

			for (Int64 i = 0; i < lastPatternByte; ++i)
				badCharacters[pattern[i]] = lastPatternByte - i;

			Int64 index = 0;

			while (index <= (valueLength - patternLength))
			{
				for (Int64 i = lastPatternByte; value[(index + i)] == pattern[i]; --i)
					if (i == 0) return index;
				index += badCharacters[value[(index + lastPatternByte)]];
			}

			return -1;
		}
	}
}
