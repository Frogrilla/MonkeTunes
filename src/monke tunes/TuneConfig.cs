﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;

namespace MonkeTunes
{
    internal static class TuneConfig 
    {
        internal static float InitialVolume = .5f;
        internal static int InitialMode = 0;
        internal static bool PlayOnStart = false;

        internal static Color ComputerColour = Color.white;
        internal static Color ScreenColour = Color.green;
        internal static Color TextColour = Color.white;
        internal static Color ButtonColour = Color.white;
        internal static Color PressedColour = Color.grey;
        internal static Color IconColour = Color.black;
        internal static void LoadSettings()
        {
            var File = new ConfigFile(Path.Combine(Paths.ConfigPath, "MonkeTunes.cfg"), true);

            InitialVolume = File.Bind("Music Settings", "InitialVolume", .5f).Value;
            InitialMode = File.Bind("Music Settings", "InitialMode", 0, "0 = Sequential, 1 = Looping, 2 = Random").Value;
            PlayOnStart = File.Bind("Music Settings", "PlayOnStart", false).Value;

            ComputerColour = File.Bind("Computer Settings", "ComputerColour", Color.white).Value;
            ScreenColour = File.Bind("Computer Settings", "ScreenColour", Color.green).Value;
            TextColour = File.Bind("Computer Settings", "TextColour", Color.white).Value;
            ButtonColour = File.Bind("Computer Settings", "ButtonColour", Color.white).Value;
            PressedColour = File.Bind("Computer Settings", "PressedColour", Color.grey).Value;
            IconColour = File.Bind("Computer Settings", "IconColour", Color.black).Value;
        }
    }
}
