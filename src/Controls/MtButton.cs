using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MonkeTunes.Controls
{
    internal class MtButton : MonoBehaviour
    {
#if !PLUGIN
        public Color colPressed = Color.gray;
        public Color colNormal = Color.white;
        public float waitTime = .25f;
#endif
        private float timer = 0f;
        private void Start()
        {
            gameObject.layer = 18;
            try {
                gameObject.GetComponent<Collider>().isTrigger = true;
            } 
            catch {
                gameObject.AddComponent<Collider>().isTrigger = true;
            }
        }
        private void Update()
        {
            if (timer == 0) return;
            timer -= Time.deltaTime;
            if (timer < 0.01) timer = 0;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (timer != 0) return;
            string name = collider.gameObject.name;
            if (name != "LeftHandTriggerCollider" && name != "RightHandTriggerCollider") return;
            bool left = name == "LeftHandTriggerCollider" ? true : false;

            GorillaTagger.Instance.StartVibration(left, GorillaTagger.Instance.tapHapticStrength, GorillaTagger.Instance.tapHapticDuration);
            ButtonPress();

            gameObject.GetComponent<Renderer>().material.color = colPressed;
        }

        private void OnTriggerExit(Collider collider)
        {
            if (timer != 0) return;
            string name = collider.gameObject.name;
            if (name != "LeftHandTriggerCollider" && name != "RightHandTriggerCollider") return;

            gameObject.GetComponent<Renderer>().material.color = colNormal;
        }

        internal virtual void ButtonPress()
        {
            Console.WriteLine(String.Format("MT button {0} pressed", gameObject.name));
        }
    }
}
