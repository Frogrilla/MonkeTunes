using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

namespace MonkeTunes.Music
{
    internal static class MusicLoader
    {
        internal static string MusicFolder;
        internal static List<string> LoadMusic()
        {
            MusicFolder = Path.Combine(Path.GetDirectoryName(typeof(MusicPlayer).Assembly.Location), "Music");
            return Directory.GetFiles(MusicFolder, "*.ogg", SearchOption.AllDirectories).ToList();
        }

        internal static List<Playlist> LoadPlaylists()
        {
            MusicFolder = Path.Combine(Path.GetDirectoryName(typeof(MusicPlayer).Assembly.Location), "Music");
            List<Playlist> playlists = new List<Playlist>();

            foreach (string directory in Directory.GetDirectories(MusicFolder))
            {
                Playlist playlist = new Playlist();
                playlist.name = Path.GetFileNameWithoutExtension(directory);
                playlist.songs = Directory.GetFiles(directory, "*.ogg", SearchOption.AllDirectories).ToList();
                playlist.songs.Sort();
                playlists.Add(playlist);

                Console.WriteLine(playlist.name + ":\n" + playlist.songs);
            }

            playlists.OrderBy(l => l.name);
            return playlists;
        }

        internal static async Task<AudioClip> RequestMusic(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);

            using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.UNKNOWN))
            {
                req.SendWebRequest();

                try
                {
                    while (!req.isDone) await Task.Delay(5);
                    if (req.isNetworkError || req.isHttpError)
                    {
                        Console.WriteLine("Fetching track had error: " + req.error);
                    }
                    else
                    {
                        AudioClip loadedSong = DownloadHandlerAudioClip.GetContent(req);
                        loadedSong.name = name;
                        Console.WriteLine("Got track " + name);
                        return loadedSong;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine($"{err.Message}, {err.StackTrace}");
                }
            }

            return null;
        }
    }
}
