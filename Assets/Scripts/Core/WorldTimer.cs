using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    public delegate void TimeWatch(float time);

    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class WorldTimer : MonoBehaviour
    {
        //Attributes:
        public SceneInfo start;
        public SceneInfo end;
        private float time;
        private List<TimeWatch> watches;
        private TMPro.TextMeshProUGUI textbox;

        //Methods:
        private void Awake()
        {
            textbox = GetComponent<TMPro.TextMeshProUGUI>();
            watches = new List<TimeWatch>();
        }

        private void Update()
        {
            time += Time.deltaTime;
            textbox.text = time.ToString("F");

            if (time >= start.limit)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(end.Name);
            }

            for(int i = 0; i < watches.Count; i++) { watches[i].Invoke(time); }
        }

        public void Bind(TimeWatch function)
        {
            if (watches.Contains(function)) return;
            watches.Add(function);
        }

        public void End()
        {
            start.record = time;
            UnityEngine.SceneManagement.SceneManager.LoadScene(end.name);
        }
    }
}