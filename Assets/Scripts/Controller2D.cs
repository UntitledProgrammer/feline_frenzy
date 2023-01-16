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
    public float distance;

    //Properties:
    public bool is_grounded { get { return Physics2D.Raycast(transform.position, -transform.up, distance).collider != null; } }

    //Methods:
    private void Start() => rigid_body = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (Input.GetKeyDown(jump_key) && is_grounded) { rigid_body.AddForce(transform.up * jump_force, ForceMode2D.Impulse); }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * distance));
    }
}
