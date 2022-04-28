using System;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class VolumeRegulator : MonoBehaviour
{
    private Transform _playerTransform;
    private Rigidbody _playerRb;

    private Volume _globalVolume;
    
    [Inject]
    private void Construct(Inventory playerInventory)
    {
        _playerTransform = playerInventory.transform;
    }

    private void Awake()
    {
        _playerRb = _playerTransform.gameObject.GetComponent<Rigidbody>();
        _globalVolume = GetComponent<Volume>();
        _globalVolume.enabled = true;
    }

    private void Update()
    {
        float playerCurrentVelocity = Vector3.Magnitude(_playerRb.velocity);
        _globalVolume.weight = Mathf.Lerp(_globalVolume.weight ,playerCurrentVelocity / 7, Time.deltaTime * 50);
        //_globalVolume.profile.components.
    }
}
    
