using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveInput;
    public float moveSpeed;

    private bool jump;
    public float jumpPower;
    public float jumpDuration;
    private float jumpDurationBase;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    private Rigidbody2D rb;

    private void Start()
    {
        jumpDurationBase = jumpDuration;
        if (!TryGetComponent(out rb))
        {
            Debug.Log("no rigidbody" + name);
        }
    }

    public void Update()
    {
        jump = Input.GetButton("Jump");

        moveInput = Input.GetAxis("Move");
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    private void Jump()
    {
        if (jump && jumpDuration>0)
        {
            jumpDuration -= Time.deltaTime;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);
    }
}
