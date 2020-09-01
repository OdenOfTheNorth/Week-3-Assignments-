using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FG
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static Camera PlayerCamera { get; private set; }
        public static Transform PlayerCameraTransform { get; private set; }

        public static bool LockCursor
        {
            set
            {
                if (value)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

        private void Awake()
        {
            PlayerCamera = Camera.current;
            PlayerCameraTransform = PlayerCamera.transform;

            Camera t = PlayerCamera;
        }

        private void OnEnable()
        {
            LockCursor = true;
        }

        private void OnDisable()
        {
            LockCursor = false;
        }
    }
}    
