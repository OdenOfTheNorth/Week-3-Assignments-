using System;
using System.Collections;
using System.Collections.Generic;
using FG;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Transform cam;
    private CharacterData _characterData;
    private Transform _transform;
    private CapsuleCollider _capsuleCollider;
    private Vector3 moveDirection;

    private float _currentSpeed;
    private float jumpForce = 5f;
    private float _adjustVerticalVelocity;
    private float _inputAmount;
    
    private float maxDistance = 1.2f;
    private float speed = 10f;
    private float turnSpeed = 100f;
    private float turnSpeedVelocity;
    private Vector3 gravity;

    public bool jumpInput  = Input.GetButtonDown("Jump");
    

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        gravity = Physics.gravity;
    }

    void FixedUpdate()
    {
        
        moveDirection = (cam.transform.right * Input.GetAxis("Horizontal")) + (cam.transform.forward * Input.GetAxis("Vertical"));
        
        moveDirection.y = 0;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {   
            rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), turnSpeed * Time.deltaTime);
            rigidbody.velocity = transform.forward * speed;
        }        
        else
        {
            //rigidbody.velocity = transform.forward * 0;
            rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime);
        }

        if (CheackGrounded())
        {
            if (jumpInput)
            {
                _adjustVerticalVelocity = jumpForce;
            }
            
            
        }
        

        //rigidbody.velocity.y += gravity;

        /*
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = x * speed * cam.transform.right + z * speed * cam.transform.forward;
        
        rigidbody.velocity = Time.deltaTime * move;*/
    }

    private bool CheackGrounded()
    {
        //TODO Make better
        Debug.DrawRay(_transform.position + _capsuleCollider.center, Vector3.down * maxDistance, Color.red);
        return Physics.Raycast(_transform.position + _capsuleCollider.center, Vector3.down, maxDistance);
    }
  
    private void SetVelocity()
    {
        Vector3 velocity = (moveDirection * (_currentSpeed * _inputAmount));
        velocity.y = _adjustVerticalVelocity * _characterData.gravityMultiplier;
        rigidbody.velocity = velocity;
    }

}
