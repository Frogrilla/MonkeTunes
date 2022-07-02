using ComputerInterface;
using ComputerInterface.ViewLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MonkeTunes.Music;
using UnityEngine;

namespace MonkeTunes.ComputerInterface
{
    internal class TuneControlsView : ComputerView
    {
        public int Selected = 0;
        public string[] Lines = new string[]
        {
            "Play",
            "Volume",
            "Mode",
            "Skip",
            "Back",
        };
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            DrawScreen();
        }

        public void DrawScreen()
        {
            Lines[0] = MusicPlayer.instance.Playing ? "Pause" : "Play";
            Lines[1] = "Volume: " + MusicPlayer.instance.Volume;
            Lines[2] = MusicPlayer.instance.Mode.ToString();

            SetText(str =>
            {
                str.BeginCenter();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();
                str.AppendClr("Music Controls", "#6200ff").AppendLine();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();

                str.EndAlign().AppendLine();
                foreach (string line in Lines)
                {
                    string start = Lines[Selected] == line ? Utills.ColourString("- ", "#ff0066") : "  ";
                    str.AppendLine(start + line);
                }
            });
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:
                    ReturnView();
                    break;
                case EKeyboardKey.Enter:
                    switch (Selected)
                    {
                        case 0:
                            MusicPlayer.instance.Playing ^= true;
                            DrawScreen();
                            break;
                        case 3:
                            MusicPlayer.instance.Index++;
                            break;
                        case 4:
                            MusicPlayer.instance.Index--;
                            break;
                    }
                    break;
                case EKeyboardKey.Down:
                    Selected = (int)Mathf.Repeat(Selected + 1, Lines.Length);
                    DrawScreen();
                    break;
                case EKeyboardKey.Up:
                    Selected = (int)Mathf.Repeat(Selected - 1, Lines.Length);
                    DrawScreen();
                    break;
                case EKeyboardKey.Right:
                    switch (Selected)
                    {
                        case 1:
                            MusicPlayer.instance.Volume += 0.1f;
                            break;
                        case 2:
                            MusicPlayer.instance.Mode++;
                            break;
                    }
                    DrawScreen();
                    break;
                case EKeyboardKey.Left:
                    switch (Selected)
                    {
                        case 1:
                            MusicPlayer.instance.Volume -= 0.1f;
                            break;
                        case 2:
                            MusicPlayer.instance.Mode--;
                            break;
                    }
                    DrawScreen();
                    break;
            }
        }
    }
}
