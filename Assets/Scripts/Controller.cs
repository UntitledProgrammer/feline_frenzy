using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerAction
{
    RUN,
    JUMP,
    DASH,
    SLIDE,
    IDLE
}

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Controller : MonoBehaviour
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
    private PlayerAction currentAction;

    //Properties:
    public bool IsGrounded { get => Physics2D.BoxCast(transform.position, bounds, 0.0f, Vector2.down, raycastLength).collider != null; }
    private float Velocity { get => IsGrounded ? horizontalSpeed : horizontalJumpSpeed; }
    private Vector3 DashPosition { get => transform.position + Vector3.right * dashDistance; }

    //Methods:
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        currentAction = PlayerAction.IDLE;
    }

    private void Update()
    {
        //Cache raycast.
        bool isGrounded = IsGrounded;

        //Horizontal movement.
        m_rigidbody.AddForce(Vector2.right * Velocity * Time.deltaTime, ForceMode2D.Impulse);

        //Animation.
        m_animator.SetBool(jumpKey, !isGrounded);
        m_animator.SetFloat(verticalKey, m_rigidbody.velocity.y);

        switch (currentAction)
        {
            case PlayerAction.IDLE: currentAction = GetInput(); return;
            case PlayerAction.DASH: currentAction = Dash(); return;
            case PlayerAction.JUMP: currentAction = Jump(); jumpStamina = jumpStaminaMax; return;
            case PlayerAction.SLIDE: currentAction = Slide(); return;
            default: return;
        }

    }

    private PlayerAction GetInput()
    {
        if (!Input.anyKey) return PlayerAction.IDLE;

        if (Input.GetKeyDown(spaceKey) && IsGrounded) return PlayerAction.JUMP;
        else if (Input.GetKeyDown(dashKey)) return PlayerAction.DASH;

        return PlayerAction.IDLE;
    }

    private PlayerAction Jump()
    {
        if (jumpStamina <= 0.0f) return PlayerAction.IDLE;
        else if (Input.GetKeyUp(KeyCode.Space) && !IsGrounded) { jumpStamina = 0.0f; m_rigidbody.velocity *= new Vector2(1.0f, 0.25f); return PlayerAction.IDLE; }

        m_rigidbody.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        jumpStamina -= Time.deltaTime;

        return PlayerAction.JUMP;
    }

    private PlayerAction Slide()
    {
        return PlayerAction.IDLE;
    }

    private PlayerAction Dash()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, dashDistance);
        transform.position = hit.collider == null ? DashPosition : transform.position + transform.right * (hit.distance - bounds.x);

        return PlayerAction.IDLE;
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