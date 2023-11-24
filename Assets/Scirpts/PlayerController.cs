using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public float moveSpeed;
    public Rigidbody2D TheRb;
    public float jumpForce;
    public float runSpeed;
    private float activeSpeed;

    private bool isGrounded; //Keeps player on the ground, no infinite jumps
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool canDoubleJump;

    public Animator anim;


   // public float floatingForce = 10f;

    void Start()
    {
       // TheRb = GetComponent<Rigidbody2D>();
        //TheRb.gravityScale = 0f; ((Something i need to figure out later. Trying to make the character floaty like a Spirit.))
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        //TheRb.AddForce(Vector2.up * floatingForce);
        //TheRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, TheRb.velocity.y);

        activeSpeed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;

        }



        TheRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, TheRb.velocity.y);



        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                
                Jump();
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump == true)
                {
                    //TheRb.velocity = new Vector2(TheRb.velocity.x, jumpForce);
                    Jump();
                    canDoubleJump = false;
                }
            }
        }
        //im gonna be honest, forgot what this does
        if(TheRb.velocity.x > 0)
        {
           transform.localScale = Vector3.one;
        }
        if(TheRb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        //handle animations
        anim.SetFloat("speed", Mathf.Abs(TheRb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", TheRb.velocity.y);


    }
    //custom function since i repeated jump alot in code
    void Jump()
    {
        TheRb.velocity = new Vector2(TheRb.velocity.x, jumpForce);
    }

}
