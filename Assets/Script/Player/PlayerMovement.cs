using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveInput;
    public float moveSpeed;

    private bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    public LayerMask Ground;

    public float jumpForce;
    public float jumpTimerBase;
    private float jumpTimer;
    private bool isJumping;
    
    private Rigidbody2D rb;

    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.Log("no rigidbody" + name);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, Ground);
        Jump();

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput<0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = jumpTimerBase;
            rb.velocity = Vector2.up*jumpForce;
        }
        if(Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimer > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;  
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Move");
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
