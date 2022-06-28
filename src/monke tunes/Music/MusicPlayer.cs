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
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer instance;

        internal List<string> songList = new List<string>();
        internal List<int> songQueue = new List<int>();
        internal AudioSource songPlayer;

        internal TextMeshPro screenText;
        private async void Start()
        {
            instance = this;

            Console.WriteLine("Fetching songs");
            songList = MusicLoader.LoadMusic();
            songList.Sort();
            Console.WriteLine("Song paths loaded");

            songPlayer = gameObject.AddComponent<AudioSource>();
            songPlayer.spatialize = true;
            songPlayer.loop = false;
            songPlayer.time = 0;
            songPlayer.volume = 0.5f;
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
            screenText.color = Color.white;
            screenText.fontSize = 300;
            //screenText.font = Utills.Utopium;

            await UpdateSong();
            Playing = false;

            Redraw();
            }
        private async void Update()
        {
            if (songPlayer.clip == null) return;
            if (songPlayer.clip.length - songPlayer.time < 0.1f) await End();
        }
        internal void Redraw()
        {
            screenText.text = (songPlayer.clip.name + ":\n" + (Playing ? "-Playing\n-" : "-Paused\n-") + Volume + " volume\n-" + Mode + " mode");
        }
        private async Task UpdateSong()
        {
            songPlayer.time = 0;
            songPlayer.clip = await MusicLoader.RequestMusic(songList[index]);
            Playing = true;
            Console.WriteLine("Song set to " + songPlayer.clip.name);
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
                    int next = UnityEngine.Random.Range(0, songList.Count - 1);
                    Index = next;
                    break;
            }

            Redraw();
        }

        private int index = 0;
        public int Index
        {
            get => index;
            set
            {
                try
                {
                    index = Utills.mod(value, songList.Count);
                    UpdateSong();
                }
                catch
                {
                    index = 0;
                    Console.WriteLine("Some issue with setting index - defualting to 0");
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
                value = (PlayType)Utills.mod((int)value, 3);
                mode = value;

                Console.WriteLine("Play type set to " + mode);
                Redraw();
            }
        }
        public bool Playing
        {
            get => songPlayer.isPlaying;
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
