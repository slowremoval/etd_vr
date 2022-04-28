using UnityEngine;
using UnityEngine.InputSystem;

public class HandControllerInput : MonoBehaviour
{
//todo: rework this script (or all this system)!
    [SerializeField] private InputActionProperty VelocityProperty;
    [SerializeField] private InputActionProperty JoystickProperty;
    
    public float HandVelocityMultiplier = 1;
    public Vector3 Velocity { get; private set; } = Vector3.zero;
    public Vector2 JoystickValue { get; private set; } = Vector2.zero;

    private void OnEnable()
    {
        VelocityProperty.action.performed += SetHandVelocity;
        JoystickProperty.action.performed += SetJoystickValue;
    }

    private void OnDisable()
    {
        VelocityProperty.action.performed -= SetHandVelocity;
        JoystickProperty.action.performed -= SetJoystickValue;
    }
    

    private void SetJoystickValue(InputAction.CallbackContext context)
    {
        JoystickValue = context.ReadValue<Vector2>();
    }

    private void SetHandVelocity(InputAction.CallbackContext context)
    {
        Velocity = context.ReadValue<Vector3>() * HandVelocityMultiplier;
    }
}