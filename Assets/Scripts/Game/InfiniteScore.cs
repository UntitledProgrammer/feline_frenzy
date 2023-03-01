using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFrenzy.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class InfiniteScore : MonoBehaviour
    {
        //Attributes:
        private int currentScore;
        private TMPro.TextMeshProUGUI textBox;

        //Methods:
        public void Awake()
        {
            currentScore = default;
            textBox = GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void AddScore(Interaction.InfiniteCoin coin)
        {
            currentScore += coin.Value;
            Destroy(coin.gameObject);
            UpdateScore();
        }

        public void UpdateScore()
        {
            textBox.text = currentScore.ToString();
        }
    }
}