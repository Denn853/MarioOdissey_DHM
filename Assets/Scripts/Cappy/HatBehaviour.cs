using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HatBehaviour : MonoBehaviour
{
    private Input_Manager inputManager;

    [SerializeField] private GameObject player;
    [SerializeField] private float distance; // 2.5f

    private float cappyYPosition = 3.55f;

    private bool throwCappy = false;
    private float throwTimer = 5f;
    private float stopCappy = 0f;

    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;
    }

    private void Update()
    {

        if (inputManager.GetCappyButtonPressed())
        {
            throwCappy = true;
        }
        
        if (throwCappy)
        {
            if (stopCappy <= 1)
            {
                transform.position = transform.position + transform.forward * distance * Time.deltaTime;
            }

            stopCappy += Time.deltaTime;

            throwTimer -= Time.deltaTime;

            if (throwTimer <= 0)
            {
                throwCappy = false;
                throwTimer = 10f;
                stopCappy = 0;

            }
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cappyYPosition, player.transform.position.z);
        }
    }
}
