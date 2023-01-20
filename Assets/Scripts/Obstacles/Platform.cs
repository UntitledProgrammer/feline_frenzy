using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    //Attributes:
    public float delay;
    public Vector2 offset;
    public LayerMask m_mask;
    private Vector2 trigger_perimeter;
    private Collider2D m_collider;
    private bool m_active;

    //Methods:
    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        trigger_perimeter = m_collider.bounds.size;
        m_active = false;
    }

    private void Update()
    {
        Collider2D external = Physics2D.OverlapBox((Vector2)transform.position + offset, trigger_perimeter, 0.0f, m_mask);
        if (external == null || m_active) return;

        m_collider.enabled = false;
        m_active = true;
        StartCoroutine(Enable(delay));
    }

    private IEnumerator Enable(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        m_collider.enabled = true;
        m_active = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, trigger_perimeter);
    }
}