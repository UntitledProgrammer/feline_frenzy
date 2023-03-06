using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FelineFrenzy.Game
{
    [System.Serializable]
    public class StoryMeta
    {
        //Attributes:
        public bool unlocked;
        public float record;
        [SerializeField] private string name;

        //Properties:
        public string Name { get => name; }
        public bool Unlocked { get => unlocked; set => unlocked = value; }

        //Methods:
        public void Lock() { unlocked = false; }
        public void Unlock() { unlocked = true; }
        
        //Constructor:
        public StoryMeta(string name, bool unlocked = false)
        {
            this.name= name;
            this.unlocked = unlocked;
            record = default;
        }
    }

    [CreateAssetMenu(fileName = "StoryManager", menuName = "Game/StoryManager", order = 0), System.Serializable]
    public class StoryManager : ScriptableObject
    {
        //Attributes:
        public List<StoryMeta> stories = new List<StoryMeta>();
        private int currentSceneIndex = 0;

        //Methods:
        public void LoadNext()
        {
            //If the final story level has been played return to the first scene (menu) in the build index.
            if (currentSceneIndex + 1 >= stories.Count) { SceneManager.LoadScene(SceneManager.GetSceneAt(default).name); return; }
            
            //Unlock and load the next story level.
            currentSceneIndex++;
            stories[currentSceneIndex].Unlock();
            SceneManager.LoadScene( stories[currentSceneIndex].Name);
        }

        private void LoadScene(int index)
        {
            currentSceneIndex = index;
            SceneManager.LoadScene(stories[index].Name);
        }

        public void LoadStory(int index)
        {
            if (index < 0 || index >= stories.Count || !stories[index].Unlocked) return;

            LoadScene(index);
        }

        public void LoadStory(string name)
        {
            int index = stories.FindIndex(x => x.Name == name);

            if (!stories[index].Unlocked) return;

            LoadScene(index);
        }

        public void ReloadCurrentStory()
        {
            if (stories.Count <= 0) return;
            SceneManager.LoadScene(stories[currentSceneIndex].Name);
        }

        public void ResetProgress()
        {
            for(int i = 0; i < stories.Count; i++)
            {
                stories[i] = new StoryMeta(stories[i].Name);
            }

            currentSceneIndex = default;
        }
    }
}