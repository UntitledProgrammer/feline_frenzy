using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFrenzy.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI), typeof(AudioSource))]
    public class InfiniteScore : MonoBehaviour
    {
        //Attributes:
        public AudioClip soundEffect;
        private AudioSource source;
        private int currentScore;
        private TMPro.TextMeshProUGUI textBox;

        //Methods:
        public void Awake()
        {
            currentScore = default;
            textBox = GetComponent<TMPro.TextMeshProUGUI>();
            source = GetComponent<AudioSource>();
        }

        public void AddScore(Interaction.InfiniteCoin coin)
        {
            currentScore += coin.Value;
            source.PlayOneShot(soundEffect);
            Destroy(coin.gameObject);
            UpdateScore();
        }

        public void UpdateScore()
        {
            textBox.text = currentScore.ToString();
        }
    }
}