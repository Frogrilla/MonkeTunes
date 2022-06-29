﻿using BepInEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Zenject;
using Bepinject;
using UnityEngine;
using System.Linq;
using Utilla;
using TMPro;

namespace MonkeTunes
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("com.ahauntedarmy.gorillatag.tmploader")]
    public class Plugin : BaseUnityPlugin
    {
        private Dictionary<string, Vector3[]> triggerDict = new Dictionary<string, Vector3[]>()
        {
            { "JoinPublicRoom - Canyon",                    new Vector3[] { new Vector3(-118, 17.75f, -168.5f), new Vector3(0, 90, 0) } },
            { "JoinPublicRoom - Cave",                      new Vector3[] { new Vector3(-70.1f, -18.525f, -7.5f), new Vector3(0, 150, 0) } },
            { "JoinPublicRoom - City Back",                 new Vector3[] { new Vector3(-45.5f, 15.95f, -115.25f), new Vector3(0, 30, 0) } },  
            { "JoinPublicRoom - City Front",                new Vector3[] { new Vector3(-45.5f, 15.95f, -115.25f), new Vector3(0, 30, 0) } },
            { "JoinPublicRoom - Forest, End of Tutorial",   new Vector3[] { new Vector3(-59.85f, 13.925f, -44.6f), new Vector3(0, 15, 0) } },
            { "JoinPublicRoom - Forest, Tree Exit",         new Vector3[] { new Vector3(-59.85f, 13.925f, -44.6f), new Vector3(0, 15, 0) } },
            { "JoinPublicRoom - Mountain For Computer",     new Vector3[] { new Vector3(26.5f, .25f, -38.75f), new Vector3(0, 290, 0) } }
        };
        void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            Zenjector.Install<ComputerInterface.MainInstaller>().OnProject();
            
            StartCoroutine(Delay());
        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(5);

            Transform[] mapTriggers = GameObject.Find("/NetworkTriggers/Networking Trigger").GetComponentsInChildren<Transform>(false);
            mapTriggers = mapTriggers.OrderBy(o => o.name).ToArray();

            foreach(Transform trigger in mapTriggers)
            {
                try
                {
                    trigger.gameObject.AddComponent<Music.SpeakerTrigger>().target = triggerDict[trigger.gameObject.name];
                    Console.WriteLine("Added MonkeTunes.Music.SpeakerTrigger to " + trigger.gameObject.name);
                }
                catch
                {
                    Console.WriteLine("No key in dict");
                }
            }

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeTunes.Resources.mt_assets");
            var Bundle = AssetBundle.LoadFromStream(stream);
            GameObject computer = Bundle.LoadAsset<GameObject>("MusicComputer");
            //Utills.Utopium = Bundle.LoadAsset<TMP_FontAsset>("Utopium SDF");
            object[] allAssets = Bundle.LoadAllAssets();
            Bundle.Unload(false);

            GameObject JukeBox = Instantiate(computer);

            JukeBox.AddComponent<Music.MusicPlayer>();
            JukeBox.name = "MT_JukeBox";
            JukeBox.transform.localScale = Vector3.one * .5f;
            JukeBox.transform.position = new Vector3(-63, 11.825f, -82);
            JukeBox.transform.eulerAngles = new Vector3(0, -120, 0);

            JukeBox.transform.Find("play").gameObject.AddComponent<Controls.PlayButton>();
            JukeBox.transform.Find("skip").gameObject.AddComponent<Controls.SkipButton>();
            JukeBox.transform.Find("back").gameObject.AddComponent<Controls.BackButton>();
            JukeBox.transform.Find("vol+").gameObject.AddComponent<Controls.VolumeButton>().volumeOffset = .1f;
            JukeBox.transform.Find("vol-").gameObject.AddComponent<Controls.VolumeButton>().volumeOffset = -.1f;
            JukeBox.transform.Find("mode+").gameObject.AddComponent<Controls.ModeButton>().modeOffset = 1;
            JukeBox.transform.Find("mode-").gameObject.AddComponent<Controls.ModeButton>().modeOffset = -1;
        }
    }
}