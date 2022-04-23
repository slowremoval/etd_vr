using UnityEngine;

public class GolemBehaviorWithoutLeg : IGolemBehaviorTarget
{
    private Animator _animator;
    private int animationHash = Animator.StringToHash("Stas_WithoutLegs");

    private UnitFollower _unitFollower;
    private Transform _pelvisTransform;
    private Transform _targetTransform;
    private ConfigurableJoint _configurableJoint;
    private GolemStates _golemStates;
    
    public GolemBehaviorWithoutLeg(Animator animator, Transform pelvisTransform, ConfigurableJoint configurableJoint, GolemStates golemStates)
    {
        _animator = animator;
        _pelvisTransform = pelvisTransform;
        _configurableJoint = configurableJoint;
        _golemStates = golemStates;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
        _targetTransform = null;
        _unitFollower = null;
        Debug.Log("Golem WITHOUT_LEG_IDLE Exit");
    }

    public void Update()
    {
        
        if (_targetTransform == null)
        {
            Debug.LogError("TargetTransform is null!");
            _unitFollower.StopPathFollowingTask();
            return;
        }

        float distance = Vector3.Distance(_targetTransform.position, _pelvisTransform.position);
        
        if (distance <= 1.15f)
        {
            _golemStates.SetGolemBehaviorWithoutLegAggressive();
            _unitFollower.StopPathFollowingTask();
            return;
        }
        else if (distance > 15f)
        {
            _golemStates.SetBehaviorIdleWithoutLeg();
            _unitFollower.StopPathFollowingTask();
            return;
        }

        _unitFollower.FollowPath();
    }

    public void FixedUpdate()
    {
    }

    public void Enter(Transform currentTarget)
    {
        _animator.CrossFade(animationHash, 0.03f);
        _targetTransform = currentTarget;
        _unitFollower = new UnitFollower(_configurableJoint, _pelvisTransform, currentTarget);
        Debug.Log("Golem WITHOUT_LEG_IDLE Enter");
    }
}