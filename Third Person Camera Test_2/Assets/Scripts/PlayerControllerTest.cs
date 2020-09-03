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
    [NonSerialized] public float turnInput;
    [NonSerialized] public bool runInput;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool crouchInput;
    
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private float maxDistance = 2f;

    
    private Rigidbody rigidbody;
    private Transform cam;
    private Transform _transform;
    private CapsuleCollider _capsuleCollider;
    
    private Vector3 moveDirection;
    private Vector2 _originalCapsuleSize;

    private float _currentSpeed;
    private float _adjustVerticalVelocity;
    private float _inputAmount;
    
    private float speed = 1000f;
    private float turnSpeed = 10f;
    private float turnSpeedVelocity;
    private Vector3 gravity = Physics.gravity;
    
    public bool IsCrouching { get; private set; }
    public bool IsRuning { get; private set; }


    void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        gravity = Physics.gravity;
        _originalCapsuleSize.Set(_capsuleCollider.radius,_capsuleCollider.height);
    }

    void FixedUpdate()
    {
        _inputAmount = Mathf.Clamp01(Mathf.Abs(forwardInput * speed) + Mathf.Abs(sidewaysInput * speed));
        //_currentSpeed = rigidbody.velocity.magnitude;
        
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
