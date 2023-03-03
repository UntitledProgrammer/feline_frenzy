using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFrenzy.Game
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class Delay : MonoBehaviour
    {
        //Attributes:
        public UnityEngine.Events.UnityEvent onAwake;
        public UnityEngine.Events.UnityEvent onExit;
        [SerializeField] private float delay;
        private float currentTime;
        private TMPro.TextMeshProUGUI textBox;

        //Methods:
        public void Awake()
        {
            textBox = GetComponent<TMPro.TextMeshProUGUI>();
            Restart();
        }

        public void Update()
        {
            currentTime += Time.deltaTime;
            textBox.text = currentTime.ToString("F");

            if(currentTime >= delay) Exit();
        }

        public void Restart()
        {
            gameObject.SetActive(true);
            currentTime = default;
            onAwake.Invoke();
        }

        public void Exit()
        {
            onExit.Invoke();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Restart();
        }
    }
}