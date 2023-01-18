using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpringArm : MonoBehaviour
{
    //Attributes:
    public float force;
    public float raycastDistance;
    private Vector2 pivot;
    private Rigidbody2D m_rigidbody;
    private const float tolerance = 0.1f;

    //Properties:
    private bool IsClear { get => Physics2D.Raycast(transform.position, Vector2.right, raycastDistance).collider == null; }

    //Methods:
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.position;
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(pivot.x - transform.position.x) <= tolerance || !IsClear) return;

        Vector2 direction = Vector2.right * (pivot.x - transform.position.x);
        m_rigidbody.AddForce(Time.deltaTime * force * direction, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * raycastDistance);
    }
}
