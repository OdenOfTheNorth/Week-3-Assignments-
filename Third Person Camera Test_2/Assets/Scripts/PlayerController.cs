using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform cam;
    
    private float speed = 100f;
    private float turnSpeed = 100f;
    private float turnSpeedVelocity;
    private Vector3 gravity;
    

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        gravity = Physics.gravity;
    }

    void FixedUpdate()
    {
        
        Vector3 dir = (cam.transform.right * Input.GetAxis("Horizontal")) + (cam.transform.forward * Input.GetAxis("Vertical"));
        
        dir.y = 0;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {   
            rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            rigidbody.velocity = transform.forward * speed;
        }        
        else
        {
            rigidbody.velocity = transform.forward * 0;
            // rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        }

        //rigidbody.velocity.y += gravity;

        /*
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = x * speed * cam.transform.right + z * speed * cam.transform.forward;
        
        rigidbody.velocity = Time.deltaTime * move;*/
    }

  

}
