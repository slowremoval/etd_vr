// using UnityEngine;
// using Cinemachine;
//
// namespace PatheticSouls
// {
//     public class AimController : MonoBehaviour
//     {
//
//         private AnimatorHandler _animatorHandler;
//         
//         public CinemachineFreeLook ThirdPersonCamera;
//         public CinemachineImpulseSource ImpulseSource;
//
//         private float _startCameraPositionTopRigRadius;
//         private float _startCameraPositionTopRigHeight;
//         private float _startCameraPositionMiddleRigRadius;
//         private float _startCameraPositionBottomRigRadius;
//
//         private float _onAimingDistance = 1.79f;
//         private float _onAimingTopHeight = 3.5f;
//
//         private float _maxCameraSpeedX = 155;
//         private float _maxCameraSpeedY = 1.15f;
//         private float _aimCameraSpeedX = 55;
//         private float _aimCameraSpeedY = 0.75f;
//
//         private void Awake()
//         {
//             _animatorHandler = GetComponentInChildren<AnimatorHandler>();
//         }
//
//         
//         private void OnEnable()
//         {
//             GlobalEventManager.OnStartAiming += StartAiming;
//             GlobalEventManager.OnEndAiming += EndAiming;
//             //GlobalEventManager.OnStartPulling += HandleWeaponPulling;
//         }
//
//         private void OnDisable()
//         {
//             GlobalEventManager.OnStartAiming -= StartAiming;
//             GlobalEventManager.OnEndAiming -= EndAiming;
//         }
//
//
//         private void Start()
//         {
//             _startCameraPositionTopRigRadius = ThirdPersonCamera.m_Orbits[0].m_Radius;
//             _startCameraPositionMiddleRigRadius = ThirdPersonCamera.m_Orbits[1].m_Radius;
//             _startCameraPositionBottomRigRadius = ThirdPersonCamera.m_Orbits[2].m_Radius;
//
//             _startCameraPositionTopRigHeight = ThirdPersonCamera.m_Orbits[0].m_Height;
//         }
//
//         private void StartAiming()
//         {
//             for (int i = 0; i < 3; i++)
//             {
//                 ThirdPersonCamera.m_Orbits[i].m_Radius = _onAimingDistance;
//             }
//
//             _animatorHandler.PlayTargetAnimation("Aiming", false, 0.02f);
//
//             ThirdPersonCamera.m_Orbits[0].m_Height = _onAimingTopHeight;
//             ThirdPersonCamera.m_XAxis.m_MaxSpeed = _aimCameraSpeedX;
//             ThirdPersonCamera.m_YAxis.m_MaxSpeed = _aimCameraSpeedY;
//         }
//
//         private void EndAiming()
//         {
//             ThirdPersonCamera.m_Orbits[0].m_Radius = _startCameraPositionTopRigRadius;
//             ThirdPersonCamera.m_Orbits[1].m_Radius = _startCameraPositionMiddleRigRadius;
//             ThirdPersonCamera.m_Orbits[2].m_Radius = _startCameraPositionBottomRigRadius;
//
//             ThirdPersonCamera.m_Orbits[0].m_Height = _startCameraPositionTopRigHeight;
//             ThirdPersonCamera.m_XAxis.m_MaxSpeed = _maxCameraSpeedX;
//             ThirdPersonCamera.m_YAxis.m_MaxSpeed = _maxCameraSpeedY;
//             _animatorHandler.PlayTargetAnimation("Empty", false, 0.06f);
//         }
//
//         public void GenerateAimImpulse(float impulseForce = 1)
//         {
//             ImpulseSource.GenerateImpulse(Vector3.right * impulseForce);
//         }
//     }
// }
