using UnityEngine;

namespace Data
{
    public enum ItemType
    {
        Helmet,
        Armor,
        Weapon,
        Shield,
        Boot,
    }

    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public ItemType itemType;

        public int armor;
        public int health;
        public int damage;
        public int speed;

        public int coins;
    }
}
