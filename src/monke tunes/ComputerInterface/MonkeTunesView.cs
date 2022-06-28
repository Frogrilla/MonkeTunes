using ComputerInterface;
using ComputerInterface.ViewLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace MonkeTunes.ComputerInterface
{
    internal class MonkeTunesView : ComputerView
    {
        public int Selected = 0;
        public string[] Lines = new string[]
        {
            "Music",
            "Controls"
        };
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            DrawScreen();
        }

        public void DrawScreen()
        {
            SetText(str =>
            {
                str.BeginCenter();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10").AppendLine();
                str.AppendClr("Monke Tunes", "#6200ff").AppendLine();
                str.AppendClr("By Frogrilla", "#9000ff").AppendLine();
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
                    ReturnToMainMenu();
                    break;
                case EKeyboardKey.Enter:
                    switch (Selected)
                    {
                        case 0:
                            ShowView<MuiscListView>();
                            break;
                        case 1:
                            ShowView<TuneControlsView>();   
                            break;
                    }
                    break;
                case EKeyboardKey.Down:
                    Select(Selected + 1);
                    break;
                case EKeyboardKey.Up:
                    Select(Selected - 1);
                    break;
            }
        }

        public void Select(int i)
        {
            Selected = Mathf.Clamp(i,0,Lines.Length);
            DrawScreen();
        }
    }
}
