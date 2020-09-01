using System;
using UnityEngine;

namespace FG
{
    public class PlayerInputFPS : MonoBehaviour
    {
        private CharacterMovement _movemant;
        private MousePitch _mousePitch;

        private void Awake()
        {
            _movemant = GetComponent<CharacterMovement>();
            _mousePitch = GetComponent<MousePitch>();
            
        }

        private void Update()
        {
            _mousePitch.input = Input.GetAxis("Mouse Y") * GameplaySettings.mouseSensitivity.y;
            
            _movemant.turnInput = Input.GetAxis("Mouse X") * GameplaySettings.mouseSensitivity.x;
            _movemant.forwardInput = Input.GetAxis("Vertical");
            _movemant.sidewaysInput = Input.GetAxis("Horizontal");
            _movemant.runInput = Input.GetKey(KeyCode.LeftShift);
            _movemant.jumpInput = Input.GetButtonDown("Jump");
            _movemant.crouchInput = Input.GetButtonDown("Crouch");

            Interact();
        }

        private void Interact()
        {
            //TODO Interact with stuff
        }
    }
}

