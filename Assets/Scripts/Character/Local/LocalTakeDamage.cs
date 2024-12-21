using Character.General;
using Controller.GameController;
using UnityEngine;

namespace Character.Local
{
    public class LocalTakeDamage : MonoBehaviour
    {
        [SerializeField] private LocalAnimation localAnimation;
        [SerializeField] private CharacterStat stat;

        public void ReduceHealth(int damageTaken)
        {
            if (stat.CurrentHealth <= 0)
                return;

            localAnimation.PlayHurtAnimation();
            if (!GameController.Instance.GameStart)
                return;

            var remainDamage = Mathf.Abs(stat.Armor - damageTaken);
            stat.CurrentHealth -= remainDamage;
        }
    }
}
