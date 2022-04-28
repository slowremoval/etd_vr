using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemBehaviorAggressive : IGolemBehaviorTarget
{
    private float _lightAttackCooldown = 1f;
    private float _attackCooldown = 1.5f;
    private float _heavyAttackCooldown = 2f;
    private float _cooldownProgressTime = 0;

    private string[] attackArray = new string[2];
    
    private Animator _animator;
    private int _animationLayerIndex = 0;

    private bool _isAttacked;
    private GolemStates _golemStates;
    private Transform _target;
    private Transform _pelvisTransform;
    private ConfigurableJoint _joint;

    public GolemBehaviorAggressive(Animator animator, GolemStates golemStates, Transform pelvisTransform, ConfigurableJoint joint)
    {
        _golemStates = golemStates;
        _animator = animator;
        _joint = joint;
        _pelvisTransform = pelvisTransform;
        
        attackArray[0] = "Stas_LightAttack";
        attackArray[1] = "Stas_Attack";
    }

    public void Enter()
    {
    }

    public void Enter(Transform currentTarget)
    {
        Debug.Log("Golem AGGRESSIVE Enter");
        _target = currentTarget;
        GetAttack();
    }

    public void Exit()
    {
        Debug.Log("Golem AGGRESSIVE Exit");
    }

    public void Update()
    {
        Rotate();
    }

    private float GetCurrentAnimationLength(int attackNumber)
    {
        AnimationClip[] _animatorClips = _animator.runtimeAnimatorController.animationClips;
        float clipTime = 0;
        string clipName = String.Empty;
        foreach (var clip in _animatorClips)
        {
            if (clip.name == attackArray[attackNumber])
            {
                clipTime = clip.length;
                clipName = clip.name;
                break;
            }
        }


        Debug.Log($"clipName is {clipName}, clipTime : {clipTime}");
        return clipTime;
    }

    public void FixedUpdate()
    {
    }

    private void StartAttackAnimation(int attackNumber)
    {
        _animator.CrossFade(attackArray[attackNumber], 0.035f);
    }

    private async Task CooldownAsync(float attackTime)
    {
        float delay = attackTime * 1000;
        Debug.Log($"Delay is {delay}");
        await Task.Delay((int)delay);
        PushTarget();
        await Task.Delay((int)delay);
    }

    private  void PushTarget()
    {
        float distance = Vector3.Distance(_target.position, _pelvisTransform.position);
        
        if (distance >= 1.75f)
        {
            return;
        }
        
        Vector3 toTarget = _target.position - _pelvisTransform.position;
        
        Rigidbody targetRb = _target.gameObject.GetComponent<Rigidbody>();
        
        targetRb.AddForce(toTarget * 2100, ForceMode.Acceleration);
    }
    
    private async void GetAttack()
    {
        int attackNumber = Random.Range(0, attackArray.Length);
        StartAttackAnimation(attackNumber);
        
        float attackTime = GetCurrentAnimationLength(attackNumber);
        
        
        await CooldownAsync(attackTime);
        
        _golemStates.SetGolemBehaviorIdle();
    }
    
    private void Rotate()
    {
        Vector3 toTarget = _target.position - _pelvisTransform.position;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
        Quaternion rotation = Quaternion.LookRotation(-toTargetXZ);

        _joint.targetRotation = Quaternion.Inverse(rotation);
        Debug.Log("rotated!");
    }
}