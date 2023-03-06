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
        public UnityEngine.Events.UnityEvent onAwake;
        public UnityEngine.Events.UnityEvent onLimitReached;
        public StoryManager storyManager;
        [SerializeField] private float timeLimit;
        private float currentTime;
        private TMPro.TextMeshProUGUI textBox;
        #endregion

        #region Methods
        public void Awake()
        {
            //Initalise components and timer.
            textBox = GetComponent<TMPro.TextMeshProUGUI>();
            currentTime = default;

            //Invoke any starting methods/functions.
            onAwake.Invoke();
        }

        public void Restart()
        {
            currentTime = default;
            onLimitReached.Invoke();
        }

        public void AddTime(float amount)
        {
            currentTime -= amount > currentTime ? currentTime : amount;
        }

        public void LateUpdate()
        {
            //Update timer.
            currentTime += Time.deltaTime;
            textBox.text = (timeLimit - currentTime).ToString("F");

            //Reached time limit.
            if(currentTime >= timeLimit)
            {
                currentTime = default;
                onLimitReached.Invoke();
            }
        }
        #endregion
    }

}