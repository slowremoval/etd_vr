using System;
using UnityEngine;

public class IntegrityManager
{
    //private ConfigurableJoint _configurableJoint;
    private GolemStates _golemStates;
    private Golem _golem;
    
    private PhysicalBodyPart _leftArmJoint;
    private PhysicalBodyPart _rightArmJoint;
    private PhysicalBodyPart _leftLegJoint;
    private PhysicalBodyPart _rightLegJoint;

    public bool _rightArmDestroyed;
    public bool _leftArmDestroyed;
    public bool _rightLegDestroyed;
    public bool _leftLegDestroyed;

    public event Action OnSomethingBroken;
    
    public IntegrityManager(Golem golem, GolemStates golemStates,
        PhysicalBodyPart leftArmJoint, PhysicalBodyPart rightArmJoint,
        PhysicalBodyPart leftLegJoint, PhysicalBodyPart rightLegJoint)
    {
        //_configurableJoint = configurableJoint;
        _golemStates = golemStates;
        _golem = golem;
        
        _leftArmJoint = leftArmJoint;
        _rightArmJoint = rightArmJoint;
        _leftLegJoint = leftLegJoint;
        _rightLegJoint = rightLegJoint;

        Initialize();
    }

    private void Initialize()
    {
        _leftLegJoint.OnJointDestroyed += SendLeftLegDestroyed;
        _rightLegJoint.OnJointDestroyed += SendRightLegDestroyed;
        _leftArmJoint.OnJointDestroyed += SendLeftArmDestroyed;
        _rightArmJoint.OnJointDestroyed += SendRightArmDestroyed;
    }

    public void SendLeftLegDestroyed()
    {
        Debug.Log("Left leg is destroyed!");
        _leftLegDestroyed = true;
        
        OnSomethingBroken?.Invoke();
        //_golemStates.SetBehaviorIdleWithoutLeg();
        // if (_rightLegDestroyed)
        // {
        //     todo: logic
        // }
        //
        // Quaternion targetRotation = _configurableJoint.targetRotation;
        // Vector3 rotation = new Vector3(295, targetRotation.y, targetRotation.z);
        // _configurableJoint.targetRotation = Quaternion.Euler(rotation);
        //
        //_golemStates.SetBehaviorWalkingWithoutLeg(_golem.CurrentTarget);
    }

    public void SendRightLegDestroyed()
    {
        _rightLegDestroyed = true;
        OnSomethingBroken?.Invoke();
        //_golemStates.SetBehaviorIdleWithoutLeg();
        //todo
    }

    public void SendLeftArmDestroyed()
    {
        OnSomethingBroken?.Invoke();
        //_leftArmDestroyed = true;
        //todo
    }

    public void SendRightArmDestroyed()
    {
        OnSomethingBroken?.Invoke();
        //_rightArmDestroyed = true;
        //todo
    }
}