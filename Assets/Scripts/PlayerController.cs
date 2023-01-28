using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    //Attributes:
    public float horizontalSpeed;
    public float jumpForce;
    public KeyCode spaceKey;
    public float raycastLength;
    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;
    private Animator m_animator;
    private const string jumpKey = "jump";
    private const string verticalKey = "vertical";

    //Properties:
    bool IsGrounded { get => Physics2D.Raycast(m_collider.transform.position, Vector2.down, raycastLength).collider != null; }

    //Methods:
    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_animator.SetFloat(verticalKey, m_rigidbody.velocity.y);

        if (Input.GetKeyDown(spaceKey) && IsGrounded) { m_animator.SetTrigger(jumpKey); m_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); }
        if(IsGrounded) m_rigidbody.AddForce(Vector2.right * horizontalSpeed * Time.deltaTime, ForceMode2D.Impulse);

        Debug.Log(m_rigidbody.velocity.x);
    }

    private void OnDrawGizmos()
    {
        //Draw raycast that checks whether a player is grounded.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastLength);
    }
}