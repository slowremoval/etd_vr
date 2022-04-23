
using UnityEngine;

namespace PatheticSouls
{
    public class Item : ScriptableObject
    {
        [Header("Item Info")]
        public Sprite ItemIcon;
        public string ItemName;

        public string LightAttack_1;
        public string HeavyAttack_1;
    }
}

