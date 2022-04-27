using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    private Dictionary<Type, IGolemBehavior> _behaviorsMap;
    private IGolemBehavior _behaviorCurrent;
    private Animator _animator;
    private DetectionManager _detectionManager;
    private IntegrityManager _integrityManager;
    private GolemStates _golemStates;
    private UnitFollower _unitFollower;
    private bool _isDied;
    [SerializeField] private Transform _pelvisTransform;

    [Header("Joints")] [SerializeField] private ConfigurableJoint _configurableJoint;
    [SerializeField] private PhysicalBodyPart _leftArmJoint;
    [SerializeField] private PhysicalBodyPart _rightArmJoint;
    [SerializeField] private PhysicalBodyPart _leftLegJoint;
    [SerializeField] private PhysicalBodyPart _rightLegJoint;

    [Header("Detection Properties")] public LayerMask DetectionLayer;
    public LayerMask DetectionObstacles;
    public float DetectionRadius;
    public float MinimumDirectionAngle;
    public float MaximumDirectionAngle;
    public Transform CurrentTarget;

    //public Transform EscapePoint;

    private void Awake()
    {
        _detectionManager = new DetectionManager(this);
        _animator = GetComponentInChildren<Animator>();
        _golemStates = new GolemStates(_animator, _pelvisTransform, _configurableJoint,
            _detectionManager);
        _integrityManager = new IntegrityManager(
            this, _golemStates, _leftArmJoint,
            _rightArmJoint, _leftLegJoint, _rightLegJoint);

        _detectionManager.OnTargetDetected += SetCurrentTarget;
        _integrityManager.OnSomethingBroken += ResetCurrentTarget;
    }

    private void ResetCurrentTarget()
    {
        CurrentTarget = null;

        if (_integrityManager._rightLegDestroyed &&
            _integrityManager._leftLegDestroyed &&
            _integrityManager._leftArmDestroyed &&
            _integrityManager._rightArmDestroyed)
        {
            //_isDied = true;
            Destroy(_configurableJoint);
        }
        else if (_integrityManager._rightLegDestroyed ||
                 _integrityManager._leftLegDestroyed)
        {
            _golemStates.SetBehaviorIdleWithoutLeg();
            Debug.Log("leg is broken");
        }
        else
        {
            _golemStates.SetGolemBehaviorIdle();
        }

        Debug.Log("Current target is cleared!");
    }

    private void SetCurrentTarget(Transform currentTarget)
    {
        CurrentTarget = currentTarget;

        if (_integrityManager._rightLegDestroyed || _integrityManager._leftLegDestroyed)
        {
            _golemStates.SetBehaviorWalkingWithoutLeg(CurrentTarget);
            Debug.Log("leg is broken");
        }
        else
        {
            _golemStates.SetBehaviorWalking(CurrentTarget);
        }
    }

    private void Update()
    {
        _golemStates?._behaviorCurrent.Update();
    }
}