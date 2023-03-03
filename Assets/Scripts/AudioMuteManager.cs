using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMuteManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> backgroundAudio = new List<AudioSource>();
    [SerializeField] List<AudioSource> soundEffects = new List<AudioSource>();

    [SerializeField] ChangeSounds changeSoundsScript;

    private void Awake()
    {
        HandleBackgroundMute();
        HandleSoundEffectMute();

        if (changeSoundsScript)
        {
            changeSoundsScript.muteButtonClicked.AddListener(HandleSoundEffectMute);
            changeSoundsScript.muteButtonClicked.AddListener(HandleBackgroundMute);
        }
    }

    void HandleBackgroundMute()
    {
        int thisIsABool = PlayerPrefs.GetInt("Background Audio Muted");

        if (thisIsABool == 1)
        {
            foreach(AudioSource audioSource in backgroundAudio)
            {
                audioSource.mute = true;
            }
        }
        else if (thisIsABool == 0)
        {
            foreach(AudioSource audioSource in backgroundAudio)
            {
                audioSource.mute = false;
            }
        }
    }

    void HandleSoundEffectMute()
    {
        int thisIsABool = PlayerPrefs.GetInt("Sound Effects Audio Muted");

        if (thisIsABool == 1)
        {
            foreach (AudioSource audioSource in soundEffects)
            {
                audioSource.mute = true;
            }
        }
        else if (thisIsABool == 0)
        {
            foreach (AudioSource audioSource in soundEffects)
            {
                audioSource.mute = false;
            }
        }
    }
}
