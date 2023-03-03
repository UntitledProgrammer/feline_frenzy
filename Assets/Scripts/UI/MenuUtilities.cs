using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUtilities : MonoBehaviour
{
    //Methods:
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    public void LoadSceneAsync(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneDelay(sceneName, delay));
    }

    public IEnumerator LoadSceneDelay(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Quit() => Application.Quit();

    public void PauseGame(GameObject gameObject)
    {
        switch(gameObject.activeSelf)
        {
            case false:
                Time.timeScale = 1.0f;
                gameObject.SetActive(true);
                return;

            default:
                Time.timeScale = 0.0f;
                gameObject.SetActive(false);
                return;
        }
    }

    public void Pause(GameObject gameObject)
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(false);
    }

    public void Unpause(GameObject gameObject)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(true);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Toggle(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }
}
