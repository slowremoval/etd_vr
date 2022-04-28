using System;
using System.Collections.Generic;
using UnityEngine;

public class GolemStates
{
    private Dictionary<Type, IGolemBehavior> _behaviorsMap;
    public IGolemBehavior _behaviorCurrent;
    private Animator _animator;
    private DetectionManager _detectionManager;

    //[SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _pelvisTransform;
    [SerializeField] private ConfigurableJoint _configurableJoint;

    public event Action OnStateChanged;
    
    public GolemStates(Animator animator, Transform golemPelvisTransform, ConfigurableJoint _golemJoint,
        DetectionManager detectionManager)
    {
        _animator = animator;
        _pelvisTransform = golemPelvisTransform;
        _configurableJoint = _golemJoint;
        _detectionManager = detectionManager;

        InitializeBehaviors();
        SetBehaviorByDefault();
    }

    private void InitializeBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IGolemBehavior>();

        _behaviorsMap[typeof(GolemBehaviorIdle)] = new GolemBehaviorIdle(_animator, _detectionManager);
        _behaviorsMap[typeof(GolemBehaviorIdleWithoutLeg)] = new GolemBehaviorIdleWithoutLeg(_animator, _detectionManager);
        _behaviorsMap[typeof(GolemBehaviorFollowing)] = new GolemBehaviorFollowing(
            _animator, _pelvisTransform, _configurableJoint, this);
        _behaviorsMap[typeof(GolemBehaviorAggressive)] = new GolemBehaviorAggressive(_animator, this, _pelvisTransform, _configurableJoint);
        _behaviorsMap[typeof(GolemBehaviorWithoutLeg)] = new GolemBehaviorWithoutLeg(
            _animator, _pelvisTransform, _configurableJoint, this);
        _behaviorsMap[typeof(GolemBehaviorWithoutLegAggressive)] = new GolemBehaviorWithoutLegAggressive(_animator, this);
    }

    private void SetBehavior(IGolemBehavior newBehavior)
    {
        _behaviorCurrent?.Exit();

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }

    private void SetBehavior(IGolemBehavior newBehavior, Transform target)
    {
        _behaviorCurrent?.Exit();

        if (newBehavior is IGolemBehaviorTarget behavior)
        {
            _behaviorCurrent = behavior;
            behavior.Enter(target);
        }
        else
        {
            throw new Exception("New state isn`t a IGolemBehavior!");
        }
    }

    private void SetBehaviorByDefault()
    {
        IGolemBehavior behaviorByDefault = GetBehavior<GolemBehaviorIdle>();
        SetBehavior(behaviorByDefault);
    }

    private IGolemBehavior GetBehavior<T>() where T : IGolemBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }

    public void SetGolemBehaviorIdle()
    {
        IGolemBehavior behavior = GetBehavior<GolemBehaviorIdle>();
        SetBehavior(behavior);
    }

    public void SetGolemBehaviorAggressive(Transform target)
    {
        IGolemBehavior behavior = GetBehavior<GolemBehaviorAggressive>();
        SetBehavior(behavior, target);
    }

    public void SetBehaviorWalking(Transform target)
    {
        IGolemBehavior behavior = GetBehavior<GolemBehaviorFollowing>();
        SetBehavior(behavior, target);
    }

    public void SetBehaviorWalkingWithoutLeg(Transform target)
    {
        IGolemBehavior behavior = GetBehavior<GolemBehaviorWithoutLeg>();
        SetBehavior(behavior, target);
    }
    public void SetBehaviorIdleWithoutLeg()
    {
        IGolemBehavior behavior = GetBehavior<GolemBehaviorIdleWithoutLeg>();
        SetBehavior(behavior);
    }

    public void SetGolemBehaviorWithoutLegAggressive()
    {
        IGolemBehavior behavior = GetBehavior<GolemBehaviorWithoutLegAggressive>();
        SetBehavior(behavior);
    }
}

// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class GolemStates
// {
//     private Dictionary<Type, IGolemBehavior> _behaviorsMap;
//     private IGolemBehavior _behaviorCurrent;
//     private IGolemBehaviorTarget _behaviorWithCurrent;
//     private Animator _animator;
//     private DetectionManager _detectionManager;
//
//     //[SerializeField] private Transform _targetTransform;
//     [SerializeField] private Transform _pelvisTransform;
//     [SerializeField] private ConfigurableJoint _configurableJoint;
//
//     public GolemStates(Animator animator, Transform golemPelvisTransform, ConfigurableJoint _golemJoint,
//         DetectionManager detectionManager)
//     {
//         _animator = animator;
//         _pelvisTransform = golemPelvisTransform;
//         _configurableJoint = _golemJoint;
//         _detectionManager = detectionManager;
//
//         InitializeBehaviors();
//         _detectionManager.OnTargetDetected += SetBehaviorWalking;
//         SetBehaviorByDefault();
//     }
//
//     private void InitializeBehaviors()
//     {
//         _behaviorsMap = new Dictionary<Type, IGolemBehavior>();
//
//         _behaviorsMap[typeof(GolemBehaviorIdle)] = new GolemBehaviorIdle(_animator, _detectionManager);
//         _behaviorsMap[typeof(GolemBehaviorFollowing)] = new GolemBehaviorFollowing(
//             _animator, _pelvisTransform, _configurableJoint);
//         _behaviorsMap[typeof(GolemBehaviorAggressive)] = new GolemBehaviorAggressive();
//         _behaviorsMap[typeof(GolemBehaviorWithoutLegIdle)] = new GolemBehaviorWithoutLegIdle();
//         _behaviorsMap[typeof(GolemBehaviorWithoutLegAggressive)] = new GolemBehaviorWithoutLegAggressive();
//     }
//
//     private void SetBehavior(IGolemBehavior newBehavior)
//     {
//         _behaviorCurrent?.Exit();
//
//         _behaviorCurrent = newBehavior;
//         _behaviorCurrent.Enter();
//     }
//
//     private void SetBehavior(IGolemBehavior newBehavior, Transform Target)
//     {
//         _behaviorCurrent?.Exit();
//
//         if (newBehavior is IGolemBehaviorTarget behavior)
//         {
//             _behaviorWithCurrent = behavior;
//             _behaviorWithCurrent.Enter(Target);
//             Debug.LogWarning("Target selected. Start following...");
//         }
//         else
//         {
//             throw new Exception("New state isn`t a IGolemBehavior !");
//         }
//     }
//
//     private void SetBehaviorByDefault()
//     {
//         IGolemBehavior behaviorByDefault = GetBehavior<GolemBehaviorIdle>();
//         SetBehavior(behaviorByDefault);
//     }
//
//     private IGolemBehavior GetBehavior<T>() where T : IGolemBehavior
//     {
//         var type = typeof(T);
//         return _behaviorsMap[type];
//     }
//
//     public void FollowPath()
//     {
//         _behaviorCurrent?.FollowPath();
//         _behaviorWithCurrent?.FollowPath();
//         Debug.Log("StateMachine Updated!");
//     }
//
//     public void FixedUpdate()
//     {
//         Debug.LogWarning($"_behaviorCurrent : {_behaviorCurrent}, _behaviorWithCurrent : {_behaviorWithCurrent}");
//         _behaviorCurrent?.FixedUpdate();
//         _behaviorWithCurrent?.FixedUpdate();
//         Debug.Log("StateMachine FixedUpdated!");
//         Debug.LogWarning($"_behaviorCurrent : {_behaviorCurrent}, _behaviorWithCurrent : {_behaviorWithCurrent}");
//     }
//
//     public void SetGolemBehaviorIdle()
//     {
//         IGolemBehavior behavior = GetBehavior<GolemBehaviorIdle>();
//         SetBehavior(behavior);
//     }
//
//     public void SetGolemBehaviorAggressive()
//     {
//         IGolemBehavior behavior = GetBehavior<GolemBehaviorAggressive>();
//         SetBehavior(behavior);
//     }
//
//     public void SetBehaviorWalking(Transform Target)
//     {
//         IGolemBehavior behavior = GetBehavior<GolemBehaviorFollowing>();
//         SetBehavior(behavior, Target);
//     }
//
//     public void SetBehaviorWalkingWithoutLeg()
//     {
//         IGolemBehavior behavior = GetBehavior<GolemBehaviorWithoutLegIdle>();
//         SetBehavior(behavior);
//     }
//
//     public void SetGolemBehaviorWithoutLegAggressive()
//     {
//         IGolemBehavior behavior = GetBehavior<GolemBehaviorWithoutLegAggressive>();
//         SetBehavior(behavior);
//     }
// }