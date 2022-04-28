using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PullChecker : MonoBehaviour
{
    private ActionBasedController _controller;
    private ItemPuller _itemPuller;
    
    [SerializeField] private Inventory _playerInventory;


    private const string _activatePullingName = "ActivatePulling";
    private bool IsPullingPossible;

    private void Awake()
    {
        _controller = GetComponent<ActionBasedController>();
        _itemPuller = new ItemPuller();
    }
    
    private void OnEnable()
    {
        _controller.selectAction.action.performed += StartPulling;
    }

    private void OnDisable()
    {
        _controller.selectAction.action.performed -= StartPulling;
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
    private void StartPulling(InputAction.CallbackContext context)
    {
        if (IsPullingPossible)
        {
            if (_playerInventory.Item != null)
            {
                _itemPuller.StartWeaponPulling(_playerInventory.Item, this);
            }
        }
    }
}