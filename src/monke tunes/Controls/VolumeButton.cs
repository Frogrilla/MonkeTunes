using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MonkeTunes.Controls
{
    internal class VolumeButton : MtButton
    {
#if !PLUGIN
        public float volumeOffset = .1f;
#endif
        internal override void ButtonPress()
        {
            base.ButtonPress();
            Music.MusicPlayer.instance.Volume += volumeOffset;
        }
    }
}
