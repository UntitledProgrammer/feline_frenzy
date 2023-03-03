using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Game
{
    [System.Serializable]
    public struct StoryMeta
    {
        //Attributes:
        public bool unlocked;
        public float record;
        [SerializeField] private UnityEditor.SceneAsset scene;

        //Properties:
        public bool IsValid { get => scene != null; }
        
        //Constructor:
        public StoryMeta(UnityEditor.SceneAsset scene, bool unlocked = false)
        {
            this.scene = scene;
            this.unlocked = unlocked;
            record = default;
        }
    }

    [CreateAssetMenu(fileName = "StoryManager", menuName = "Game/StoryManager", order = 0)]
    public class StoryManager : ScriptableObject
    {
        //Attributes:
        public List<StoryMeta> stories;
        private uint position;
    }
}