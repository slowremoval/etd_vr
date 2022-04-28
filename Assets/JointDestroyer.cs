using System;
using UnityEngine;

public class JointDestroyer : MonoBehaviour
{
    public ConfigurableJoint Joint;

    private float _startValue;
    private float _collisionExitValue;

    private float _exitValueDivider = 1.29f;
    private float _damageForce = 1.55f;

    private void Start()
    {
        _startValue = Joint.breakForce;
        _collisionExitValue = _startValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Joint.breakForce /= _damageForce;
    }

    private void OnCollisionExit(Collision other)
    {
        _collisionExitValue /= _exitValueDivider;
        Joint.breakForce = _collisionExitValue;
    }
}