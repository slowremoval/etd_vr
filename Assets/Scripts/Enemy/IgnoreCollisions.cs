using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private Collider[] _allColliders;

    private void Awake()
    {
        for (int i = 0; i < _allColliders.Length; i++)
        {
            foreach (var collider in _allColliders)
            {
                Physics.IgnoreCollision(_allColliders[i], collider, true);
            }
        }
    }
}
