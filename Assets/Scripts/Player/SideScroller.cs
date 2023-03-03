using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool grounded;

    public float Speed;
    public float JumpForce;
    private SpriteRenderer Srenderer; 

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Srenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(HorizontalInput * Speed, rb2d.velocity.y);

        //flipping player sprite
        if(Input.GetAxis("Horizontal") != 0f)
        {
            Srenderer.flipX = (HorizontalInput < -0.001f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        anim.SetBool("Running?", HorizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }
    private void Jump()
    {
        if (grounded = true){
            rb2d.velocity = new Vector2(rb2d.velocity.x, Speed);
            anim.SetTrigger("Jump");
            grounded = false;
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
