﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform focus = default;

    [SerializeField, Range(1f, 20f)]
    float maxDistance = 5f;
    
    [SerializeField, Min(0f)]
    float focusRadius = 1f;
    
    [SerializeField, Range(0f, 1f)]
    float focusCentering = 0.5f;
    
    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;
    
    [SerializeField, Range(-89f, 89f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 60f;
    
    [SerializeField, Min(0f)]
    float alignDelay = 5f;
    
    [SerializeField, Range(0f, 90f)]
    float alignSmoothRange = 45f;
    
    [SerializeField]
    Transform playerInputSpace = default;
    
    [SerializeField]
    LayerMask obstructionMask = -1;
    
    Camera regularCamera;
    
    
    
    public LayerMask enviormentLayerMask;
    
    Vector2 orbitAngles = new Vector2(45f, 0f);
    
    void Awake () {
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
        regularCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    Vector3 focusPoint, previousFocusPoint;

    void LateUpdate () {
        ChangeZoom();
        UpdateFocusPoint();
        Quaternion lookRotation;
        if (ManualRotation() || AutomaticRotation()) {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else {
            lookRotation = transform.localRotation;
        }
        lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * maxDistance;

        if (Physics.Raycast(focusPoint, -lookDirection, out RaycastHit hit, maxDistance, obstructionMask)) 
        {
            lookPosition = focusPoint - lookDirection * hit.distance;
        }
        
        transform.SetPositionAndRotation(lookPosition, lookRotation);

        //transform.localPosition = focusPoint - lookDirection * (hit.distance + regularCamera.nearClipPlane);
    }

    void ChangeZoom()
    {
        float ZoomIn = Input.GetAxis("Mouse ScrollWheel");
        maxDistance -= ZoomIn * 2f;
        if (maxDistance < 0f)
        {
            maxDistance = 0f;
        }else if (maxDistance > 20f)
        {
            maxDistance = 20f;
        }
        

    }
    
    void UpdateFocusPoint () {
        previousFocusPoint = focusPoint;

        Vector3 targetPoint = focus.position;
        
        float distance = Vector3.Distance(targetPoint, focusPoint);
        float t = 1f;
        
        if (distance > 0.01f && focusCentering > 0f) {
            t = Mathf.Pow(1f - focusCentering, Time.deltaTime);
        }
        if (distance > focusRadius) {
            t = Mathf.Min(t, focusRadius / distance);
        }
        focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        
        t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
    }
    
    float lastManualRotationTime;
    
    bool ManualRotation () {
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X")
        );
        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
            orbitAngles.x += rotationSpeed * Time.unscaledDeltaTime * -input.x;
            orbitAngles.y += rotationSpeed * Time.unscaledDeltaTime * input.y;
            lastManualRotationTime = Time.unscaledTime;
            return true;
        }
        return false;
    }
    
    
    void OnValidate () {
        if (maxVerticalAngle < minVerticalAngle) {
            maxVerticalAngle = minVerticalAngle;
        }
    }
    
    void ConstrainAngles () {
        orbitAngles.x =
            Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitAngles.y < 0f) {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y >= 360f) {
            orbitAngles.y -= 360f;
        }
    }
    
    bool AutomaticRotation () {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay) {
            return false;
        }
		
        Vector2 movement = new Vector2(
            focusPoint.x - previousFocusPoint.x,
            focusPoint.z - previousFocusPoint.z
        );
        float movementDeltaSqr = movement.sqrMagnitude;
        if (movementDeltaSqr < 0.000001f) {
            return false;
        }
        
        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
        float rotationChange = rotationSpeed *  Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);
        if (deltaAbs < alignSmoothRange) {
            rotationChange *= deltaAbs / alignSmoothRange;
        }		
        else if (180f - deltaAbs < alignSmoothRange) {
            rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        }
        
        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y,headingAngle, rotationChange);
        return true;
    }
    
    static float GetAngle (Vector2 direction) {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle : angle;
    }
    
    Vector3 CameraHalfExtends {
        get {
            Vector3 halfExtends;
            halfExtends.y =
                regularCamera.nearClipPlane *
                Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
            halfExtends.x = halfExtends.y * regularCamera.aspect;
            halfExtends.z = 0f;
            return halfExtends;
        }
    }
}
