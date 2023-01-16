using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    //Attributes:
    public Vector2 perimeter;
    public Vector2 offset;

    //Methods:
    public virtual void Spawn() {  }
    public virtual void Despawn() {  }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Chunk temp = GameObject.Instantiate(this).GetComponent<Chunk>();
        temp.transform.position = transform.position;
        temp.transform.position += (Vector3.right * perimeter.x);
        temp.transform.SetParent(transform.parent);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, perimeter);
    }
}
