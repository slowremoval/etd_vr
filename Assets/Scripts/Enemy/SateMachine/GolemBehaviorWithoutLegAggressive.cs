using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemBehaviorWithoutLegAggressive : IGolemBehavior
{
    //todo


    private float _lightAttackCooldown = 1f;
    private float _attackCooldown = 1.5f;
    private float _heavyAttackCooldown = 2f;
    private float _cooldownProgressTime = 0;

    private string[] attackArray = new string[2];

    private Animator _animator;
    private int _animationLayerIndex = 0;

    private bool _isAttacked;
    private GolemStates _golemStates;

    public GolemBehaviorWithoutLegAggressive(Animator animator, GolemStates golemStates)
    {
        _golemStates = golemStates;
        _animator = animator;

        attackArray[0] = "Stas_WithoutLeg_Attack";
    }

    public void Enter()
    {
        Debug.Log("Golem WITHOUT_LEG_AGGRESSIVE Enter");
        GetAttack();
    }

    public void Exit()
    {
        Debug.Log("Golem WITHOUT_LEG_AGGRESSIVE Exit");
    }

    public void Update()
    {
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
        var delay = attackTime * 1000 * 1.99f;
        Debug.Log($"Delay is {delay}");
        await Task.Delay((int) delay);
    }

    private async void GetAttack()
    {
        int attackNumber = Random.Range(0, attackArray.Length);
        StartAttackAnimation(attackNumber);

        float attackTime = GetCurrentAnimationLength(attackNumber);
        await CooldownAsync(attackTime);

        _golemStates.SetBehaviorIdleWithoutLeg();
    }
}