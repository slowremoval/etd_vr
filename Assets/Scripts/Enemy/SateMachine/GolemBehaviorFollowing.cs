using UnityEngine;

public class GolemBehaviorFollowing : IGolemBehaviorTarget
{
    private int animationHash = Animator.StringToHash("Stas_Walking");
    private Animator _animator;
    private Transform _pelvisTransform;
    private Transform _targetTransform;
    private ConfigurableJoint _configurableJoint;
    //private IGolemBehaviorTarget _golemBehaviorTargetImplementation;
    private GolemStates _golemStates;
    private UnitFollower _unitFollower;
    private float _stoppingDistance = 1.15f;

    public GolemBehaviorFollowing(Animator animator, Transform pelvisTransform, ConfigurableJoint configurableJoint, GolemStates golemStates)
    {
        _animator = animator;
        _pelvisTransform = pelvisTransform;
        _configurableJoint = configurableJoint;
        _golemStates = golemStates;
    }

    public void Enter()
    {
        //_golemBehaviorTargetImplementation.Enter();
    }

    public void Enter(Transform target)
    {
        _targetTransform = target;
        _animator.CrossFade(animationHash, 0.1f);
        _unitFollower = new UnitFollower(_configurableJoint, _pelvisTransform, target, _stoppingDistance);
        //Debug.Log(_animator == null);
        Debug.Log("Golem WALKING Enter");
    }

    public void Exit()
    {
        Debug.Log("Golem WALKING Exit");
        _targetTransform = null;
        _unitFollower = null;
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
        
        if (distance <= _stoppingDistance)
        {
            _golemStates.SetGolemBehaviorAggressive(_targetTransform);
            _unitFollower.StopPathFollowingTask();
            return;
        }
        else if (distance > 15f)
        {
            _golemStates.SetGolemBehaviorIdle();
            _unitFollower.StopPathFollowingTask();
            return;
        }

        _unitFollower.FollowPath();
    }

    public void FixedUpdate()
    {
        //     Debug.Log($"Walking is fixedUpdated! _targetTransform : {_targetTransform}");
        //     Debug.Log($"_targetTransform.position : {_targetTransform.position}, _pelvisTransform.position : {_pelvisTransform.position}");
        

        // Vector3 toTarget = _targetTransform.position - _pelvisTransform.position;
        // Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
        // Quaternion rotation = Quaternion.LookRotation(-toTargetXZ);
        //
        // _configurableJoint.targetRotation = Quaternion.Inverse(rotation);
    }
}