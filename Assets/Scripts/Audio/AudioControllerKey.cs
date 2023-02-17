using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Audio
{
    public class AudioDefault : AudioClipMeta
    {
        //Attributes:
        public bool active;

        //Constructor:
        public AudioDefault(AudioClip audioClip, string name) : base(audioClip, name) { }

        //Properties:
        public override bool Active { get => active; }
    }

    /**
    *  An extension of the audio controller; scriptable object which wraps an audio clips index around a string
    *  that can be instead used to reference the audio clip.
    * 
    *  @author Thomas Jacobs.
    *  @project Feline Frenzy.
    *  @date 17/2/23.
    */
    [CreateAssetMenu(fileName = "AudioControllerKey", menuName = "Audio/AudioControllerKey", order = 0)]
    public class AudioControllerKey : AudioController<AudioDefault>
    {
        //Attributes:
        private Dictionary<string, int> keyToIndex = new Dictionary<string, int>();

        //Methods:
        public void Remove(string key)
        {
            if(keyToIndex.ContainsKey(key)) { return; }

            int i = keyToIndex[key];

            //If audio clip is not at the back of the array swap the clip at the back with clip being removed.
            if (audibles.Count - 1 != i)
            {
                audibles[i] = audibles[audibles.Count - 1];
                keyToIndex[audibles[i].name] = i;
            }

            keyToIndex.Remove(key);
            audibles.RemoveAt(audibles.Count - 1);
        }

        //Operators:
        public AudioClip this[string key]
        {
            get => keyToIndex.ContainsKey(key)? audibles[keyToIndex[key]].audioClip : null;

            set
            {
                if(keyToIndex.ContainsKey(key))
                {
                    audibles[keyToIndex[key]].audioClip = value;
                    return;
                }

                audibles.Add(new AudioDefault(value, key));
                keyToIndex.Add(key, audibles.Count - 1);
            }
        }

        public AudioClip this[int index]
        {
            get => audibles.Count < index ? audibles[index].audioClip : null;
        }
    }
}