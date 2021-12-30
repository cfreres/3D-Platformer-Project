using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float gravity = -8.0f;
    public float runSpeed = 6.0f;
    public float walkSpeed = 3.0f;
    public float jumpHeight = 6.0f;
    public float dashSpeed = 8.0f;
    private int numDashes = 1;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    bool isRunning = true;
    Vector3 vertDirection;
    Vector3 vertVelocity;
    Vector3 dashVelocity;    
    // Update is called once per frame

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        vertDirection = Vector3.up;
        
        if(isGrounded && vertVelocity.y < 0)
        {
            vertVelocity = vertDirection * -2f;
            numDashes = 1;
        }
      
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        
        transform.rotation = Quaternion.Euler(0, angle, 0);

        Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        
        if (direction == Vector3.zero)
        {
            moveDir = Vector3.zero;
        }
        else
        {
            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        
        if(dashVelocity.magnitude > 0)
        {
            dashVelocity -= dashVelocity.normalized ;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded) { 
                vertVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity) * vertDirection;
            }
            else if (numDashes != 0)
            {
                dashVelocity = moveDir.normalized * dashSpeed;
                numDashes = 0;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
        }

        vertVelocity += gravity * Time.deltaTime * vertDirection;

        controller.Move(dashVelocity * Time.deltaTime);
        controller.Move(vertVelocity * Time.deltaTime);

        if (isRunning)
        {
            controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
        }
        if (!isRunning)
        {
            controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
        }               
    }

}
