using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine.Networking;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace MonkeTunes.Music
{
    public enum PlayType
    {
        Sequential = 0,
        Looping = 1,
        Random = 2
    }
    public struct Playlist
    {
        public string name;
        public List<string> songs;
    }
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer instance;

        internal List<Playlist> playlists = new List<Playlist>();
        internal AudioSource songPlayer;
        internal TextMeshPro screenText;

        private bool Downloaded = false;
        private async void Start()
        {
            instance = this;

            Console.WriteLine("Fetching songs");
            //songList = MusicLoader.LoadMusic();
            //songList.Sort();
            playlists = MusicLoader.LoadPlaylists();
            Console.WriteLine("Song paths loaded");

            songPlayer = gameObject.AddComponent<AudioSource>();
            transform.Find("MT_ComputerModel/default").GetComponent<Renderer>().material.SetColor("_EmissionColor", TuneConfig.ScreenColour);
            songPlayer.spatialize = true;
            songPlayer.loop = false;
            songPlayer.time = 0;
            songPlayer.volume = TuneConfig.InitialVolume;
            songPlayer.priority = 128;
            songPlayer.maxDistance = 100;
            

            GameObject text = new GameObject();
            text.transform.parent = transform.Find("screen");
            screenText = text.AddComponent<TextMeshPro>();

            screenText.rectTransform.localPosition = Vector3.zero;
            screenText.rectTransform.localEulerAngles = Vector3.zero;
            screenText.rectTransform.localScale = Vector3.one;
            screenText.rectTransform.sizeDelta = new Vector2(500, 300);

            screenText.alignment = TextAlignmentOptions.TopLeft;
            screenText.color = TuneConfig.TextColour;
            screenText.fontSize = 300;
            //screenText.font = Utills.Utopium;

            await UpdateSong();
            Playing = TuneConfig.PlayOnStart;
            mode = (PlayType)TuneConfig.InitialMode;
            Redraw();

            }
        private async void Update()
        {
            if (songPlayer.clip == null) return;
            if (!Downloaded && Playing) { await UpdateSong(); return; }
            if (songPlayer.clip.length - songPlayer.time < 0.1f && Playing) await End();
        }
        internal void Redraw()
        {
            screenText.text = (CurrentSong() + ":\n" + "-" + playlists[PlaylistIndex].name + " playlist\n" + (Playing ? "-Playing\n-" : "-Paused\n-") + Volume + " volume\n-" + Mode + " mode");
        }
        private async Task UpdateSong()
        {
            songPlayer.time = 0;
            songPlayer.clip = await MusicLoader.RequestMusic(playlists[PlaylistIndex].songs[Index]);
            Downloaded = true;
            Playing = Playing;
            Console.WriteLine("Song set to " + songPlayer.clip.name + " from playlist " + playlists[PlaylistIndex].name);
        }
        public async Task End()
        {
            switch (mode)
            {
                case PlayType.Sequential:
                    // Go to next song in list or queue 
                    ++Index;
                    break;
                case PlayType.Looping:
                    // Same Song - no need to change
                    songPlayer.time = 0;
                    Playing = true;
                    break;
                case PlayType.Random:
                    // Pick a random index from 0 - SongList length
                    int next = UnityEngine.Random.Range(0, playlists[PlaylistIndex].songs.Count - 1);
                    Index = next;
                    break;
            }

            Redraw();
        }

        public string CurrentSong() => Path.GetFileNameWithoutExtension(playlists[PlaylistIndex].songs[index]);

        private int index = 0;
        public int Index
        {
            get => index;
            set
            {
                try
                {
                    index = (int)Mathf.Repeat(value, playlists[PlaylistIndex].songs.Count);
                    Downloaded = false;
                    Redraw();
                    //UpdateSong();
                }
                catch
                {
                    Console.WriteLine("Setting index failed");
                }
            }
        }

        private int playlist = 0;
        public int PlaylistIndex
        {
            get => playlist;
            set 
            {
                try
                {
                    playlist = (int)Mathf.Repeat(value, playlists.Count);
                    Console.WriteLine("Current playlist set to " + playlists[playlist].name);
                }
                catch
                {
                    Console.WriteLine("Setting playlist failed");   
                }
            }
        }

        private PlayType mode = PlayType.Sequential;
        public PlayType Mode
        {
            get => mode;
            set
            {
                if (value < 0) value += 3;
                value = (PlayType)Mathf.Repeat((int)value, 3);
                mode = value;

                Console.WriteLine("Play type set to " + mode);
                Redraw();
            }
        }

        private bool playBuffer = false;
        public bool Playing
        {
            get => songPlayer.isPlaying || playBuffer;
            set
            {
                if (value)
                {
                    songPlayer.Play();
                    //songPlayer.time = lastTime;
                    Console.WriteLine("Music playing");

                    transform.Find("play/icon_play").gameObject.SetActive(false);
                    transform.Find("play/icon_pause").gameObject.SetActive(true);
                }
                else
                {
                    //lastTime = songPlayer.time;
                    songPlayer.Pause();
                    Console.WriteLine("Music Stopped");

                    transform.Find("play/icon_play").gameObject.SetActive(true);
                    transform.Find("play/icon_pause").gameObject.SetActive(false);
                }
                playBuffer = value;
                Redraw();
            }
        }
        public float Volume
        {
            get => songPlayer.volume;
            set
            {
                songPlayer.volume = (float)Math.Round(Mathf.Clamp(value, 0, 1), 1);
                Console.WriteLine("Volume set to " + songPlayer.volume);
                Redraw();
            }
        }
    }
}
