using UnityEngine;

namespace Physics_proj
{
    public class StepanMoving : MonoBehaviour
    {
        private Rigidbody _stepanRb;

        //private MagnetTarget _magnetTarget;
        private bool _isDirectionChanged;
        private Stepan _stepan;
        private CapsuleCollider _collider;

        [SerializeField] private Vector3 _moveDirection;

        [SerializeField] private float SpeedMultiplier = 0.42f;
        [SerializeField] private float MaxMoveVectorAxis = 1.19f;
        [SerializeField] private float MinMoveVectorAxis = 0.42f;
        [SerializeField] private float _walkHeight;

        public bool IsMoving = true;

        private void Awake()
        {
            _stepanRb = GetComponent<Rigidbody>();
            _stepan = GetComponent<Stepan>();
            _collider = GetComponent<CapsuleCollider>();
            //_magnetTarget = GetComponent<MagnetTarget>();
            _stepanRb.isKinematic = false;
        }

        private void Start()
        {
            _moveDirection = Vector3.forward;
        }

        private void OnTriggerEnter(Collider other)
        {
            _moveDirection = -_moveDirection;
        }

        private void HandleActivate()
        {
            if (_isDirectionChanged == false)
            {
                _moveDirection = GetRandomVector();
            }
        }

        private void HandleDeactivate()
        {
            if (_isDirectionChanged)
            {
                _isDirectionChanged = false;
            }
        }

        private Vector3 GetRandomVector()
        {
            Vector3 newVector = new Vector3(
                Random.Range(MinMoveVectorAxis, MaxMoveVectorAxis),
                0,
                Random.Range(MinMoveVectorAxis, MaxMoveVectorAxis)
            );
            return newVector;
        }

        private void FixedUpdate()
        {
            Vector3 rayOrigin = transform.position;
            rayOrigin.y += _collider.radius * 2;
            Ray ray = new Ray(rayOrigin, Vector3.down);

            RaycastHit hit;

            if (Physics.SphereCast(ray, _collider.radius + 0.35f, out hit, _walkHeight, _stepan.TargetLayerMask))
            {
                Move();
            }
        }

        private void Move()
        {
            HandleDeactivate();
            Vector3 direction = transform.position + _moveDirection;
            _stepanRb.AddForce(Vector3.up * 0.25f, ForceMode.VelocityChange);
            _stepanRb.MovePosition(Vector3.Lerp(transform.position, direction, Time.deltaTime * SpeedMultiplier));
        }
    }
}