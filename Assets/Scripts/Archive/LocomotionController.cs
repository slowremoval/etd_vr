// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
//
// public class LocomotionController : MonoBehaviour
// {
//     private CapsuleCollider _playerCollider;
//     private ActionBasedContinuousMoveProvider _continuousMoveProvider;
//     
//     public LayerMask GroundLayer;
//
//     private void Awake()
//     {
//         _playerCollider = GetComponentInParent<CapsuleCollider>();
//         _continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
//     }
//
//     private void FollowPath()
//     {
//         Vector3 rayOrigin = transform.TransformPoint(_playerCollider.center);
//         float rayLength = _playerCollider.center.y + 0.03f;
//         Ray ray = new Ray(rayOrigin, Vector3.down);
//         if (!Physics.SphereCast(ray, _playerCollider.radius, rayLength, GroundLayer))
//         {
//             if (_continuousMoveProvider.enabled)
//             {
//                 _continuousMoveProvider.enabled = false;
//             }
//         }
//         else
//         {
//             _continuousMoveProvider.enabled = true;
//         }
//     }
// }
