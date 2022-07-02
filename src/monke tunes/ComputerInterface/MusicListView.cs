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
        private int MaxLines = 5;
        public int Selected = 0;
        public int CurPlaylist = 0;
        public List<string> Lines = new List<string>();
        public override void OnShow(object[] args)
        {
            base.OnShow(args);  
            DrawScreen();
        }

        public void DrawScreen()
        {
            Lines.Clear();

            foreach(string songPath in MusicPlayer.instance.playlists[CurPlaylist].songs)
            {
                string song = Path.GetFileNameWithoutExtension(songPath);
                Lines.Add(song);
            }

            SetText(str =>
            {
                str.BeginCenter();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();
                str.AppendClr("Music List", "#6200ff").AppendLine();
                str.AppendClr(MusicPlayer.instance.CurrentSong(), MusicPlayer.instance.Playing ? "#44ff54" : "#ff1154").AppendLine();
                str.AppendClr(MusicPlayer.instance.playlists[CurPlaylist].name, "#9000ff").AppendLine();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();

                str.EndAlign().AppendLine();
                int page = Mathf.FloorToInt(Selected / MaxLines);
                for (int i = page * MaxLines; i < Mathf.Min((page + 1) * MaxLines, MusicPlayer.instance.playlists[CurPlaylist].songs.Count); i++)
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
                    if (MusicPlayer.instance.Index == Selected && MusicPlayer.instance.PlaylistIndex == CurPlaylist) MusicPlayer.instance.Playing ^= true;
                    else
                    {
                        MusicPlayer.instance.PlaylistIndex = CurPlaylist; 
                        MusicPlayer.instance.Index = Selected;
                    }
                    DrawScreen();
                    break;
                case EKeyboardKey.Down:
                    Selected = (int)Mathf.Repeat(Selected + 1, Lines.Count);
                    DrawScreen();
                    break;
                case EKeyboardKey.Up:
                    Selected = (int)Mathf.Repeat(Selected + 1, Lines.Count);
                    DrawScreen();
                    break;
                case EKeyboardKey.Right:
                    CurPlaylist = (int)Mathf.Repeat(CurPlaylist + 1, MusicPlayer.instance.playlists.Count);
                    DrawScreen();
                    break;
                case EKeyboardKey.Left:
                    CurPlaylist = (int)Mathf.Repeat(CurPlaylist - 1, MusicPlayer.instance.playlists.Count);
                    DrawScreen();
                    break;
                case EKeyboardKey.Option1:
                    MusicPlayer.instance.Playing ^= true;
                    DrawScreen();
                    break;
            }
        }
    }
}
