using Character.Local;
using UnityEngine;

namespace Character.General
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private AttackPoint[] attackPoints;

        public void AttackEnemies()
        {
            var currentDir =
                gameObject.GetComponentInParent<SpriteRenderer>().flipX ? attackPoints[0] : attackPoints[1];

            if (currentDir.SensorEnemies().Length <= 0) return;
            var enemies = currentDir.SensorEnemies();
            foreach (var target in enemies)
            {
                if (target.gameObject.GetComponent<LocalTakeDamage>())
                    target.gameObject.GetComponent<LocalTakeDamage>().ReduceHealth(CharacterStat.Instance.Damage);
            }
        }
    }
}
