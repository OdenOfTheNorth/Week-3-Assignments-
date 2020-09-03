using System;
using UnityEngine;

namespace FG
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerControllerTest PlayerControllerTest;
        private CameraController cameraController;

        private void Awake()
        {
            PlayerControllerTest = GetComponent<PlayerControllerTest>();
            cameraController = GetComponent<CameraController>();
            
        }

        private void Update()
        {
            if (cameraController)
            {
                cameraController.mouseY = Input.GetAxis("Mouse Y") * cameraController.Sensetivity * Time.deltaTime;
                cameraController.mouseX = Input.GetAxis("Mouse X") * cameraController.Sensetivity * Time.deltaTime;
                cameraController.ADS = Input.GetButton("Fire2");
            }
            if (PlayerControllerTest)
            {
                PlayerControllerTest.forwardInput = Input.GetAxis("Vertical") * Time.deltaTime;
                PlayerControllerTest.sidewaysInput = Input.GetAxis("Horizontal") * Time.deltaTime;
                //PlayerControllerTest.runInput = Input.GetKey(KeyCode.LeftShift);
                PlayerControllerTest.runInput = Input.GetButtonDown("Run");
                PlayerControllerTest.jumpInput = Input.GetButtonDown("Jump");
                PlayerControllerTest.crouchInput = Input.GetButtonDown("Crouch");
            }

            Interact();
        }

        private void Interact()
        {
            //TODO Interact with stuff
        }
    }
}

