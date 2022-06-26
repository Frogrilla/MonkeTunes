using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MonkeTunes
{
    internal static class Utills
    {
        //internal static TMPro.TMP_FontAsset Utopium;
        internal static string ColourToHex(Color colour)
        {
            int r = Mathf.RoundToInt(colour.r * 255);
            int g = Mathf.RoundToInt(colour.g * 255);
            int b = Mathf.RoundToInt(colour.b * 255);
            int a = Mathf.RoundToInt(colour.a * 255);

            string hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");

            return hex;
        }

        internal static string ColourString(string str, Color colour)
        {
            return "<color=#" + ColourToHex(colour) + ">" + str + "</color>";
        }

        internal static string ColourString(string str, string hex)
        {
            return "<color=" + hex + ">" + str + "</color>";
        }
        internal static int mod(int a, int b)
        {
            return a - b * Mathf.FloorToInt(a / b);
        }
    }
}
