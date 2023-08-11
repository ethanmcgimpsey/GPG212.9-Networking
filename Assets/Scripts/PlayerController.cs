﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
/// <summary>
/// Controls the player
/// </summary>

public class PlayerController : NetworkBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float runSpeed, walkSpeed, crouchSpeed, jumpSpeed;
    [SerializeField] public float curHealth;
    [SerializeField] public float _gravity = 20;
    //Struct - Contains Multiple Variables (eg...3 floats)
    private Vector3 _moveDir;
    [SerializeField] private Rigidbody rb = null;
    //Reference Variable
    [SerializeField] public Text hp;

    [SerializeField] public bool isZoomedIn;
    [SerializeField] public bool damaged;
    [SerializeField] public bool isGrounded;

    [SerializeField] private float verticalDirection;
    [SerializeField] private float horizontalDirection;



    void Update()
    {
        verticalDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        horizontalDirection = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontalDirection, 0, verticalDirection);


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        hp.text = curHealth.ToString();
        Move(horizontalDirection, verticalDirection);
    }
    /// <summary>
    /// Making the player be able to walking around
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    public void Move(float horizontal, float vertical)
    {
        bool isCrouchPressed = Input.GetButton("Crouch");
        bool isSprintPressed = Input.GetButton("Sprint");

        //set speed
        if (isCrouchPressed && isSprintPressed)
        {
            moveSpeed = walkSpeed;
        }
        else if (isSprintPressed)
        {
            moveSpeed = runSpeed;
        }
        else if (isCrouchPressed)
        {
            moveSpeed = crouchSpeed;
        }
        else if (isZoomedIn) //isZoomedIn == true
        {
            moveSpeed = crouchSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        //move this direction based off inputs
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    /// <summary>
    /// Recieve damage when getting shot from another player
    /// </summary>
    /// <param name="damage"></param>

    
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void SetMovement(Vector3 movement)
    {
        _moveDir = movement;
    }

    private void ResetMovement()
    {
        _moveDir = Vector3.zero;
    }
}
