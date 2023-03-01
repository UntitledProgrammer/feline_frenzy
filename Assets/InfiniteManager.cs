using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFrenzy.Core
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class InfiniteManager : MonoBehaviour
    {
        //Attributes:
        [SerializeField] private UnityEditor.SceneAsset mainMenu;
        [SerializeField] private TMPro.TextMeshProUGUI textBox;
        private float currentTime;
        private uint currentAttempts;
        private const uint maxAttempts = 3;

        //Methods:
        private void Awake()
        {
            textBox = GetComponent<TMPro.TextMeshProUGUI>();
            currentAttempts = default;
        }

        private void LateUpdate()
        {
            currentTime += Time.deltaTime;
            textBox.text = currentTime.ToString("F");
        }

        public void Restart()
        {
            //If player has used the maximum number of attempts, return to the main menu.
            if(++currentAttempts > maxAttempts)
            {
                if (mainMenu == null) { Debug.LogError("InfiniteManager: Main Menu was null.");  Application.Quit(); }
                UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu.name);
                return;
            }

            //Restart the timer for the next attempt.
            currentTime = default;
        }
    }
}