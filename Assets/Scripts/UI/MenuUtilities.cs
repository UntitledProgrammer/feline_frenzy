using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUtilities : MonoBehaviour
{
    //Methods:
    public static void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    public static void Quit() => Application.Quit();
}
