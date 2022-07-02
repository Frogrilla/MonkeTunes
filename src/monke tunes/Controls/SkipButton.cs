using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using MonkeTunes.Music;

namespace MonkeTunes.Controls
{
    internal class SkipButton : MtButton
    {
        internal override void ButtonPress()
        {
            base.ButtonPress();
            if (MusicPlayer.instance.Mode == PlayType.Random) {
                int randIndex = UnityEngine.Random.Range(0, MusicPlayer.instance.playlists[MusicPlayer.instance.PlaylistIndex].songs.Count - 2);
                if (randIndex == MusicPlayer.instance.Index) randIndex = MusicPlayer.instance.playlists[MusicPlayer.instance.PlaylistIndex].songs.Count - 1;
                MusicPlayer.instance.Index = randIndex;
                return;
            }
            ++MusicPlayer.instance.Index;
        }
    }
}
