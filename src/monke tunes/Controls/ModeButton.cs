using System;
using System.Collections.Generic;
using System.Text;

namespace MonkeTunes.Controls
{
    internal class ModeButton : MtButton
    {
#if !PLUGIN
        public int modeOffset = 1;
#endif
        internal override void ButtonPress()
        {
            base.ButtonPress();
            Music.MusicPlayer.instance.Mode += modeOffset;
        }
    }
}
