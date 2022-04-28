using UnityEngine;
using Zenject;

public class GrapplingGun : MonoBehaviour
{
    private SpringJoint _springJoint;
    private Vector3 _grapplePoint;
    private Vector2 _rightJoystickValues;
    private Rigidbody _playerRb;

    public AimVisualizer _aimVisualizer;

    private const float _joystickValueScaler = 0.042f;
    private const float _startMinDistanceScaler = 0.15f;
    private const float _startMaxDistanceScaler = 0.85f;

    [Header("Grapple properties")] [SerializeField]
    private LayerMask WhatIsGrappleable;

    [SerializeField] private float MaxDistance = 17;
    [SerializeField] private float SpringForce = 90;
    [SerializeField] private float SpringDamper = 7.5f;
    [SerializeField] private GameObject Aim;

    public Transform GunTip, Player;

    [HideInInspector] public HandControllerInput activeHandControllerInput;
    private GrapplingRope _grapplingRope;
    public AnimationCurve AffectCurve;
    private LineRenderer _lineRenderer;

    [Inject]
    private void Construct(Inventory playerInventory)
    {
        _playerRb = playerInventory.gameObject.GetComponent<Rigidbody>();
        Player = playerInventory.transform;
        Debug.Log($"PlayerRb is : {_playerRb}");
    }

    private void Awake()
    {
        _aimVisualizer = new AimVisualizer(Aim, GunTip, MaxDistance, WhatIsGrappleable);

        _lineRenderer = GetComponent<LineRenderer>();

        _grapplingRope = new GrapplingRope(AffectCurve, this, _lineRenderer);
    }

    private void FixedUpdate()
    {
        if (IsGrappling() && _springJoint.maxDistance > _springJoint.minDistance)
        {
            _springJoint.maxDistance -= activeHandControllerInput.JoystickValue.y * _joystickValueScaler;
            _playerRb.AddForce(new Vector3(-activeHandControllerInput.Velocity.x,
                -activeHandControllerInput.Velocity.y,
                -activeHandControllerInput.Velocity.z
            ) * 0.025f, ForceMode.Impulse);
        }
        else if (IsGrappling())
        {
            _springJoint.maxDistance -= activeHandControllerInput.Velocity.magnitude;
            _playerRb.AddForce(new Vector3(-activeHandControllerInput.Velocity.x,
                -activeHandControllerInput.Velocity.y,
                -activeHandControllerInput.Velocity.z
            ) * 0.025f, ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        _aimVisualizer.Aiming();
        _grapplingRope.DrawRope();
    }

    public void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(GunTip.position, GunTip.forward, out hit, MaxDistance, WhatIsGrappleable))
        {
            _grapplePoint = hit.point;
            _springJoint = Player.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = _grapplePoint;

            float distanceFromPoint = Vector3.Distance(Player.position, _grapplePoint);

            _springJoint.maxDistance = distanceFromPoint * _startMaxDistanceScaler;
            _springJoint.minDistance = distanceFromPoint * _startMinDistanceScaler;

            _springJoint.spring = SpringForce;
            _springJoint.damper = SpringDamper;
            _springJoint.massScale = 2f;
        }
    }


    public void StopGrapple()
    {
        Destroy(_springJoint);
    }

    public bool IsGrappling()
    {
        return _springJoint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return _grapplePoint;
    }
}