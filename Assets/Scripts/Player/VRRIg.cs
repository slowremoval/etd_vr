using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform VRTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = VRTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = VRTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRIg : MonoBehaviour
{
    [SerializeField] private VRMap _head;
    [SerializeField] private VRMap _rightHand;
    [SerializeField] private VRMap _leftHand;

    [SerializeField] private Transform _headConstraint;
    [SerializeField] private Vector3 _headBodyOffset;

    void Start()
    {
        _headBodyOffset = transform.position - _headConstraint.position;
    }

    // FollowPath is called once per frame
    void LateUpdate()
    { 
        transform.position = _headConstraint.position + _headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(_headConstraint.up, Vector3.up).normalized;
        
        _head.Map();
        _rightHand.Map();
        _leftHand.Map();
    }
}