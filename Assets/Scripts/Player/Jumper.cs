using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Jumper : MonoBehaviour
{
    [SerializeField] private InputActionReference _jumpStick;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private ActionBasedContinuousMoveProvider _continuousMoveProvider;
    private bool _isGrounded;
    private float _jumpForce = 499;
    
    public LayerMask GroundLayer;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        _playerCollider = GetComponentInParent<CapsuleCollider>();
        _continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        
        _jumpStick.action.started += Jump;
    }

    private void Update()
    {
        CheckGround();

        _continuousMoveProvider.enabled = _isGrounded;
    }

    private void CheckGround()
    {
        Vector3 rayOrigin = transform.TransformPoint(_playerCollider.center);
        float rayLength = _playerCollider.height / 2 + 0.03f;
        Ray ray = new Ray(rayOrigin, Vector3.down);
        
        if (Physics.SphereCast(ray, _playerCollider.radius, rayLength, GroundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        _continuousMoveProvider.enabled = _isGrounded;
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Acceleration);
        }
    }
}