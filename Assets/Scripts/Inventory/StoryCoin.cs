using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public class StoryCoin : MonoBehaviour
    {
        #region Attributes
        public float value;
        public string playerTag;
        private Game.Story.StoryTimer storyTimer;
        #endregion

        #region Methods
        private void Awake()
        {
            storyTimer = FindObjectOfType<Game.Story.StoryTimer>();
            if (storyTimer == null) Destroy(this);

            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == playerTag) storyTimer.AddTime(value);
            Destroy(gameObject);
        }
        #endregion
    }
}