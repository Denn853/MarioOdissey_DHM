using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed = 0f;
    private float timeSinceJumpPresseds = 0f;
    private float timeSinceJumpPressedd = 0f;
    private float timeSinceJumpPressedf = 0f;
    private Vector2 leftAxisValue = Vector2.zero;

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
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += leftAxisUpdate;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;

        InputSystem.Update();
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }

    private void leftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }
    //public bool GetNorthButtonPressed()
    //{
    //    return this.timeSinceJumpPressed == 0f;
    //}

    public bool GetSouthButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }

    //public bool GetEastButtonPressed()
    //{
    //    return this.timeSinceJumpPressed == 0f;
    //}

    //public bool GetWestButtonPressed()
    //{
    //    return this.timeSinceJumpPressed == 0f;
    //}
}
