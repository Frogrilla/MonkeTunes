using System;
using System.Collections.Generic;
using System.Text;
using MonkeTunes.Music;

namespace MonkeTunes.Controls
{
    internal class SkipButton : MtButton
    {
        internal override void ButtonPress()
        {
            base.ButtonPress();
            ++MusicPlayer.instance.Index;
        }
    }
}
