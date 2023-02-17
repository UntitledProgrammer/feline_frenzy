using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Audible : MonoBehaviour
    {
        //Attributes:
        public AudioInputController controller;
        private AudioSource source;

        //Methods:
        private void Awake()
        {
            if (controller == null) Destroy(this);
            source = GetComponent<AudioSource>();
        }

        private void LateUpdate()
        {
            for (int i = 0; i < controller.audibles.Count; i++)
            {
                if (!controller.audibles[i].Active) continue;

                Debug.Log(controller.audibles[i].name);

                if(controller.overridable && controller.audibles[i].layerable)
                {
                    AudioSource temp = gameObject.AddComponent<AudioSource>();
                    temp.PlayOneShot(controller.audibles[i].audioClip);
                    Destroy(temp, controller.audibles[i].audioClip.length);
                }

                source.PlayOneShot(controller.audibles[i].audioClip);

                return;
            }
        }
    }
}