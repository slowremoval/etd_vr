using UnityEngine;

namespace PatheticSouls
{
    public class WeaponThrower1 : MonoBehaviour
    {
        //private AnimatorHandler _animatorHandler;
        //private PlayerManager _playerManager;
        //private InputHandler _inputHandler;
        //private AimController _aimController;


        private Vector3 _pullPosition;
        
        public Transform CurvePoint;

        //private WeaponScript _weaponScript;
        private float _returnTime;
        
        public Transform Hand;
        
        [Space] public float ThrowPower = 30;
        
        private void Awake()
        {
            //_animatorHandler = GetComponentInChildren<AnimatorHandler>();
            //_playerManager = GetComponent<PlayerManager>();
            //_aimController = GetComponent<AimController>();
        }

        private void Start()
        {
            //if (_weaponScript == null)
            {
             //   _weaponScript = GetComponentInChildren<WeaponScript>();
            }
        }

        private void OnEnable()
        {
            //GlobalEventManager.OnStartPulling += HandleWeaponPulling;
        }

        private void OnDisable()
        {
            //GlobalEventManager.OnStartPulling -= HandleWeaponPulling;
        }

        private void HandleWeaponPulling()
        {
            StartWeaponPulling();
        }
        
        private void Update()
        {
            //if (_playerManager.IsPulling)
            {
                if (_returnTime < 1)
                {
                  //  _weaponScript.transform.position = GetQuadraticCurvePoint(_returnTime, _pullPosition, CurvePoint.position, HandControllerInput.position);
                    _returnTime += Time.deltaTime * 1.5f;
                    
                }
                else
                {
                    WeaponCatch();
                }
            }
        }

        public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }
        
        public void StartWeaponPulling()
        {
            // _pullPosition = _weaponScript.transform.position;
            // _weaponScript._weaponRigidbody.Sleep();
            // _weaponScript._weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            // _weaponScript._weaponRigidbody.isKinematic = true;
            //_playerManager.IsPulling = true;
            //_animatorHandler.PlayTargetAnimation("Pulling_2", true, 0.05f);
        }

        public void WeaponCatch(){
            _returnTime = 0;
            //IsPulling = false;
            //_playerManager.IsPulling = false;
            // _weaponScript.transform.Parent = HandControllerInput;
            // _weaponScript.IsThrowed = false;
            // _weaponScript.transform.localRotation = Quaternion.identity;
            // _weaponScript.transform.localPosition = Vector3.zero;
            //_playerManager.HasWeapon = true;
            //_aimController.GenerateAimImpulse();
            //_animatorHandler.PlayTargetAnimation("Empty", false, 0.2f);
            
        }
        
        //public void HandleWeaponThrow(WeaponItem weapon)
        //{
            //_playerManager.HasWeapon = false;
            // _weaponScript.IsThrowed = true;
            // _weaponScript._weaponRigidbody.isKinematic = false;
            // _weaponScript._weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            // _weaponScript.transform.Parent = null;
            // _weaponScript.transform.eulerAngles = Camera.main.transform.eulerAngles + new Vector3(0, 90, 0);
            // _weaponScript.transform.position += transform.right / 5;
            // _weaponScript._weaponRigidbody.AddForce(Camera.main.transform.forward * ThrowPower + transform.up * 2,
            //     ForceMode.Impulse);

            //_animatorHandler.PlayTargetAnimation("Throwing", true, 0);
        //}
    }
}