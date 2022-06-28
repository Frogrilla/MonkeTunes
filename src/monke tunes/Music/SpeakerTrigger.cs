using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace MonkeTunes.Music
{
    internal class SpeakerTrigger : MonoBehaviour
    {
        public Vector3[] target = new Vector3[2];
        void OnTriggerEnter(Collider collider)
        {
            GameObject GO = MusicPlayer.instance.gameObject;
            GO.transform.parent = null;
            GO.transform.position = target[0];
            GO.transform.eulerAngles = target[1];
        }
    }
}
