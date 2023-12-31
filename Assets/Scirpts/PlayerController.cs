using System;
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


    public BulletController shotToFire;
    public Transform shotPoint;
    public float dashSpeed, dashTime;

    private float dashCounter;

    public SpriteRenderer theSR, afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;

    private PlayerAbilityTracker abilities;

    public bool canMove;

    public float knockbackLength, knockbackSpeed;
    private float knockbackCounter;
    /*public int shotsFiredCount = 0;
    private float shotCooldown = 1.5f;
    private float shotTimer = 0f; */
   // public float floatingForce = 10f;

    void Start()
    {

        abilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;

       // TheRb = GetComponent<Rigidbody2D>();
        //TheRb.gravityScale = 0f; ((Something i need to figure out later. Trying to make the character floaty like a Spirit.))
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {


            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);


            if (knockbackCounter <= 0)
            {



                activeSpeed = moveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    activeSpeed = runSpeed;

                }



                TheRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, TheRb.velocity.y);



                if (Input.GetButtonDown("Jump") && (isGrounded || (canDoubleJump && abilities.canDoubleJump)))
                {
                    if (isGrounded == true)
                    {

                        Jump();
                        canDoubleJump = true;
                    }
                    else
                    {
                        if (canDoubleJump == true)
                        {

                            Jump();
                            canDoubleJump = false;
                        }



                    }


                }
                /*  if(shotTimer > 0)
                  {
                      shotTimer -= Time.deltaTime;
                  }*/

                // if (shotTimer <= 0f && shotsFiredCount < 3)
                // {

                if (Input.GetButtonDown("Fire1") /* && shotsFiredCount < 3 */)
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                    anim.SetTrigger("shotFired");


                    //   shotsFiredCount++;
                    //   shotTimer = shotCooldown;

                }

                //  }
                if (dashRechargeCounter > 0)
                {

                    dashRechargeCounter -= Time.deltaTime;

                }
                else
                {

                    if (Input.GetButtonDown("Fire2") && abilities.canDash)
                    {
                        dashCounter = dashTime;

                        ShowAfterImage();

                    }

                }

                if (dashCounter > 0)
                {
                    dashCounter = dashCounter - Time.deltaTime;

                    TheRb.velocity = new Vector2(dashSpeed * transform.localScale.x, TheRb.velocity.y);

                    afterImageCounter -= Time.deltaTime;
                    if (afterImageCounter <= 0)

                    {
                        ShowAfterImage();
                    }

                    dashRechargeCounter = waitAfterDashing;


                }

                else
                {





                    //im gonna be honest, forgot what this does
                    if (TheRb.velocity.x > 0)
                    {
                        transform.localScale = Vector3.one;
                    }
                    if (TheRb.velocity.x < 0)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }

                }
            }
            else
            {
                knockbackCounter -= Time.deltaTime;

                TheRb.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, TheRb.velocity.y);
            }
        }
        else
        {
            TheRb.velocity = Vector2.zero;
        }
            //handle animations
            anim.SetFloat("speed", Mathf.Abs(TheRb.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("ySpeed", TheRb.velocity.y);
        

    }
    public void ShowAfterImage()
    {

     SpriteRenderer image =   Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

    }



    //custom function since i repeated jump alot in code
    void Jump()
    {
        TheRb.velocity = new Vector2(TheRb.velocity.x, jumpForce);

        

    }
    public void KnockBack()
    {
        TheRb.velocity = new Vector2(0f, jumpForce * .5f);
        anim.SetTrigger("isKnockingBack");

        knockbackCounter = knockbackLength;

    }


}
