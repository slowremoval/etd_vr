using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class DetectionManager
{
    private Golem _golem;
    private Transform _golemTransform;

    public Transform CurrentTarget;
    public float Distance;

    public event Action<Transform> OnTargetDetected;

    public DetectionManager(Golem golem)
    {
        _golem = golem;
        _golemTransform = _golem.transform;
    }

    public void HandleDetection()
    {
        var golemPosition = _golemTransform.position;

        Collider[] colliders =
            Physics.OverlapSphere(golemPosition, _golem.DetectionRadius, _golem.DetectionLayer);

        foreach (var collider in colliders)
        {
            Transform playerOriginTransform = collider.transform;
            CapsuleCollider _playerCollider = playerOriginTransform.gameObject.GetComponent<CapsuleCollider>();
            Transform playerOrigin = playerOriginTransform.GetComponent<Transform>();
            Distance = Vector3.Distance(_playerCollider.transform.position, _golemTransform.position);


            if (playerOrigin != null)
            {
                Vector3 targetDirection = _playerCollider.center - golemPosition;
                Debug.Log("Playercollider.center is" + _playerCollider.center);
                float viewableAngle = Vector3.Angle(targetDirection, _golem.transform.forward);

                if (viewableAngle > _golem.MinimumDirectionAngle && viewableAngle < _golem.MaximumDirectionAngle)
                {
                    Vector3 directionToTarget = playerOriginTransform.position - golemPosition;
                    Debug.DrawRay(golemPosition, directionToTarget, Color.magenta);

                    Ray ray = new Ray(golemPosition, directionToTarget);
                    LayerMask obstacleLayer = _golem.DetectionObstacles;

                    //if (!Physics.Raycast(ray, out var hit, Distance, obstacleLayer))
                    //{
                        //Debug.Log($"Current Target is {CurrentTarget = playerOrigin}");
                        
                        CurrentTarget = playerOrigin;
                        OnTargetDetected?.Invoke(CurrentTarget.gameObject.transform);
                    //}
                }
            }
        }
    }

    //public void HandleRotationTowardsTarget(float delta, float rotationSpeed)
    //{
        //Vector3 targetVelocity = _golemTransform.InverseTransformDirection(_navMeshAgent.desiredVelocity);

        //_navMeshAgent.enabled = true;
        //_navMeshAgent.SetDestination(CurrentTarget.transform.position);
        //_enemyRb.velocity = targetVelocity;
        //_golemTransform.rotation = Quaternion.Slerp(_golemTransform.rotation, _navMeshAgent.transform.rotation,
        //rotationSpeed / delta);
    //}
}