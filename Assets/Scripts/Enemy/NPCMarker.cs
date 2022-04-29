using UnityEngine;

public class NPCMarker : MonoBehaviour
{
    public NPCType NPCType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.25f);
        Gizmos.color = Color.white;
    }
}