using UnityEngine;

public class ItemMarker : MonoBehaviour
{
    public ItemType ItemType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.15f);
        Gizmos.color = Color.white;
    }
}