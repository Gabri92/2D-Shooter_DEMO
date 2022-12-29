using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool isGunPressed = false;
    bool isShooting = false;
    private float xNegLimit = -11;
    private float xPosLimit = 10;

    // Update is called once per frame
    void Update()
    { 
        if(gameObject != null)
        {
            MovePlayer();

            ConstraintMovement();

            Jump();

            PrepareForShooting();

            Shoot();
        }
    }

    // Control the landing animation
    public void onLanding()
    {
            animator.SetBool("isJumping", false);
    }

    // Control the shooting animation
    void Shoot()
    {
        isShooting = Input.GetButtonDown("Fire1");
        if (isShooting && isGunPressed)
        {
            animator.SetBool("isShot", true);
        }
    }

    // It works like 'Update' function, but instead of being called
    // once every frame it is called a fixed amount of time per second
    void FixedUpdate() 
    {
        if (gameObject != null)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;

            animator.SetBool("isShot", false);
        }
    }

    // Control player movement's input and animation
    void MovePlayer()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    // Constraint the player movements into the scene
    void ConstraintMovement()
    {
        if (transform.position.x < xNegLimit)
        {
            transform.position = new Vector3(xNegLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xPosLimit)
        {
            transform.position = new Vector3(xPosLimit, transform.position.y, transform.position.z);
        }
    }

    // Control Jump animation
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
    }
    
    // Control 'Prepare for shooting' animation
    void PrepareForShooting()
    {
        isGunPressed = Input.GetKey("left ctrl");
        if (isGunPressed && horizontalMove == 0)
        {
            animator.SetBool("isGunPressed", true);
        }
        else
        {
            animator.SetBool("isGunPressed", false);
        }
    }

    // Manage collisions with an enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject != null)
        {
            Debug.Log("Game Over!");
        }
    }
}


