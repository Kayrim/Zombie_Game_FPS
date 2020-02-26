using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    bool isGrounded;
    public bool isCrouched = false;
    public CharacterController controller;
    public float moveSpeed = 12f;
    public float jumpHeight = 10f;
    float playerHeight;
    float crouchHeight;
    
    public float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundMask;

    private void Start()
    {
        playerHeight = controller.height;
        crouchHeight = controller.height / 2;
       
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Jump();
        Crouch();
        
        


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;
        

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
            moveSpeed = moveSpeed / 2;
            groundDistance = 0;
         
        }
        else if(isCrouched && !Input.GetKey(KeyCode.LeftControl))
        { 
            controller.height = playerHeight;
            isCrouched = false;
            moveSpeed = moveSpeed * 2;
            groundDistance = 0.25f;

        }
    }
}
