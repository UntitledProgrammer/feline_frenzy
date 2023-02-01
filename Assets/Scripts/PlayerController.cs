using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    //Attributes:
    public float horizontalSpeed;
    public float horizontalJumpSpeed;
    public float jumpForce;
    public KeyCode spaceKey;
    public float raycastLength;
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;
    private const string jumpKey = "jump";
    private const string verticalKey = "vertical";
    private const float jumpStaminaMax = 0.4f;
    private float jumpStamina;

    //Properties:
    public bool IsGrounded { get => Physics2D.Raycast(m_rigidbody.transform.position, Vector2.down, raycastLength).collider != null; }
    private float Velocity { get => IsGrounded ? horizontalSpeed : horizontalJumpSpeed; }

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
        if(Input.GetKeyDown(spaceKey) && isGrounded) jumpStamina = jumpStaminaMax;

        else if(Input.GetKey(spaceKey) && jumpStamina > 0.0f)
        {
            m_rigidbody.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            jumpStamina -= Time.deltaTime;
        }
        
        //Horizontal movement.
        m_rigidbody.AddForce(Vector2.right * Velocity * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        //Draw raycast that checks whether a player is grounded.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastLength);
    }
}