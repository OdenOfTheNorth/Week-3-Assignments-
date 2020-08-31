using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform cam;
    private float speed = 10f;
    private float turnSpeed = 100f;

    private Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        Vector3 dir = Vector3.zero;

        dir.y = 0f;

    }
}
