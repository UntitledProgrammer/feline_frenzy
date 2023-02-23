using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Issue
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        //Attributes:
        [Header("Run Settings")]
        public float horizontalSpeed;
        public float horizontalJumpSpeed;

        [Header("Jump Settings")]
        public float jumpForce;
        public Vector2 bounds;
        public KeyCode spaceKey;
        public float raycastLength;

        [Header("Dash Settings")]
        public float dashDistance;
        public KeyCode dashKey;

        private float radius;
        private Rigidbody2D m_rigidbody;
        private Animator m_animator;
        private const string jumpKey = "jump";
        private const string verticalKey = "vertical";
        private const float jumpStaminaMax = 0.4f;
        private float jumpStamina;

        //Properties:
        public bool IsGrounded { get => Physics2D.BoxCast(transform.position, bounds, 0.0f, Vector2.down, raycastLength).collider != null; }
        private float Velocity { get => IsGrounded ? horizontalSpeed : horizontalJumpSpeed; }
        private Vector3 DashPosition { get => transform.position + Vector3.right * dashDistance; }

        //Methods:
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //Cache raycast.
            bool isGrounded = IsGrounded;

            //Animation.
            m_animator.SetBool(jumpKey, !isGrounded);
            m_animator.SetFloat(verticalKey, m_rigidbody.velocity.y);

            //Jump mechanic.
            if (Input.GetKeyDown(spaceKey) && isGrounded) jumpStamina = jumpStaminaMax;

            else if (Input.GetKey(spaceKey) && jumpStamina > 0.0f)
            {
                m_rigidbody.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                jumpStamina -= Time.deltaTime;
            }

            else if (Input.GetKeyUp(KeyCode.Space) && !isGrounded) { jumpStamina = 0.0f; m_rigidbody.velocity *= new Vector2(1.0f, 0.25f); }

            //Horizontal movement.
            m_rigidbody.AddForce(Vector2.right * Velocity * Time.deltaTime, ForceMode2D.Impulse);
        }

        private void OnDrawGizmos()
        {
            //Draw raycast that checks whether a player is grounded.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastLength);
            Gizmos.DrawWireCube(transform.position + Vector3.down * raycastLength, bounds);

            //Draw dash distance:
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, DashPosition);
        }
    }
}