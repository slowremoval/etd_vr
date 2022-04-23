using Unity.XR.CoreUtils;
using UnityEngine;

public class HeadsetFollower : MonoBehaviour
{
    private float _additionalHeight = 0.2f;
    
    private CapsuleCollider _collider;
    private XROrigin _xrOrigin;
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _xrOrigin = GetComponent<XROrigin>();
    }

    void FixedUpdate()
    {
        CapsuleFollowHeadset();
    }
    
    private void CapsuleFollowHeadset()
    {
        _collider.height = _xrOrigin.CameraInOriginSpaceHeight + _additionalHeight;
        //rotate collider with camera, didn`t test before!11!
        _collider.transform.rotation = Quaternion.Euler(
            new Vector3(_collider.transform.rotation.x, _xrOrigin.transform.rotation.y, _collider.transform.rotation.z));
        Vector3 capsuleCenter = transform.InverseTransformPoint(_xrOrigin.Camera.gameObject.transform.position);
        _collider.center = new Vector3(
            capsuleCenter.x,
            (_collider.height / 2) + 0.03f,
            capsuleCenter.z
        );
    }
}