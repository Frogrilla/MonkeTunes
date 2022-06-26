using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MonkeTunes.Controls
{
    internal class PlayButton : MtButton
    {
        internal override void ButtonPress()
        {
            base.ButtonPress();
            Music.MusicPlayer.instance.Playing ^= true;
        }
    }
}
