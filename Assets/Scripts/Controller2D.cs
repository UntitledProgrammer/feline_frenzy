using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Controller2D : MonoBehaviour
{
    //Attributes:
    public Rigidbody2D rigid_body;
    public float jump_force;
    public KeyCode jump_key;
    public float raycast_distance;
    private float jump_counter;
    private bool in_jump = false;
    private const float jump_max = 0.14f;

    //Properties:
    public bool IsGrounded { get { return Physics2D.Raycast(transform.position, -transform.up, raycast_distance).collider != null; } }

    //Methods:
    public void Jump()
    {

    }

    private void Start() { rigid_body = GetComponent<Rigidbody2D>(); jump_counter = jump_max; }

    private void Update()
    {
        if (!in_jump && !IsGrounded) return;

        if(jump_counter > 0.0f && Input.GetKey(jump_key))
        {
            rigid_body.AddForce(transform.up * jump_force * Time.deltaTime, ForceMode2D.Impulse);
            jump_counter -= Time.deltaTime;
            in_jump = true;
        }
        else if(jump_counter <= 0.0f || Input.GetKeyUp(jump_key))
        {
            jump_counter = jump_max;
            in_jump = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * raycast_distance));
    }
}
