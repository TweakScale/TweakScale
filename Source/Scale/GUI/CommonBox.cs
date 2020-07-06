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
using UnityEngine;
using KSPe.UI;

namespace TweakScale.GUI
{
    internal class CommonBox : TimedMessageBox
    {
        internal static GUIStyle createWinStyle (Color titlebarColor)
        {
            GUIStyle winStyle;
            {
                winStyle = new GUIStyle("Window")
                {
                    fontSize = 22,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.UpperCenter,
                    wordWrap = false
                };
                winStyle.focused.textColor =
                    winStyle.normal.textColor =
                    winStyle.active.textColor =
                    winStyle.hover.textColor = titlebarColor;
                winStyle.border.top = 5;
                { 
                    Texture2D tex = new Texture2D(1,1);
                    tex.SetPixel(0,0,new Color(0f, 0f, 0f, 0.45f));
                    tex.Apply();
                    winStyle.normal.background = tex;
                }
            }

            return winStyle;
        }

        internal static GUIStyle createTextStyle ()
        {
            GUIStyle textStyle;
            {
                textStyle = new GUIStyle("Label")
                {
                    fontSize = 14,
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleLeft,
                    wordWrap = true
                };
                textStyle.focused.textColor =
                    textStyle.normal.textColor =
                    textStyle.active.textColor =
                    textStyle.hover.textColor = Color.white;
                textStyle.padding.top = 8;
                textStyle.padding.bottom = textStyle.padding.top;
                textStyle.padding.left = textStyle.padding.top;
                textStyle.padding.right = textStyle.padding.top;
                {
                    Texture2D tex = new Texture2D(1,1);
                    tex.SetPixel(0,0,new Color(0f, 0f, 0f, 0.45f));
                    tex.Apply();
                    textStyle.active.background =
                        textStyle.focused.background =
                        textStyle.normal.background = tex;
                }
            }
            return textStyle;
        }
    }
}
