using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPresence : MonoBehaviour
{
    public GameObject HandModelPrefab;

    private ActionBasedController _controller;
    private GameObject _spawnedHandModel;
    private Animator _handAnimator;

    private int _animatorTriggerValueHash = Animator.StringToHash("Trigger");
    private int _animatorGripValueHash = Animator.StringToHash("Grip");
    private void Awake()
    {
        _controller = GetComponentInParent<ActionBasedController>();
        _controller.activateActionValue.action.performed += UpdateHandActivateAnimation;
        _controller.selectActionValue.action.performed += UpdateHandSelectAnimation;
    }
    private void UpdateHandSelectAnimation(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log($"UpdateHandSelectAnimation : {callbackContext.ReadValue<float>()}");
        _handAnimator.SetFloat(_animatorGripValueHash, callbackContext.ReadValue<float>());
    }
    
    private void UpdateHandActivateAnimation(InputAction.CallbackContext callbackContext)
    {
        _handAnimator.SetFloat(_animatorTriggerValueHash, callbackContext.ReadValue<float>());
    }

    private void Start()
    {
        _spawnedHandModel = Instantiate(HandModelPrefab, transform);
        _handAnimator = _spawnedHandModel.GetComponent<Animator>();
    }
    
}