using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveInput;
    public float moveSpeed;

    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    public LayerMask Ground;
    public Animator animator;

    public float jumpForce;
    public float jumpTimerBase;
    private float jumpTimer;
    private bool isJumping;
    public ParticleSystem jumpParticle;
    
    private Rigidbody2D rb;

    public GameObject body;
    
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
            body.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput<0)
        {
            body.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (rb.velocity == Vector2.zero)
            animator.SetBool("isRunning", false);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpParticle.Play();
            isJumping = true;
            jumpTimer = jumpTimerBase;
            rb.velocity = Vector2.up*jumpForce;
        }
        if(Input.GetKey(KeyCode.Space) && isJumping)
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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;  
        }
    }

    private void FixedUpdate()
    {
        moveInput = 0;
        
        if (Input.GetKey(KeyCode.Q)) moveInput -= 1;
        if (Input.GetKey(KeyCode.D)) moveInput += 1;
        Move();

        if (moveInput == 0)
            animator.SetBool("isRunning", false);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetBool("isRunning", true);
    }
}
