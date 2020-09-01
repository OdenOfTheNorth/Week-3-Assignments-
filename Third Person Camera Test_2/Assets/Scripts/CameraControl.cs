using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Sensetivity = 100f;
    public Transform PlayerTransform;
    
    Vector2 minMax = new Vector2(-90f, 90f);
    
    private float distance = -5f;
    private float positionX = 0f;
    private float positionY = 0f;

    private void Update()
    {
        positionX -= Input.GetAxis("Mouse X") * Sensetivity * Time.deltaTime;
        positionY -= Input.GetAxis("Mouse Y") * Sensetivity * Time.deltaTime;

        positionY = Mathf.Clamp(positionY, minMax.x, minMax.y);
        
        gameObject.transform.position = PlayerTransform.position + Quaternion.Euler(positionY, -positionX, 0) * new Vector3(0, 0, distance);
        gameObject.transform.LookAt(PlayerTransform);
    }
    /*
    private void OnCollisionEnter(float DistanceFromPlayer)
    {
        DistanceFromPlayer = Vector3.Distance(PlayerTransform.transform.position,other.transform.position );
        
        return DistanceFromPlayer;
    }*/
}
