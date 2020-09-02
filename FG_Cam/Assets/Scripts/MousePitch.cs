using System;
using UnityEngine;

namespace FG
{
    public class MousePitch : MonoBehaviour
    {
        [NonSerialized] public float input;
        private Vector2 _pitchLimit = new Vector2(-90f, 90f);
        
        private Transform _cameraTransform;
        private Quaternion _cameraRotation;
 
        private void Awake()
        {
            _cameraTransform = GameManager.PlayerCameraTransform;
            _cameraRotation = _cameraTransform.localRotation;
        }
 
        private void LateUpdate()
        {
            _cameraRotation *= Quaternion.Euler(-input, 0f, 0f);
 
            _cameraRotation = ClampRotationAround(_cameraRotation);
            
            _cameraTransform.localRotation = _cameraRotation;
        }
 
        private Quaternion ClampRotationAround(Quaternion rotation)
        {
            rotation.x /= rotation.w;
            rotation.y /= rotation.w;
            rotation.z /= rotation.w;
            rotation.w = 1.0f;
 
            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.x);
 
            angleX = Mathf.Clamp(angleX, _pitchLimit.x, _pitchLimit.y);
 
            rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
 
            return rotation;
        }
    }
}