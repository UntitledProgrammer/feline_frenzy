using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    //Attributes:
    public Vector2 perimeter;
    public BoxCollider2D trigger;
    public string tag;

    //Methods:
    private void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (trigger.tag != tag) return;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, perimeter);
    }
}
