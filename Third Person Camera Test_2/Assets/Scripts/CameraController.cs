using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [NonSerialized] public float mouseX;
    [NonSerialized] public float mouseY;
    [NonSerialized] public bool ADS;
    
    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;
    
    [SerializeField] Transform focus = default;
    
    public float Sensetivity = 100f;
    public Transform PlayerTransform;
    public float FOV = 15f;
    public float maxDistance = -5f;
    public Camera myCamera;
    public LayerMask enviormentLayerMask;
    
    Vector2 minMax = new Vector2(-10f, 10f);
    Vector2 orbitAngles = new Vector2(45f, 0f);
    Vector3 playerTransformPosition;
    Vector3 focusPoint;
    
    private float distance;
    private float shoulderDistance = 3f;
    private float positionX = 0f;
    private float positionY = 0f;
    private float savedFOV = 0f;
    private float SensitivityIDS = 0;
    
    private void Awake()
    {
        savedFOV = myCamera.fieldOfView;
        SensitivityIDS = Sensetivity / 2;
        Cursor.lockState = CursorLockMode.Locked;
        distance = maxDistance;
        focusPoint = focus.position;
    }

    private void Update()
    {
        if (mouseX != 0 || mouseY != 0)
        {
            positionX += mouseX;//* Sensetivity * Time.deltaTime;
            positionY -= mouseY;// * Sensetivity * Time.deltaTime;
        }

        positionY = Mathf.Clamp(positionY, minMax.x, minMax.y);
        

    }

    private void LateUpdate()
    {
        ManualRotation();
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;
        if (Physics.Raycast(
            focusPoint, -lookDirection, out RaycastHit hit, distance
        )) {
            lookPosition = focusPoint - lookDirection * hit.distance;
        }

        transform.SetPositionAndRotation(lookPosition, lookRotation);
  
        /*
        playerTransformPosition = PlayerTransform.position;
        if (ADS)
        {
            playerTransformPosition = new Vector3(shoulderDistance, playerTransformPosition.y, playerTransformPosition.z);
            myCamera.fieldOfView = FOV;
            Sensetivity = SensitivityIDS;
            gameObject.transform.position = playerTransformPosition + Quaternion.Euler(positionY,positionX, 0) * new Vector3( 0f, 0f, distance);
            gameObject.transform.LookAt(playerTransformPosition);
        }
        else
        {
            myCamera.fieldOfView = savedFOV;
            gameObject.transform.position = playerTransformPosition + Quaternion.Euler(positionY, positionX, 0) * new Vector3(0f, 0f, distance);
            gameObject.transform.LookAt(playerTransformPosition);
        }*/

        void ManualRotation () {
            Vector2 input = new Vector2(
                mouseX,
                mouseY
            );
            const float e = 0.001f;
            if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
                orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            }
        }

    }


}
