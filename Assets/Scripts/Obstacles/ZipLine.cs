using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLine : MonoBehaviour
{
    public Vector2 start;
    public Vector2 end;
    public Vector2 StartPoint {get => (Vector2)transform.position + start;}
    public Vector2 EndPoint { get => (Vector2)transform.position + end; }
    private Rigidbody2D target;
    public float timer;
    public float delay;
    private const float tollerance = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        //drawing gismos to show where the player whill be travelling across
        Gizmos.color = Color.black;
        Gizmos.DrawLine(StartPoint, EndPoint);
        Gizmos.DrawWireSphere(StartPoint, 0.25f);
        Gizmos.DrawWireSphere(EndPoint, 0.25f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player presses space while colliding with box trigger, disables player rigidbody2D compnent 
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D component))
        {
            target = component;
            target.isKinematic = true;
            Debug.Log("Collision Enter");
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }
        //calls disabele function if player presses space while on zipline
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Disable();
            return;
        }
        timer += Time.deltaTime;
        //allows the player to move from StartPoint to EndPoint
        target.transform.position = Vector3.Lerp(StartPoint, EndPoint, timer / delay);
        if (Vector2.Distance(target.transform.position, EndPoint) < tollerance)
        {
            Disable();
            return;
        }
    }
    private void Disable()
    {
        //disables ziplines 
        target.isKinematic = false;
        target = null;
    }
}
