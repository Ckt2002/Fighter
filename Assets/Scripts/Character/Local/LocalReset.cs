using Character.General;
using UnityEngine;

namespace Character.Local
{
    public class LocalReset : MonoBehaviour
    {
        [SerializeField] private CharacterStat characterStat;
        [SerializeField] private LocalCharacterController localCharacterController;
        [SerializeField] private LocalAnimation localAnimation;
        [SerializeField] private LocalNetwork localNetwork;

        public void Reset()
        {
            characterStat.CurrentHealth = characterStat.MaxHealth;
            localNetwork.IsTakingDamage = false;
            localNetwork.Revival = true;
            localNetwork.IsDeath = false;
            localAnimation.PlayRevivalAnimation();
            localCharacterController.RunDeathAnimation = false;
        }
    }
}
