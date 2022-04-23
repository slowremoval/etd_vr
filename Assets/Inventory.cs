using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Transform _item;

    public Transform Item
    {
        get => _item;
        set => _item = value;
    }
}
