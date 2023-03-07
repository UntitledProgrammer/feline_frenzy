using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFrenzy.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public sealed class Scoreboard : MonoBehaviour
    {
        #region Attributes
        public Game.StoryManager storyManager;
        private TMPro.TextMeshProUGUI textbox;
        #endregion

        #region Methods
        private void Awake()
        {
            if(storyManager == null) { Destroy(this); return; }
            textbox = GetComponent<TMPro.TextMeshProUGUI>();

            textbox.text = string.Empty;

            for (int i = 0; i < storyManager.TotalStories; i++)
            {
                if(i>0) textbox.text += "\n";
                textbox.text += storyManager[i].Name + ": " + (storyManager[i].record > 0 ? storyManager[i].record.ToString("F") : "N/A");
            }
        }
        #endregion
    }
}