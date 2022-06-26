using System;
using System.Collections.Generic;
using System.Text;
using MonkeTunes.Music;

namespace MonkeTunes.Controls
{
    internal class BackButton : MtButton
    {
        internal override void ButtonPress()
        {
            base.ButtonPress();
            --MusicPlayer.instance.Index;
        }
    }
}
