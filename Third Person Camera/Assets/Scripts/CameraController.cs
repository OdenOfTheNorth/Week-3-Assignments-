using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public Transform CenterPoint;
    public Camera myCamera;
    public Transform Player;
    public float Sensitivity = 100.0f;
    public float FOV = 15f;
  
    
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float savedFOV = 0f;
    private float SensitivityIDS = 0;


    private void Awake()
    {
        savedFOV = myCamera.fieldOfView;
        SensitivityIDS = Sensitivity / 2;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        yRotation -= mouseY;
        //xRotation -= mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        
        
        ///CenterPoint.transform.rotation = Quaternion.Euler(xRotation,-yRotation,0f);
        
        if (Input.GetButton("Fire2"))
        {
            myCamera.fieldOfView = FOV;
            CenterPoint.transform.rotation = Quaternion.Euler(0f,-xRotation,0f);
            Sensitivity = SensitivityIDS;
        }
        else
        {
            myCamera.fieldOfView = savedFOV;
        }

    }
}
