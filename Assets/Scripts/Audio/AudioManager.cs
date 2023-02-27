using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Audio
{
    public class AudioManager : MonoBehaviour
    {
        //Attributes:
        private AudioListener mainListener;
        private float musicVolume, effectVolue;
        private static AudioManager singleton;
        private static AudioSource source;
        private const float MIN_VOLUME = 0.0f, MAX_VOLUME = 1.0f;

        //Properties:
        public static AudioManager Singleton
        {
            get
            {
                if(singleton == null) { singleton = new GameObject("AudioManager").AddComponent<AudioManager>(); source = singleton.gameObject.AddComponent<AudioSource>(); }
                return singleton;
            }
        }

        public float MusicVolume { set => musicVolume = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME); }

        public float SFXVolume { set => effectVolue = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME); }

        public void PlayMusic(AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            AudioSource temp = singleton.gameObject.AddComponent<AudioSource>();
            temp.PlayOneShot(clip);

            Destroy(temp, clip.length);
        }
    }
}