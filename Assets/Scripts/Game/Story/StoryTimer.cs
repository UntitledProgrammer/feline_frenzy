using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFrenzy.Game.Story
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class StoryTimer : MonoBehaviour
    {
        #region Attributes
        public StoryManager storyManager;
        [SerializeField] private float timeLimit;
        private float currentTime;
        private TMPro.TextMeshProUGUI textBox;
        #endregion

        #region Methods
        public void Awake()
        {
            textBox = GetComponent<TMPro.TextMeshProUGUI>();
            currentTime = default;
        }

        public void LateUpdate()
        {
            //Update timer.
            currentTime += Time.deltaTime;
            textBox.text = (timeLimit - currentTime).ToString("F");

            //Reached time limit.
            if(currentTime >= timeLimit)
            {
                storyManager.ReloadCurrentStory();
            }
        }
        #endregion
    }

}