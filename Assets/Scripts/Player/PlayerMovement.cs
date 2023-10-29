using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera camera;

    [SerializeField] private Animator animator;


    private CharacterController controller;
    private Input_Manager inputManager;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 followDirecction = Vector3.zero;


    [Header("Movement")]
    [SerializeField] private float maxVelocity; // max velocityXZ // 8f
    private float velocity = 0f; // initial velocityXZ
    [SerializeField] private float acceleration; //5f
    [SerializeField] private float decceleration; //7f
    

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1f;


    [Header("Jump")]
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float jumpForce;

    private int maxJumps = 2; // 3 jumps
    private float jumpIncrement = 5f;
    private int jumpCounter = 0;
    public float jumpTimer = 0f;

    private float jumpPlatformForce = 50f;


    [Header("Crouch")]
    [SerializeField] private bool canCrouch;



    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //Bloquea cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Movement();
        TripleJump();
        //Crouch();
        
        controller.Move(finalVelocity * Time.deltaTime);
    }


    private void Movement()
    {
        float horizontalInput = inputManager.GetLeftAxisValue().x;
        float verticalInput = inputManager.GetLeftAxisValue().y;

        // Calcular dirección XZ (movimiento)
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(horizontalInput, 0f, verticalInput);
        direction.Normalize();


        if (direction != Vector3.zero)
        {
            //rotation
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;


            // acceleration
            velocity += acceleration * Time.deltaTime;
            followDirecction = direction;

            //// Walk
            //animator.SetBool("isWalking", true);

        }
        else
        {
            // decceleration
            velocity -= decceleration * Time.deltaTime;
            direction.x = followDirecction.x;
            direction.z = followDirecction.z;

            //// Idle
            //animator.SetBool("isWalking", false);
            //animator.SetBool("isRunning", false);
        }

        velocity = Mathf.Clamp(velocity, 0f, maxVelocity);

        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocity; // XZ
        finalVelocity.z = direction.z * velocity; // XZ

        // Asignar dirección Y
        direction.y = -1f;


        /// -------------------- ANIMATIONS
        if (velocity == 0)
        {
            // Idle
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else if (velocity < maxVelocity / 2 && velocity != 0)
        {
            // Walk
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if (velocity > maxVelocity / 2)
        {
            // Run
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
    }
 
    private void TripleJump()
    {
        // Calcular gravedad 
        if (controller.isGrounded)
        {
            //finalVelocity.y = -1f * gravity * Time.deltaTime;

            if (inputManager.GetJumpButtonPressed())
            {
                finalVelocity.y = jumpForce + jumpIncrement * jumpCounter;
                jumpTimer = 0.5f;

                if (jumpCounter < maxJumps)
                {
                    jumpCounter++;
                }
                else
                {
                    jumpCounter = 0;
                }
            }

            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f)
            {
                jumpCounter = 0;
            }
        }
        else
        {
            finalVelocity.y += -1f * gravity * Time.deltaTime;
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && canCrouch == false)
        {
            //Debug.Log("crouching");
            canCrouch = true;
        }
        else
        {
            canCrouch = false;
        }

        if (canCrouch == true)
        {
            finalVelocity *= 0.5f;
            controller.height = 1f;
        }
        else
        {
            controller.height = 2f;
        }
    }


    public Vector3 superJump()
    {
        finalVelocity.y = jumpPlatformForce;
        return finalVelocity;
    }

}