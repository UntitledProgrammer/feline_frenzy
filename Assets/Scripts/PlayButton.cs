using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject playButton;

    public GameObject storyModeButton;
    public GameObject survivalModeButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonChange()
    {
        storyModeButton.SetActive(true);
        survivalModeButton.SetActive(true);
        playButton.SetActive(false);
    }

    public void StoryModeButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
