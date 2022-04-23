using Import.Physics_7.Scripts.StepanScripts;
using UnityEngine;

namespace Physics_proj
{
    public class Stepan : MonoBehaviour
    {
        public LayerMask TargetLayerMask;

        [SerializeField] private float _distanceToStep;

        [SerializeField] private Transform[] _legsCount;
        [SerializeField] private Transform[] _targetIKTransforms;
        [SerializeField] private Transform[] _rayOrigins;

        [SerializeField] private Transform defaultLegPosition;

        [SerializeField] private bool _isActive = true;
        
        //[Space] private MagnetTarget _magnetTarget;

        private Rigidbody _stepanRb;
        private Animator _animator;
        private StepansLeg[] _legs;
        private CapsuleCollider _collider;
        private StepanMoving _stepanMoving;

        private void Awake()
        {
            //_magnetTarget = GetComponent<MagnetTarget>();
            _animator = GetComponent<Animator>();
            _stepanRb = GetComponent<Rigidbody>();
            _legs = new StepansLeg[_legsCount.Length];
            _collider = GetComponent<CapsuleCollider>();
            _stepanMoving = GetComponent<StepanMoving>();
            
            for (int i = 0; i < _legsCount.Length; i++)
            {
                _legs[i] = new StepansLeg(_rayOrigins[i], _targetIKTransforms[i], _distanceToStep, TargetLayerMask);
            }
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                _animator.enabled = true;
                HandleLegsMovement();
            }
            else
            {
                _animator.enabled = false;
            }
        }

        private void HandleLegsMovement()
        {
            for (int i = 0; i < _legs.Length; i++)
            {
                _legs[i].HandleMovement(_rayOrigins[i].position);
            }
        }

        private void HandleDefaultLegsPosition()
        {
            for (int i = 0; i < _legs.Length; i++)
            {
                _legs[i].HandleHanging(defaultLegPosition.position);
            }
        }
    }
}