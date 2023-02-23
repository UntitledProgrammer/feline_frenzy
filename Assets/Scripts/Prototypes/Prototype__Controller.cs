using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

namespace Feline.Frenzy.Prototypes
{
    [System.Serializable]
    public struct Stamina
    {
        //Attributes:
        private float currentValue;
        [SerializeField] private float maximumValue;

        //Constructor:
        public Stamina(float maximumValue)
        {
            this.maximumValue = maximumValue;
            currentValue = maximumValue;
        }

        //Properties:
        public float Value { get => currentValue; }
        public float MaximumValue { get => maximumValue; }

        //Methods:
        public void Reset() => currentValue = maximumValue;
        public bool Subtract(float substitution)
        {
            if (substitution > currentValue) return false;

            currentValue -= substitution;

            return true;
        }
        public void Add(float addition)
        {
            currentValue += addition;
            if (currentValue > addition) currentValue = maximumValue;
        }
    }

    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Prototype__Controller : MonoBehaviour
    {
        //Attributes:
        private Rigidbody2D uRigidbody;
        private Animator uAnimator;

        [Header("Attributes")]
        public float groundedVelocity;
        public float airVelocity;
        public Stamina stamina;

        [Header("Jump")]
        public Vector2 bounds;
        public float jumpForce;
        public float jumpRaycast;
        public float jumpCost;

        [Header("Dash")]
        public float dashDistance;
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
            //Horizontal movement.
            uRigidbody.AddForce(uRigidbody.transform.right * Velocity * Time.deltaTime, ForceMode2D.Force);

            if (IsGrounded) Grounded();
            else Air();
        }

        private void OnDrawGizmos()
        {
            //Draw raycast that checks whether a player is grounded.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * jumpRaycast);
            Gizmos.DrawWireCube(transform.position + Vector3.down * jumpRaycast, bounds);
        }

        private void Air()
        {
            //Continue to jump
            if (Input.GetKey(jumpKey) && stamina.Subtract(jumpCost))
            {
                uRigidbody.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            }

            //Dash.
            else if (Input.GetKeyDown(dashKey))
            {
                Debug.Log("Dash");
            }    

        }

        private void Grounded()
        {
            stamina.Add(Time.deltaTime);

            //Jump.
            if(Input.GetKeyDown(jumpKey) && stamina.Subtract(jumpCost))
            {
                uRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }

            //Slide.
            else if(Input.GetKeyDown(slideKey)) { Debug.Log("Dash"); }
        }
    }
}