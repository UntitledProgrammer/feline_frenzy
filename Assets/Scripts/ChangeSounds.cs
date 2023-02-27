using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSounds : MonoBehaviour
{
    private Sprite SoundOnImage;
    public Sprite SoundOffImage;
    public Button button;
    private bool isOn = true;
    private AudioListener listener;

    public AudioSource backgroundMusicSource;
    public AudioSource buttonSource;

    // Start is called before the first frame update
    void Awake()
    {
        listener = GameObject.FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            listener = new GameObject("listener").AddComponent<AudioListener>();
        }
        SoundOnImage = button.image.sprite;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void ButtonClicked() 
    {
        if (isOn)
        {
            button.image.sprite = SoundOffImage;
            isOn = false;
            backgroundMusicSource.mute = true;
            buttonSource.mute = true;
            listener.enabled = false;
        }
        else
        {
            button.image.sprite = SoundOnImage;
            isOn = true;
            backgroundMusicSource.mute = false;
            buttonSource.mute = false;
            listener.enabled = true;
        }
    }
}

