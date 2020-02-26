using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    bool isGrounded;
    public bool isCrouched = false;
    public bool isSprinting = false;
    public CharacterController controller;
    public float moveSpeed = 12f;
    float tempSpeed;
    public float jumpHeight = 10f;
    public float sprintMultiplier = 1.5f;
    float playerHeight;
    float crouchHeight;    
    public float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundMask;
    private Vector3 current_pos;

    private void Start()
    {
        playerHeight = controller.height;
        crouchHeight = controller.height / 2;
        tempSpeed = moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Jump();
        Crouch();
        Sprint();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Storing the inputs
        Vector3 move = transform.right * x + transform.forward * z;

        // Applying the inputs to the move Function

        controller.Move(move * moveSpeed * Time.deltaTime);
        controller.enableOverlapRecovery = true;

        // Creating a force for grativy 
        velocity.y -= gravity * Time.deltaTime;
        
        // Applying gravity every frame
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded)
        {
            velocity.y = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y += jumpHeight;
            }

        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
    }

    void Crouch()
    {
        
        if (!isCrouched && Input.GetKey(KeyCode.LeftControl))
        {

            controller.height -= crouchHeight;
            isCrouched = true;            
            moveSpeed = tempSpeed * 0.75f;
            groundDistance = 0;

        }        
        else if(isCrouched && !Input.GetKey(KeyCode.LeftControl))
        { 
            controller.height = playerHeight;
            isCrouched = false;
            moveSpeed = tempSpeed;
            groundDistance = 0.25f;

        }
    }

    void Sprint()
    {
        if (isGrounded)
        {
            if (!isSprinting && Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = !isSprinting;
                moveSpeed = tempSpeed * sprintMultiplier;
            }
            else if (isSprinting && !Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = !isSprinting;
                moveSpeed = tempSpeed;
            }
        }
        else return;
    }
}
