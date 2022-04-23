// using UnityEditor;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// // public enum ControllerType
// // {
// //     RightHandController,
// //     LeftHandController
// // }
//
// public class VelocityTracker : MonoBehaviour
// {
//     //public ControllerType _controllerType;
//     public InputActionProperty VelocityProperty;
//     
//     public float HandVelocityMultiplier = 1;
//     
//     public Vector3 Velocity { get; private set; } = Vector3.zero;
//
//     private void Awake()
//     {
//         VelocityProperty.action.performed += SetVelocity;
//     }
//
//     private void SetVelocity(InputAction.CallbackContext context)
//     {
//         Velocity = context.ReadValue<Vector3>() * HandVelocityMultiplier;
//     }
// }