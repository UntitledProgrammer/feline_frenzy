using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    /*
#if UNITY_EDITOR
    [CustomEditor(typeof(StoryCoin))]
    public class StoryCoin_Editor : Editor
    {
        //Attrubutes:
        private StoryCoin self;

        //Methods:
        private void Awake() => self = (StoryCoin)target;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.TextField("Main Settings", EditorStyles.boldLabel);
            self.value = EditorGUILayout.FloatField("Value", self.value);
            self.playerTag = EditorGUILayout.TagField("Player Tag", self.playerTag);
        }
    }
#endif
    */
}