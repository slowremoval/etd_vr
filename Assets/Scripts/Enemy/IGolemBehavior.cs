using Unity.XR.CoreUtils;
using UnityEngine;

public interface IGolemBehavior
{
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
}

public interface IGolemBehaviorTarget : IGolemBehavior
{
    public void Enter(Transform currentTarget);
}
