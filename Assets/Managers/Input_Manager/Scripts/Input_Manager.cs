using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed = 0f;
    private Vector2 leftAxisValue = Vector2.zero;

    private bool jumpButtonPressed = false;
    private bool crouchButtonPressed = false;

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
            playerInputs.Character.Move.performed += leftAxisUpdate;
            playerInputs.Character.Jump.performed += jumpButtonUpdate;
            playerInputs.Character.Crouch.performed += crouchButtonUpdate;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        jumpButtonPressed = false;
        crouchButtonPressed = false;

        InputSystem.Update();
    }


    //MOVEMENT
    private void leftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    //JUMP
    private void jumpButtonUpdate(InputAction.CallbackContext context)
    {
        jumpButtonPressed = true;
        timeSinceJumpPressed = 0f;
    }

    //CROUCH
    private void crouchButtonUpdate(InputAction.CallbackContext context)
    {
        crouchButtonPressed = true;
    }


    //MOVEMENT
    public Vector2 GetLeftAxisValue()
    {
        return leftAxisValue;
    }

    //JUMP
    public bool GetJumpButtonPressed()
    {
        return jumpButtonPressed;
    }

    public float GetJumpButtonPressedTime()
    {
        return timeSinceJumpPressed;
    }

    //CROUCH
    public bool GetCrouchButtonPressed()
    {
        return crouchButtonPressed;
    }
}
