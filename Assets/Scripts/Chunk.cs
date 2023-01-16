using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    //Attributes:
    public Vector2 flag_position;
    public Vector2 perimeter;
    public Vector2 offset;
    private const float radius = 0.25f;

    //Properties:
    public Vector2 FlagPosition { get => (Vector2)transform.position + flag_position; }
    public Vector2 Centre { get => perimeter + offset; }

    //Methods:
    public virtual void Spawn() {  }
    public virtual void Despawn() {  }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere((Vector2)transform.position + flag_position, radius);
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, (Vector3)perimeter);
    }
}
