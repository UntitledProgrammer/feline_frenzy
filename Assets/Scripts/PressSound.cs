using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.UI
{
    /**
     *  A simple script for playing a sound effect when a UI element clicked on.
     *  (For an element to be 'clicked on' the mouse has to be over the defined rectangle
     *  whilst the left mouse button is down).
     *  
     *  @owner Stefana Tiurean with guidance from Thomas Jacobs. 
     *  @project Feline Frenzy
     *  @date 27/3/23
     */
    [RequireComponent(typeof(AudioSource), typeof(Animator))]
    public class PressSound : MonoBehaviour
    {
        [Range(0.0f, 1.0f)] public float volume;
        private AudioSource audioSource;
        private Animator animator;
        [SerializeField] private float width, height;
        [SerializeField] private Vector2 offset;
        public AudioClip soundEffect;
        private const int MOUSE_LEFT = 0;

        //Properties:

        /**
         *  The pivot is the position of the gameObject plus the offset.
         * 
         *  @return Vector2 resulting from the addition of the position and offset.
         */
        public Vector2 Pivot { get => (Vector2)transform.position + offset; }
        public Vector2 TopRight { get => Pivot + Vector2.right * (width/2) + Vector2.up * (height/2); }
        public Vector2 BottomLeft { get => Pivot + Vector2.left * (width / 2) + Vector2.down * (height/2); }
        public Vector2 Size { get => new Vector2(width, height); }

        // Start is called before the first frame update
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetMouseButtonDown(MOUSE_LEFT)) return;

            Vector2 mousePosition = Input.mousePosition;

            Debug.Log("Click");

            if (mousePosition.x >= BottomLeft.x && mousePosition.x <= TopRight.x &&
                mousePosition.y >= BottomLeft.y && mousePosition.y <= TopRight.y)
            {
                Debug.Log("Event Detected");
                audioSource.PlayOneShot(soundEffect, volume);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(Pivot, Size);
        }
    }
}