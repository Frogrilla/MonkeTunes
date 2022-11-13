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
        public Vector3 lastPos;
        public Vector3 lastRot;
        public bool posUpdated = false;
        public void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            Events.OnMapEnter += OnMapEnter;
            //Events.OnMapChange += OnMapChange;
        }

        // Find JukeBox and send it to position in map if it exists
        public void OnMapEnter(bool enter)
        {
            if (!enter) return;
            try
            {
                GameObject target = GameObject.Find("MT_CustomMapLocation");

                lastPos = MusicPlayer.instance.transform.position;
                lastRot = MusicPlayer.instance.transform.eulerAngles;
                posUpdated = true;

                MusicPlayer.instance.transform.SetParent(target.transform, true);
                MusicPlayer.instance.transform.localPosition = Vector3.zero;
                MusicPlayer.instance.transform.localEulerAngles = Vector3.zero;

                Console.WriteLine("MT_Computer moved to map");
            }
            catch(Exception e)
            {
                Console.WriteLine("MT_Computer not moved to map:\n" + e);
                return;
            }
        }
        /*  DOESN'T WORK AS MAP LEAVE
        public void OnMapChange(bool enter)
        {
            Console.WriteLine("Map Change / Leave");
            if (enter || !posUpdated) return;
            MusicPlayer.instance.transform.SetParent(null, true);
            MusicPlayer.instance.transform.position = lastPos;
            MusicPlayer.instance.transform.eulerAngles = lastRot;
            posUpdated = false;

            Console.WriteLine("MT_Computer moved back to previous pos");
        }
        */
    }
}
