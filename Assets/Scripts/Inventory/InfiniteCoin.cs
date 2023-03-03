using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public class InfiniteCoin : MonoBehaviour
    {
        //Attributes:
        [SerializeField] private string playerTag;
        [SerializeField] private int value;
        private UI.InfiniteScore scoreboard;

        //Properties:
        public int Value { get => value; }

        //Methods:
        private void Awake()
        {
            scoreboard = FindObjectOfType<UI.InfiniteScore>();
            if(scoreboard == null) {  Destroy(gameObject); }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != playerTag) return;

            scoreboard.AddScore(this);
        }
    }
}