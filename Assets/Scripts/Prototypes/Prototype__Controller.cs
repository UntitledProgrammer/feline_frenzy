using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

namespace FelineFrenzy.Prototypes
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(AudioSource))]
    public class Prototype__Controller : Game.Respawnable
    {
        //Attributes:
        private Rigidbody2D uRigidbody;
        private Animator uAnimator;
        private AudioSource uAudioSource;
        private const string verticalString = "vertical", jumpString = "jump";
        private const string jumpAnim = "JUMP", slideAnim = "SLIDE", dashAnim = "DASH", groundedAnim = "GROUNDED";

        [Header("Attributes")]
        public float groundedVelocity;
        public float airVelocity;
        public float staminaRecovery;
        public UI.Stamina stamina;

        [Header("Jump")]
        public Vector2 bounds;
        public float jumpForce;
        public float jumpRaycast;
        public float jumpCost;

        [Header("Dash")]
        public float dashVelocity;
        public float dashCost;
        public float dashTime;

        [Header("Slide")]
        public float slideVelocity;
        public float slideCost;
        public float slideTime;

        [Header("Input")]
        public KeyCode jumpKey;
        public KeyCode dashKey;
        public KeyCode slideKey;

        [Header("Sound Effects")]
        public AudioClip jumpSoundEffect;
        public AudioClip slideSoundEffect;
        public AudioClip dashSoundEffect;

        [Header("OnDestroy")]
        public UnityEvent onRespawn;
        private bool doubleJump = true;

        //Properties:
        public bool IsGrounded { get => Physics2D.BoxCast(transform.position, bounds, 0.0f, Vector2.down, jumpRaycast).collider; }
        public float Velocity { get => IsGrounded ? groundedVelocity : airVelocity; }

        //Methods:
        private void Awake()
        {
            //Initialise components.
            uAnimator = GetComponent<Animator>();
            uRigidbody = GetComponent<Rigidbody2D>();
            uAudioSource = GetComponent<AudioSource>();
            stamina.Reset();
        }

        private void Update()
        {
            //Animation.
            uAnimator.SetBool(groundedAnim, IsGrounded);

            //Horizontal movement.
            uRigidbody.AddForce(uRigidbody.transform.right * Velocity * Time.deltaTime, ForceMode2D.Impulse);

            //State logic.
            if (IsGrounded) { Grounded(); }
            else Air();
        }

        private void Air()
        {
            //Dash.
            if (Input.GetKeyDown(dashKey) && stamina.Subtract(dashCost))
            {
                uAnimator.SetTrigger(dashAnim);
                uAudioSource.PlayOneShot(dashSoundEffect);
                StartCoroutine(Dash(dashTime, dashVelocity));
            }
            else if (Input.GetKeyDown(jumpKey) && doubleJump && stamina.Subtract(jumpCost))
            {
                doubleJump = false;
                uRigidbody.velocity -= uRigidbody.velocity * Vector2.up;
                uAnimator.SetTrigger(jumpAnim);
                uRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                uAudioSource.PlayOneShot(jumpSoundEffect);
            }

            else stamina.Add(staminaRecovery * Time.deltaTime);
        }

        private void Grounded()
        {
            //Recover stamina.
            stamina.Add(staminaRecovery * Time.deltaTime);
            doubleJump = true;

            //Jump.
            if (Input.GetKeyDown(jumpKey) && stamina.Subtract(jumpCost))
            {
                uAnimator.SetTrigger(jumpAnim);
                uRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                uAudioSource.PlayOneShot(jumpSoundEffect);
            }

            //Slide.
            else if(Input.GetKeyDown(slideKey) && stamina.Subtract(slideCost)) 
            { 
                uAnimator.SetTrigger(slideAnim); 
                StartCoroutine(Dash(slideTime, slideVelocity)); 
                uAudioSource.PlayOneShot(slideSoundEffect); 
            }
        }

        private IEnumerator Dash(float delay, float speed)
        {
            float temp = airVelocity;
            airVelocity = speed;
            uRigidbody.velocity = Velocity * Time.deltaTime * transform.right;
            yield return new WaitForSecondsRealtime(delay);
            airVelocity = temp;
        }

        public override void Respawn() { stamina.Reset(); onRespawn.Invoke(); }

        //For development only.
        private void OnDrawGizmos()
        {
            //Draw raycast that checks whether a player is grounded.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * jumpRaycast);
            Gizmos.DrawWireCube(transform.position + Vector3.down * jumpRaycast, bounds);
        }
    }
}