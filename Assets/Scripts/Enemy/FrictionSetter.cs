using UnityEngine;

public class FrictionSetter : MonoBehaviour
{
    [SerializeField] private PhysicMaterial _defaultFriction;
    [SerializeField] private PhysicMaterial _zeroFriction;

    [SerializeField] private Collider[] _leftColliders;
    [SerializeField] private Collider[] _rightColliders;

    public void SetLeftFriction()
    {
        foreach (var collider in _leftColliders)
        {
            collider.material = _defaultFriction;
        }

        foreach (var collider in _rightColliders)
        {
            collider.material = _zeroFriction;
        }
        
        // _leftColliders.material = _defaultFriction;
        // _rightColliders.material = _zeroFriction;
    }
    
    public void SetRightFriction()
    {
        foreach (var collider in _leftColliders)
        {
            collider.material = _zeroFriction;
        }

        foreach (var collider in _rightColliders)
        {
            collider.material = _defaultFriction;
        }
        
        // _leftColliders.material = _zeroFriction;
        // _rightColliders.material = _defaultFriction;
    }
}