using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Input_Manager inputManager;

    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;
    }

    private void Update()
    {
        //if (inputManager.GetNorthButtonPressed())
        //{
        //    Debug.Log("Pressed-GetNorthButtonPressed");
        //}
        if (inputManager.GetSouthButtonPressed())
        {
            Debug.Log("Pressed-South");
        }
        //if (inputManager.GetEastButtonPressed())
        //{
        //    Debug.Log("Pressed-GetEastButtonPressed");
        //}
        //if (inputManager.GetWestButtonPressed())
        //{
        //    Debug.Log("Pressed-GetWestButtonPressed");
        //}
    }
}
