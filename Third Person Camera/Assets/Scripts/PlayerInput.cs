using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Movement _movemant;
    private CameraController _mousePitch;
    private Camera cam;

    private void Awake()
    {
        _movemant = GetComponent<Movement>();
        cam = Camera.main;
    }

    private void Update()
    {
        
        _movemant.turnInput = Input.GetAxis("Mouse X") * GameplaySettings.mouseSensitivity.x;
        //_mousePitch.input = Input.GetAxis("Mouse Y") * GameplaySettings.mouseSensitivity.y;
        
        _movemant.forwardInput = Input.GetAxis("Vertical");
        _movemant.sidewaysInput = Input.GetAxis("Horizontal");
        _movemant.runInput = Input.GetKey(KeyCode.LeftShift);
        _movemant.jumpInput = Input.GetButtonDown("Jump");
        _movemant.crouchInput = Input.GetButtonDown("Crouch");
    }

    private void Interact()
    {
        //TODO Interact with stuff
    }
}
