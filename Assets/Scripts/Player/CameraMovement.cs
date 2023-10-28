using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Input_Manager inputManager;

    [SerializeField] private GameObject target;

    [SerializeField] private float targetDistance;

    [SerializeField] private float cameraLerp; //12f

    private float rotationX;
    private float rotationY;

    private void Awake()
    {
        inputManager = Input_Manager._INPUT_MANAGER;
    }

    private void LateUpdate()
    {
        
        // Camera movement (orbit)
        rotationX += inputManager.GetRightAxisValue().y;
        rotationY += inputManager.GetRightAxisValue().x;

        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);


        // Camera don't touch walls
        RaycastHit hit;

        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }


        transform.position = finalPosition;

    }
}
