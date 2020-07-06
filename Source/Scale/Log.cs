/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

		THIE FILE is licensed to you under:

		* WTFPL - http://www.wtfpl.net
			* Everyone is permitted to copy and distribute verbatim or modified
 			    copies of this license document, and changing it is allowed as long
				as the name is changed.

		THIE FILE is distributed in the hope that it will be useful,
		but WITHOUT ANY WARRANTY; without even the implied warranty of
		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/
using System;
using KSPe.Util.Log;
using System.Diagnostics;

namespace TweakScale
{
    internal static class Log
    {
        private static readonly Logger log = Logger.CreateForType<TweakScale>();

        internal static void init()
        {
            log.level =
#if DEBUG
                Level.TRACE
#else
                Level.INFO
#endif
                ;
        }

        internal static void force (string msg, params object [] @params)
        {
            log.force (msg, @params);
        }

        internal static void info(string msg, params object[] @params)
        {
            log.info(msg, @params);
        }

        internal static void warn(string msg, params object[] @params)
        {
            log.warn(msg, @params);
        }

        internal static void detail(string msg, params object[] @params)
        {
            log.detail(msg, @params);
        }

        internal static void error(string msg, params object[] @params)
        {
            log.error(msg, @params);
        }

        [ConditionalAttribute("DEBUG")]
        internal static void dbg(string msg, params object[] @params)
        {
            log.trace(msg, @params);
        }
    }
}
