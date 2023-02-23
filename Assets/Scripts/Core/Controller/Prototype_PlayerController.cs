using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    public class Prototype_PlayerController : Translatable
    {
        //Attributes:
        [Header("Run Settings")]
        public float horizontalSpeed;
        public float horizontalJumpSpeed;
        private Animator m_animator;
        private const string verticalKey = "vertical";

        [Header("Jump Settings")]
        public float jumpForce;
        public Vector2 bounds;
        public KeyCode spaceKey;
        public float raycastLength;

        //Properties:
        public bool IsGrounded { get => Physics2D.BoxCast(transform.position, bounds, 0.0f, Vector2.down, raycastLength).collider != null; }
        private float Velocity { get => IsGrounded ? horizontalSpeed : horizontalJumpSpeed; }
        //private Vector3 DashPosition { get => transform.position + Vector3.right * dashDistance; }

        //Methods:
        public override void Awake()
        {
            base.Awake();

            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            ProcessTranslators();

            //Horizontal movement.
            m_rigidbody.AddForce(Vector2.right * Velocity * Time.deltaTime, ForceMode2D.Impulse);

            //Animation.
            m_animator.SetFloat(verticalKey, m_rigidbody.velocity.y);
        }

        private void OnDrawGizmos()
        {
            //Draw raycast that checks whether a player is grounded.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastLength);
            Gizmos.DrawWireCube(transform.position + Vector3.down * raycastLength, bounds);
        }
    }
}