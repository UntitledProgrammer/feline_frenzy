using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject playButton;

    public GameObject storyModeButton;
    public GameObject survivalModeButton;

    public Animator playButtonController;

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
        playButtonController.SetTrigger("PressedButton");
        //playButtonController.Play("PressedButton");
        //playButton.SetActive(false);
    }

    public void TurnOnButtons()
    {
        storyModeButton.SetActive(true);
        survivalModeButton.SetActive(true);
    }

    public void StoryModeButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
