using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject;

public class HandControllerInput : MonoBehaviour
{
//todo: rework this script (or this all system)!

    private const string _activatePullingName = "ActivatePulling";
    private bool IsPullingPossible;
    private ActionBasedController _controller;
    //public static event Action<HandControllerInput> OnWeaponPulling;
    private ItemPuller _itemPuller;
    public Inventory _playerInventory;

    public InputActionProperty VelocityProperty;
    public InputActionProperty JoystickProperty;
    public float HandVelocityMultiplier = 1;

    public Vector3 Velocity { get; private set; } = Vector3.zero;
    public Vector2 JoystickValue { get; private set; } = Vector2.zero;
    
    // [Inject]
    // private void Construct(Inventory playerInventory)
    // {
    //     _playerInventory = playerInventory;
    // }
    
    private void OnEnable()
    {
        _controller.selectAction.action.performed += StartPulling;
        VelocityProperty.action.performed += SetHandVelocity;
        JoystickProperty.action.performed += SetJoystickValue;
    }

    private void OnDisable()
    {
        _controller.selectAction.action.performed -= StartPulling;
        VelocityProperty.action.performed -= SetHandVelocity;
        JoystickProperty.action.performed -= SetJoystickValue;
    }

    private void Awake()
    {
        _controller = GetComponent<ActionBasedController>();

        _itemPuller = new ItemPuller();
    }

    private void SetJoystickValue(InputAction.CallbackContext context)
    {
        JoystickValue = context.ReadValue<Vector2>();
    }

    private void SetHandVelocity(InputAction.CallbackContext context)
    {
        Velocity = context.ReadValue<Vector3>() * HandVelocityMultiplier;
    }

    private void StartPulling(InputAction.CallbackContext context)
    {
        if (IsPullingPossible)
        {
            //HandControllerInput handControllerInput = GetComponent<HandControllerInput>();
            //OnWeaponPulling?.Invoke(handControllerInput);
            Debug.Log($"Hand activated item pull!Item is :{_playerInventory.Item.name}");
            if (_playerInventory.Item != null)
            {
               _itemPuller.StartWeaponPulling(_playerInventory.Item, this);
               Debug.Log("Hand activated item pull!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_activatePullingName))
        {
            IsPullingPossible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_activatePullingName))
        {
            IsPullingPossible = false;
        }
    }
}