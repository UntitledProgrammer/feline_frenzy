using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  A namespace containing all core and essential logic and programs that handle audio.
*/
namespace FelineFrenzy.Audio
{
    /**
     *  An abstract base class declaring the essential information about an audio clip which an
     *  audio controller can use via generics (template).
     * 
     *  @author Thomas Jacobs.
     *  @project Feline Frenzy.
     *  @date 17/2/23.
     */
    public abstract class AudioClipMeta
    {
        //Attributes:
        public AudioClip audioClip;
        public string name;
        [HideInInspector] public bool hidden;

        //Constructor:
        public AudioClipMeta(AudioClip audioClip, string name)
        {
            this.audioClip = audioClip;
            this.name = name;
            hidden = false;
        }

        //Properties:
        public abstract bool Active { get; }
    }

    /**
    *  A type of scriptable object designed to carry a list of "AudioClipMeta" instances that a monobehaviour can 
    *  reference when implementing audible effects/functionality.
    * 
    *  @author Thomas Jacobs.
    *  @project Feline Frenzy.
    *  @date 17/2/23.
    */
    [System.Serializable] public class AudioController<T> : ScriptableObject where T : AudioClipMeta
    {
        //Attributes:
        public List<T> audibles = new List<T>();
    }
}