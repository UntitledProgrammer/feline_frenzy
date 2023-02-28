using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeSounds : MonoBehaviour
{
    public Sprite BackgroundMusicSoundOnImage;
    public Sprite BackgroundMusicSoundOffImage;
    public Sprite SoundEffectsSoundOnImage;
    public Sprite SoundEffectsSoundOffImage;
    public Button BackgroundMusicButton;
    public Button SoundEffectsButton;
    private bool backgroundMusicIsOn = true;
    private bool soundEffectsIsOn = true;
    private AudioListener listener;

    public AudioSource backgroundMusicSource;
    public AudioSource buttonSource;

    public UnityEvent muteButtonClicked = new UnityEvent();

    // Start is called before the first frame update
    void Awake()
    {
        listener = GameObject.FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            listener = new GameObject("listener").AddComponent<AudioListener>();
        }

        if (PlayerPrefs.GetInt("Background Audio Muted") == 1)
            backgroundMusicIsOn = false;

        if (PlayerPrefs.GetInt("Sound Effects Audio Muted") == 1)
            soundEffectsIsOn = false;

        HandleBackgroundMusicButtonSprite();
        HandleSoundEffectsButtonSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackgroundButtonClicked()
    {
        if (backgroundMusicIsOn)
        {
            PlayerPrefs.SetInt("Background Audio Muted", 1);

            backgroundMusicIsOn = false;
            /*button.image.sprite = SoundOffImage;
            backgroundMusicSource.mute = true;
            buttonSource.mute = true;
            listener.enabled = false;*/
        }
        else
        {
            PlayerPrefs.SetInt("Background Audio Muted", 0);

            backgroundMusicIsOn = true;
            /*button.image.sprite = SoundOnImage;
            backgroundMusicSource.mute = false;
            buttonSource.mute = false;
            listener.enabled = true;*/
        }

        HandleBackgroundMusicButtonSprite();
        muteButtonClicked.Invoke();
    }

    void HandleBackgroundMusicButtonSprite()
    {
        int thisIsABool = PlayerPrefs.GetInt("Background Audio Muted");

        if (thisIsABool == 1)
            BackgroundMusicButton.image.sprite = BackgroundMusicSoundOffImage;
        else if (thisIsABool == 0)
            BackgroundMusicButton.image.sprite = BackgroundMusicSoundOnImage;
    }
    public void SoundEffectButtonClicked()
    {
        if (soundEffectsIsOn)
        {
            PlayerPrefs.SetInt("Sound Effects Audio Muted", 1);

            soundEffectsIsOn = false;
            /*button.image.sprite = SoundOffImage;
            backgroundMusicSource.mute = true;
            buttonSource.mute = true;
            listener.enabled = false;*/
        }
        else
        {
            PlayerPrefs.SetInt("Sound Effects Audio Muted", 0);

            soundEffectsIsOn = true;
            /*button.image.sprite = SoundOnImage;
            backgroundMusicSource.mute = false;
            buttonSource.mute = false;
            listener.enabled = true;*/
        }

        HandleSoundEffectsButtonSprite();
        muteButtonClicked.Invoke();
    }

    void HandleSoundEffectsButtonSprite()
    {
        int thisIsABool = PlayerPrefs.GetInt("Sound Effects Audio Muted");

        if (thisIsABool == 1)
            SoundEffectsButton.image.sprite = SoundEffectsSoundOffImage;
        else if (thisIsABool == 0)
            SoundEffectsButton.image.sprite = SoundEffectsSoundOnImage;
    }
}
