using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [NonSerialized] public float mouseX;
    [NonSerialized] public float mouseY;
    [NonSerialized] public bool ADS;
    
    public float Sensetivity = 100f;
    public Transform PlayerTransform;
    public float FOV = 15f;
    public float distance;
    
    
    Vector2 minMax = new Vector2(-10f, 10f);
    private SphereCollider myCollider;
    public Camera myCamera;
    
    private float maxDistance = -5f;
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
        myCollider = GetComponent<SphereCollider>();
        distance = maxDistance;
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
        Vector3 playerTransformPosition = PlayerTransform.position;
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
        }
    }

    private void OnCollisionEnter(Collision CollisionCheak)
    {
        if (myCollider.gameObject.CompareTag("Other"))
        {
            distance--;
            Debug.Log("enter collision");
        }
        else if (distance == distance)
        {
            distance++;
        }
    }
}
