using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //declaring the rigidbody component
    private Rigidbody2D rb2d;
    //declaring the animator component
    private Animator anim;
    //declaring the grounded variable
    private bool grounded;
    private Physics2D Phys2d;

    //declaring the movement variables
    private float Speed;
    public float JumpVelocity;

    public int Coins;

    private void Start()
    {
        Coins = 0;
        Speed = 6f;
    }

    private void Awake()
    {
        //gets the rigidbody 2d component from the player
        rb2d = GetComponent<Rigidbody2D>();
        //gets the animator component from the player
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        //stores the horizontal input for easy referencing
        float HorizontalInput = Input.GetAxis("Horizontal");
        //gets horizontal key inputs and multiplys by speed variable to make player move
        rb2d.velocity = new Vector2(HorizontalInput * Speed,rb2d.velocity.y);

        //flips player sprite depending on which direction player is moving
        if (HorizontalInput > 0.001f)
        {
            transform.localScale = Vector3.one;
        } else if (HorizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //check to see if player presses jump key
        if (Input.GetKey(KeyCode.Space))
        {
           if (grounded == true)
            {
                //moves the player on the y axis with the jumpvelocity variable
                jump();
            }
        }

        //set animator peramiters
        anim.SetBool("Running?", HorizontalInput != 0);
        anim.SetBool("grounded", grounded);

        //Sprint Logic
        if (Input.GetKey (KeyCode.LeftShift) && grounded && rb2d.velocity.x != 0)
        {
            Speed = Speed + 0.015f;
        }
        else
        {
            Speed = 6f;
        }
    }
    private void jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, JumpVelocity);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
