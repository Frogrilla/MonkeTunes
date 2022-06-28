using ComputerInterface;
using ComputerInterface.ViewLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MonkeTunes.Music;
using UnityEngine;

namespace MonkeTunes.ComputerInterface
{
    internal class MuiscListView : ComputerView
    {
        private int MaxLines = 7;
        public int Selected = 0;
        public List<string> Lines = new List<string>();
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            DrawScreen();
        }

        public void DrawScreen()
        {
            Lines.Clear();

            for(int i = 0; i < MusicPlayer.instance.songList.Count; i++)
            {
                string song = Path.GetFileNameWithoutExtension(MusicPlayer.instance.songList[i]);
                Lines.Add(song);
            }

            SetText(str =>
            {
                str.BeginCenter();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();
                str.AppendClr("Music List", "#6200ff").AppendLine();
                str.AppendClr(MusicPlayer.instance.songPlayer.clip.name, MusicPlayer.instance.Playing ? "#44ff54" : "#ff1154").AppendLine();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();

                str.EndAlign().AppendLine();
                int page = Mathf.FloorToInt(Selected / MaxLines);
                for(int i = page * MaxLines; i < (page + 1) * MaxLines; i++)
                {
                    string line = Lines[i];
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
                    if(MusicPlayer.instance.Index == Selected) MusicPlayer.instance.Playing ^= true;
                    else MusicPlayer.instance.Index = Selected;
                    DrawScreen();
                    break;
                case EKeyboardKey.Down:
                    Select(Selected + 1);
                    break;
                case EKeyboardKey.Up:
                    Select(Selected - 1);
                    break;
                case EKeyboardKey.Option1:
                    MusicPlayer.instance.Playing ^= true;
                    DrawScreen();
                    break;
            }
        }

        public void Select(int i)
        {
            Selected = Mathf.Clamp(i, 0, Lines.Count);
            DrawScreen();
        }
    }
}
