using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MenuUtilities : MonoBehaviour
{
    #region
    [SerializeField] private AudioClip onClickSoundEffect;
    private AudioSource audioSource;
    #endregion

    #region Methods
    private void Awake() => audioSource = GetComponent<AudioSource>();
    public IEnumerator LoadSceneDelay(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        StartCoroutine(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        if(onClickSoundEffect)
        {
            audioSource.PlayOneShot(onClickSoundEffect);
            StartCoroutine(LoadSceneDelay(sceneName, onClickSoundEffect.length));
            return;
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneAsync(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneDelay(sceneName, delay));
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
    #endregion
}
