using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MonkeTunes.Controls
{
    internal class MtButton : MonoBehaviour
    {
#if !PLUGIN
        public float waitTime = .25f;
#endif
        private float timer = 0f;
        internal static bool canPress = true;
        private void Start()
        {
            gameObject.layer = 18;
            try {
                gameObject.GetComponent<Collider>().isTrigger = true;
            } 
            catch {
                gameObject.AddComponent<Collider>().isTrigger = true;
            }

            foreach(Transform child in GetComponentsInChildren<Transform>(true))
            {
                if (child.name.Contains("icon_")) child.GetComponent<Renderer>().material.color = TuneConfig.IconColour;
            }
            GetComponent<Renderer>().material.color = TuneConfig.ButtonColour;
        }
        private void Update()
        {
            if (timer == 0) { canPress = true; return; }
            timer -= Time.deltaTime;
            if (timer < 0.01) timer = 0;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (timer != 0 || !canPress) return;
            string name = collider.gameObject.name;
            if (name != "LeftHandTriggerCollider" && name != "RightHandTriggerCollider") return;
            bool left = name == "LeftHandTriggerCollider" ? true : false;

            GorillaTagger.Instance.StartVibration(left, GorillaTagger.Instance.tapHapticStrength, GorillaTagger.Instance.tapHapticDuration);
            timer = waitTime;
            canPress = false;
            ButtonPress();

            gameObject.GetComponent<Renderer>().material.color = TuneConfig.PressedColour;
        }

        private void OnTriggerExit(Collider collider)
        {
            string name = collider.gameObject.name;
            if (name != "LeftHandTriggerCollider" && name != "RightHandTriggerCollider") return;

            gameObject.GetComponent<Renderer>().material.color = TuneConfig.ButtonColour;
        }

        internal virtual void ButtonPress()
        {
            Console.WriteLine(String.Format("MT button {0} pressed", gameObject.name));
        }
    }
}
