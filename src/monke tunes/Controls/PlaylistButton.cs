using System;
using System.Collections.Generic;
using System.Text;
using MonkeTunes.Music;

namespace MonkeTunes.Controls
{
    internal class PlaylistButton : MtButton
    {
#if !PLUGIN
        public int listOffset = 1;
#endif
        internal override void ButtonPress()
        {
            base.ButtonPress();
            MusicPlayer.instance.PlaylistIndex += listOffset;
            MusicPlayer.instance.Index = 0;
        }
    }
}
