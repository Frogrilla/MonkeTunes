using BepInEx;
using System;
using UnityEngine;
using VmodMonkeMapLoader;
using MonkeTunes.Music;
using System.Reflection;

namespace MT_CustomMaps
{
    [BepInDependency("vadix.gorillatag.maploader")]
    [BepInDependency("com.frogrilla.gorillatag.monketunes")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            Events.OnMapEnter += OnMapEnter;
        }

        // Find JukeBox and send it to position in map if it exists
        public void OnMapEnter(bool enter)
        {
            if (!enter) return;
            try
            {
                GameObject target = GameObject.Find("MT_CustomMapLocation");

                MusicPlayer.instance.transform.parent = target.transform;
                MusicPlayer.instance.transform.localPosition = Vector3.zero;
                MusicPlayer.instance.transform.localEulerAngles = Vector3.zero;
            }
            catch(Exception E)
            {
                Console.WriteLine("Error moving JukeBox to custom map - MT location likely missing in map\n" + E);
                return;
            }
        }
    }
}
