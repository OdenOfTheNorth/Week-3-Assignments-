using UnityEngine;
using System;

namespace FG
{
    [RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
    public class CharacterMovement : MonoBehaviour
    {
        [NonSerialized] public float forwardInput;
        [NonSerialized] public float sidewaysInput;
        [NonSerialized] public float turnInput;
        [NonSerialized] public bool runInput;
        [NonSerialized] public bool jumpInput;
        [NonSerialized] public bool crouchInput;

        [SerializeField] private float maxDistance = 1.2f;

        [SerializeField] private CharacterData _characterData;

        private Transform _transform;
        private Rigidbody _body;
        private CapsuleCollider _capsuleCollider;
        
        private Vector2 _originalCapsuleSize;
        private Vector3 _moveDirection;
        
        private float _currentSpeed;
        private float _adjustVerticalVelocity;
        private float _jumpAmount;
        private float _inputAmount;


        public bool IsCrouching { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _body = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _originalCapsuleSize.Set(_capsuleCollider.radius,_capsuleCollider.height);
        }

        private void LateUpdate()
        {
            //Rotate
            _body.MoveRotation(_body.rotation * Quaternion.Euler(Vector3.up  * turnInput));
            
            
            //Move
            _moveDirection = (sidewaysInput * _transform.right + forwardInput * _transform.forward).normalized;
            _inputAmount = Mathf.Clamp01(Mathf.Abs(forwardInput) + Mathf.Abs(sidewaysInput));

            _adjustVerticalVelocity = _body.velocity.y;

            if (CheackGrounded())
            {
                if (jumpInput)
                {
                    _adjustVerticalVelocity = _characterData.jumpForce;
                }
                else if (crouchInput || runInput)
                {
                    if (IsCrouching || runInput)
                    {
                        ExitCrouch();
                    }
                    else
                    {
                        if (!runInput)
                        {
                            EnterCrouch();
                        }
                    }
                }

                if (!IsCrouching)
                {
                    _currentSpeed = _characterData.crouchSpeed;
                }
                else
                {
                    _currentSpeed = runInput ? _characterData.runSpeed : _characterData.walkSpeed;
                }
                
            }
            else
            {
                _currentSpeed *= _characterData.inAirMovementMultiplier;
            }

            SetVelocity();

        }

        private bool CheackGrounded()
        {
            //TODO Make better
            Debug.DrawRay(_transform.position + _capsuleCollider.center, Vector3.down * maxDistance, Color.red);
            return Physics.Raycast(_transform.position + _capsuleCollider.center, Vector3.down, maxDistance);
        }

        private void ExitCrouch()
        {
            IsCrouching = false;

            _capsuleCollider.radius = _originalCapsuleSize.x;
            _capsuleCollider.height = _originalCapsuleSize.y;
        }

        private void EnterCrouch()
        {
            IsCrouching = true;
            
            _capsuleCollider.height = _characterData.crouchHight;
            _capsuleCollider.radius = _characterData.crouchRadius;
        }
        
        private void SetVelocity()
        {
            Vector3 velocity = (_moveDirection * (_currentSpeed * _inputAmount));
            velocity.y = _adjustVerticalVelocity * _characterData.gravityMultiplier;
            _body.velocity = velocity;
        }
    }
}