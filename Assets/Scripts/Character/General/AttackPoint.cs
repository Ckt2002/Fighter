using UnityEngine;

namespace Character.General
{
    public class AttackPoint : MonoBehaviour
    {
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private LayerMask enemyLayer;

        public Collider2D[] SensorEnemies()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
            return hitEnemies;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
