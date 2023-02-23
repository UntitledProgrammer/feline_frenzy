using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

namespace Feline.Frenzy
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
    }

    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Prototype__Controller2D : MonoBehaviour
    {
        //Attributes:
        private Rigidbody2D uRigidbody;
        private Animator uAnimator;

        [Header("Attributes")]
        public float groundedVelocity;
        public float airVelocity;
        public Stamina stamina;

        [Header("Jump")]
        public float jumpForce;
        public float jumpRaycast;

        [Header("Dash")]
        public float dashDistance;

        [Header("Slide")]
        public float slideVelocity;

        [Header("Input")]
        public KeyCode jumpKey;
        public KeyCode dashKey;
        public KeyCode slideKey;

        //Properties:
        public bool IsGrounded { get => Physics2D.Raycast(transform.position, -transform.up, jumpRaycast).collider; }
        public float Velocity { get => IsGrounded ? groundedVelocity : airVelocity; }

        //Methods:
        private void Awake()
        {
            //Initialise components.
            uAnimator = GetComponent<Animator>();
            uRigidbody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            //Horizontal movement.
            uRigidbody.AddForce(uRigidbody.transform.right * Velocity * Time.deltaTime, ForceMode2D.Force);
        }

        private void OnDrawGizmos()
        {
            //Jump:
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * jumpRaycast);
        }

        private void Air()
        {
            if()
        }
    }
}