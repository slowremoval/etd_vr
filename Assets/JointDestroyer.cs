using System;
using UnityEngine;

public class JointDestroyer : MonoBehaviour
{
    public ConfigurableJoint Joint;

    private float _startValue;

    private void Start()
    {
        _startValue = Joint.breakForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Joint.breakForce /= 2.5f;
    }

    private void OnCollisionExit(Collision other)
    {
        Joint.breakForce /= _startValue;
    }
}