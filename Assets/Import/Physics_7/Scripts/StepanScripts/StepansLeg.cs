using Physics_proj;
using UnityEngine;

namespace Import.Physics_7.Scripts.StepanScripts
{
    public class StepansLeg
    {

        private Transform _targetIKTransform;

        private RayHolder _rayHolder;
        
        private LayerMask _targetLayer;


        private float _minMoveDistance;
        private float _startMinMoveDistance;

        private Vector3 _currentPosition;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        
        private bool _isMoving;
        
        private float _currentStatePosition;
        private float _currentLiftPosition;
        private float _legSpeed;

        private float _maxStepSpeed = 3.7f;
        private float _minStepSpeed = 2.7f;
        private float _moveDistanceInaccuracy = 0.2f;
        
        public StepansLeg(Transform rayOrigin, Transform targetIKTransform, float minMoveDistance,
            LayerMask targetLayer)
        {
            _targetLayer = targetLayer;
            _rayHolder = new RayHolder(_targetLayer, rayOrigin, Vector3.down);
            _targetIKTransform = targetIKTransform;
            _minMoveDistance = minMoveDistance;
            _startMinMoveDistance = minMoveDistance;
        }

        public bool IsNeedsToStep(Vector3 origin, out Vector3 newPosition)
        {
            newPosition = _rayHolder.GetGroundHit(origin).point;
            float distance = Vector3.Distance(_targetIKTransform.position, newPosition);
            return distance >= _minMoveDistance;
        }

        public void HandleHanging(Vector3 legPosition)
        {
            _targetIKTransform.position = Vector3.Lerp(_startPosition, legPosition, _currentLiftPosition); 
            
            _currentLiftPosition += Time.deltaTime;
            
            if (_currentLiftPosition >= 1)
            {
                _currentLiftPosition = 1;
            }
        }

        
        public void HandleMovement(Vector3 origin)
        {
            bool isNeedsToStep = IsNeedsToStep(origin, out Vector3 newPosition);
            
            if (isNeedsToStep && _isMoving == false)
            {
                _minMoveDistance = Random.Range(
                    _startMinMoveDistance - _moveDistanceInaccuracy, 
                    _startMinMoveDistance + _moveDistanceInaccuracy
                    );
                _legSpeed = Random.Range(_minStepSpeed, _maxStepSpeed);
                
                _startPosition = _targetIKTransform.position;
                _targetPosition = newPosition;
                _currentPosition = newPosition;
                _isMoving = true;
            }
            else if (_isMoving == false)
            {
                _targetIKTransform.position = _currentPosition;
            }
            else
            {
                HandleLegLifting();
                
                _currentLiftPosition = 0;
                
                _targetIKTransform.position = Vector3.Lerp(_startPosition, _targetPosition, _currentStatePosition);
                
                _currentStatePosition += Time.deltaTime * _legSpeed;
                
                if (_currentStatePosition >= 1)
                {
                    _currentStatePosition = 0;
                    _isMoving = false;
                }
            }
        }
        
        private void HandleLegLifting()
        {
            if (_currentStatePosition < 0.5f)
            {
                _targetPosition.y += 0.027f;
            }
            else
            {
                _targetPosition.y -= 0.027f;
            }
        }
    }
}
