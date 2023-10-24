using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private float cameraLerp; //12f

    private float rotationX;
    private float rotationY;

    private void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");

        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        transform.position = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);
    }
}
