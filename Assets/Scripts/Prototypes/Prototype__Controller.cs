using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

namespace FelineFrenzy.Prototypes
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Prototype__Controller : MonoBehaviour
    {
        //Attributes:
        private Rigidbody2D uRigidbody;
        private Animator uAnimator;
        private const string verticalString = "vertical", jumpString = "jump";

        [Header("Attributes")]
        public float groundedVelocity;
        public float airVelocity;
        public UI.Stamina stamina;

        [Header("Jump")]
        public Vector2 bounds;
        public float jumpForce;
        public float jumpRaycast;
        public float jumpCost;

        [Header("Dash")]
        public float dashVelocity;
        public float dashCost;

        [Header("Slide")]
        public float slideVelocity;
        public float slideCost;

        [Header("Input")]
        public KeyCode jumpKey;
        public KeyCode dashKey;
        public KeyCode slideKey;

        //Properties:
        public bool IsGrounded { get => Physics2D.BoxCast(transform.position, bounds, 0.0f, Vector2.down, jumpRaycast).collider; }
        public float Velocity { get => IsGrounded ? groundedVelocity : airVelocity; }


        //Methods:
        private void Awake()
        {
            //Initialise components.
            uAnimator = GetComponent<Animator>();
            uRigidbody = GetComponent<Rigidbody2D>();
            stamina.Reset();
        }

        private void Update()
        {
            Debug.Log(stamina.Decimal);
            //stamina.Add(Time.deltaTime);

            //Animation.
            uAnimator.SetFloat(verticalString, uRigidbody.velocity.y);
            uAnimator.SetBool(jumpString, IsGrounded);

            //Horizontal movement.
            uRigidbody.AddForce(uRigidbody.transform.right * Velocity * Time.deltaTime, ForceMode2D.Impulse);

            if (IsGrounded) { Grounded(); }
            else Air();
        }

        private void Air()
        {
            //Dash.
            if (Input.GetKeyDown(dashKey) && stamina.Subtract(dashCost))
            {
                Debug.Log("Dash");
                StartCoroutine(Dash(1.0f, dashVelocity));
            }    
        }

        private void Grounded()
        {
            //Jump.
            if(Input.GetKeyDown(jumpKey) && stamina.Subtract(jumpCost))
            {
                uRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }

            //Slide.
            else if(Input.GetKeyDown(slideKey) && stamina.Subtract(slideCost)) { Debug.Log("Slide"); StartCoroutine(Dash(0.25f, slideVelocity)); }
        }

        private IEnumerator Dash(float delay, float speed)
        {
            float temp = airVelocity;
            airVelocity = speed;
            uRigidbody.velocity = Velocity * Time.deltaTime * transform.right;
            yield return new WaitForSecondsRealtime(delay);
            airVelocity = temp;
        }

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