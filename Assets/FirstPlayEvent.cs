using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Game
{
    public sealed class FirstPlayEvent : MonoBehaviour
    {
        #region Attributes
        public UnityEngine.Events.UnityEvent onFirstPlay;
        [SerializeField] private string key;
        private const int FIRST_LAUNCH = 0, PREVIOUSLY_LAUNCHED = 1;
        #endregion

        #region Methods
        private void Awake()
        {
            //If the game has been recorded as previously launched return.
            if (PlayerPrefs.GetInt(key, FIRST_LAUNCH) == PREVIOUSLY_LAUNCHED) return;

            //Otherwise, invoke the onFirstPlay events/methods.
            PlayerPrefs.SetInt(key, PREVIOUSLY_LAUNCHED);
            onFirstPlay.Invoke();
        }
        #endregion
    }
}