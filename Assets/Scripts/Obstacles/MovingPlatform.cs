using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 start;
    public Vector2 end;

    public Vector2 StartPoint {get => (Vector2)transform.position + start;}
    public Vector2 EndPoint {get => (Vector2)transform.position + end;}
    public float timer;
    public float delay;

    private const float TOLLERANCE = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        start = StartPoint;
        end = EndPoint;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(StartPoint, EndPoint);
        Gizmos.DrawWireSphere(StartPoint, 0.25f);
        Gizmos.DrawWireSphere(EndPoint, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(start, end, timer / delay);
        if(Vector2.Distance(transform.position, end) <= TOLLERANCE)
        {
            Vector2 temp = start;
            start = end;
            end = temp;
            timer = 0;
        }
    }
}
