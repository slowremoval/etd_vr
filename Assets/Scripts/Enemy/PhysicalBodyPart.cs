using System;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class PhysicalBodyPart : MonoBehaviour
{
    
    [SerializeField] private Transform _target;
    private ConfigurableJoint _joint;
    private Quaternion _startRotation;
    private bool _isSent;
    
    public event Action OnJointDestroyed;
    
    private void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
        _startRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        if (_joint != null)
        {
            _joint.targetRotation = Quaternion.Inverse(_target.localRotation) * _startRotation;
        }
        else if (!_isSent)
        {
            OnJointDestroyed?.Invoke();
            _isSent = true;
        }
    }
}
