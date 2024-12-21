using Character.General;
using UnityEngine;

namespace Character.Remote
{
    public class SyncStat : MonoBehaviour
    {
        [SerializeField] private CharacterStat stat;

        public void Sync(int currentHealth, int maxHealth, int armor, int damage, float speed)
        {
            stat.CurrentHealth = currentHealth;
            stat.MaxHealth = maxHealth;
            stat.Armor = armor;
            stat.Damage = damage;
            stat.Speed = speed;
        }
    }
}
