using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FelineFrenzy.Audio
{
    /**
     * An extension of the "AudioClipMeta" abstract base class; intended for implementing an audible response to keyboard input(s).
     * 
     *  @author Thomas Jacobs.
     *  @project Feline Frenzy.
     *  @date 17/2/23.
     */
    [System.Serializable] public class AudioKey : AudioClipMeta
    {
        //Enums:
        [System.Serializable] public enum KeyEvent { Up, Down, Hold }

        //Attributes:
        public KeyCode key;
        public KeyEvent keyEvent;
        public bool layerable;

        //Constructor:
        public AudioKey(AudioClip audioClip, string name) : base(audioClip, name) { }

        //Properties:
        public override bool Active
        {
            get
            {
                if (audioClip == null) return false;

                switch (keyEvent)
                {
                    case KeyEvent.Down: return Input.GetKeyDown(key);
                    case KeyEvent.Up: return Input.GetKeyUp(key);
                    default: return Input.GetKey(key);
                }
            }
        }
    }

    /**
    *  An extension of the audio controller; scriptable object, which uses the "audiokey" data-structure
    *  for ease implementing audible responses to keyboard input.
    * 
    *  @author Thomas Jacobs.
    *  @project Feline Frenzy.
    *  @date 17/2/23.
    */
    [CreateAssetMenu(fileName = "AudioInputController", menuName = "Audio/AudioInputController", order = 0)]
    public class AudioInputController : AudioController<AudioKey>
    {
        //Attributes:
        public bool overridable;
    }
}


/*
#if UNITY_EDITOR
[CustomEditor(typeof(AudioTest))]
public class EAudioTest : Editor
{
    //Attributes:
    private AudioTest self;
    private bool update;

    //Methods:
    private void OnEnable()
    {
        self = (AudioTest)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        update = false;
        EditorGUILayout.LabelField("Main Settings", EditorStyles.boldLabel);


        if (GUILayout.Button("Add Audible")) { self.audibles.Add(new AudioKey(null, "")); }

        for (int i = 0; i < self.audibles.Count; i++)
        {
            self.audibles[i].hidden = EditorGUILayout.BeginFoldoutHeaderGroup(self.audibles[i].hidden, self.audibles[i].name);

            if (self.audibles[i].hidden) { EditorGUILayout.EndFoldoutHeaderGroup(); continue; }

            //Draw attributes:
            self.audibles[i].name = EditorGUILayout.TextField("Name", self.audibles[i].name);
            self.audibles[i].audioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", self.audibles[i].audioClip, typeof(AudioClip), false);
            self.audibles[i].key = (KeyCode)EditorGUILayout.EnumPopup("Key Code", self.audibles[i].key);
            EditorGUILayout.EndFoldoutHeaderGroup();

            update = true;
        }

        if (!update) return;
        EditorUtility.SetDirty(self);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
*/