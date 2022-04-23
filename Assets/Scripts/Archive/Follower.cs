using UnityEngine;
using Zenject;

public class Follower : MonoBehaviour
{
    public Vector3 Offset;

    private Transform TargetTransform;
    
    [Inject]
    private void Construct(Inventory playerInventory)
    {
        TargetTransform = playerInventory.transform;
    }
    
    void FixedUpdate()
    {
        if (TargetTransform != null)
        {
            transform.position = TargetTransform.position
                                 + Vector3.up * Offset.y
                                 + Vector3.ProjectOnPlane(TargetTransform.right * Offset.x, Vector3.up)
                                 + Vector3.ProjectOnPlane(TargetTransform.forward * Offset.z, Vector3.up);

            transform.eulerAngles = new Vector3(0, TargetTransform.eulerAngles.y, 0);
        }
    }
}