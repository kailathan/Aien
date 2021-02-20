using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 40f;

    private bool jump = false;

    private Rigidbody2D rb;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
    }
    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || jump)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        else
        {
            float friction = 0.1f;
            if (controller.isGrounded())
            {
                friction = 8;
            }

            int direction = -1;
            if (rb.velocity.x > 0)
            {
                direction = 1;
            }

            if (Mathf.Abs(rb.velocity.x) > 0.5)
            {
                rb.AddForce(new Vector2(-1 * direction * friction, 0));
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            anim.SetFloat("Speed", 0);
        }

        jump = false;

    }
}
