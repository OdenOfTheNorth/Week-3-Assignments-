using System;
using System.Collections;
using System.Collections.Generic;
using FG;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerControllerTest : MonoBehaviour
{
    
    [NonSerialized] public float forwardInput;
    [NonSerialized] public float sidewaysInput;
    [NonSerialized] public bool jumpInput;
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private float maxDistance = 2f;

    
    private Rigidbody rigidbody;
    private Transform cam;
    private CapsuleCollider _capsuleCollider;
    
    private Vector3 moveDirection;
  
    private float speed = 1000f;
    private float turnSpeed = 10f;



    void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Vector3 rightFromCamera = new Vector3(cam.right.x, transform.right.y, cam.right.z);
        Vector3 forwardFromCamera = new Vector3(cam.forward.x, transform.forward.y, cam.forward.z);
        
        moveDirection = (sidewaysInput * speed * rightFromCamera) + (forwardInput * speed * forwardFromCamera);

        if (rigidbody.rotation.y != 0)
        {
            transform.rotation = transform.rotation; 
        }
        
        
        if (forwardInput != 0 || sidewaysInput != 0)
        {   
            rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), turnSpeed * Time.deltaTime);
            rigidbody.velocity = new Vector3(moveDirection.x, rigidbody.velocity.y,moveDirection.z);
        }
        else
        {
            rigidbody.velocity = new Vector3(moveDirection.x, rigidbody.velocity.y,moveDirection.z);
        }

        if (CheackGrounded())
        {
            if (jumpInput)
            {
                rigidbody.velocity += new Vector3(0f,_characterData.jumpForce,0f); 
            }
        }
        else
        {
            rigidbody.velocity = new Vector3(moveDirection.x, rigidbody.velocity.y,moveDirection.z);
        }
    }
    
    private bool CheackGrounded()
    {
        //TODO Make better
        Debug.DrawRay(transform.position + _capsuleCollider.center, Vector3.down * maxDistance, Color.red);
        return Physics.Raycast(transform.position + _capsuleCollider.center, Vector3.down, maxDistance);
    }


}
