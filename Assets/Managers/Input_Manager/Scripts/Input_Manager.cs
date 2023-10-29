using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private Vector2 rightAxisValue = Vector2.zero;  //Mouse [CAMERA]
    private Vector2 leftAxisValue = Vector2.zero;   //Keyboard [WASD]

    private float timeSinceJumpPressed = 0f;
    private float timeSinceCrouchPressed = 0f;
    
    
    private void Awake()
    {
        // Compruebo existencia de instancias al input manager

        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this) // Si existe un input manager y no soy yo
        {
            Destroy(this.gameObject); // Destruyelo ùwú
        }
        else
        {
            // Genero instancia y activo character sheme
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();

            //Delegates
            playerInputs.Character.Camera.performed += RightAxisUpdate;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Jump.performed += JumpButtonUpdate;
            playerInputs.Character.Crouch.performed += CrouchButtonUpdate;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        timeSinceCrouchPressed += Time.deltaTime;

        InputSystem.Update();
    }



    /// ------------------------------ FUNCTIONS

    //CAMERA
    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        rightAxisValue = context.ReadValue<Vector2>();
    }

    //MOVEMENT
    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }

    //JUMP
    private void JumpButtonUpdate(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }

    //CROUCH
    private void CrouchButtonUpdate(InputAction.CallbackContext context)
    {
        timeSinceCrouchPressed = 0f;
    }



    /// ------------------------------ GETTERS

    //CAMERA
    public Vector2 GetRightAxisValue() { return rightAxisValue; }

    //MOVEMENT
    public Vector2 GetLeftAxisValue() { return leftAxisValue; }

    //JUMP
    public bool GetJumpButtonPressed() { return timeSinceJumpPressed == 0; }

    //CROUCH
    public bool GetCrouchButtonPressed() { return timeSinceCrouchPressed == 0; }
}
