using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Sensetivity = 100f;
    public Transform PlayerTransform;
    
    
    private SphereCollider myCollider;
    Vector2 minMax = new Vector2(-10f, 10f);

    private float maxDistance = -5f;
    public float distance;
    private float shoulderDistance = 30f;
    private float positionX = 0f;
    private float positionY = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myCollider = GetComponent<SphereCollider>();
        distance = maxDistance;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            positionX += Input.GetAxis("Mouse X");//* Sensetivity * Time.deltaTime;
            positionY -= Input.GetAxis("Mouse Y");// * Sensetivity * Time.deltaTime;
        }

        positionY = Mathf.Clamp(positionY, minMax.x, minMax.y);
    }

    private void LateUpdate()
    {
        Vector3 playerTransformPositiontransform = PlayerTransform.position;
        gameObject.transform.position = playerTransformPositiontransform + Quaternion.Euler(positionY, positionX, 0) * new Vector3(0f, 0f, distance);
        gameObject.transform.LookAt(playerTransformPositiontransform);
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
