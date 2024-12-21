using Character.Local;
using UnityEngine;

namespace Character.General
{
    public class CharacterAnimationEvents : MonoBehaviour
    {
        [SerializeField] private CharacterSound sound;
        [SerializeField] private Attack attack;
        [SerializeField] private LocalNetwork localNetwork;
        [SerializeField] private LocalCharacterController localCharacterController;
        [SerializeField] private InputRolling inputRolling;
        [SerializeField] private InputAttacking inputAttacking;

        private void CauseDamage()
        {
            if (attack != null)
                attack.AttackEnemies();
        }

        private void StartRoll()
        {
            if (inputRolling == null)
                return;
            inputRolling.IsRolling = true;
            localNetwork.IsRolling = true;
        }

        private void EndRoll()
        {
            if (inputRolling == null)
                return;
            inputRolling.IsRolling = false;
            localNetwork.IsRolling = false;
        }

        private void StartAttack()
        {
            if (inputAttacking == null || localNetwork == null)
                return;
            inputAttacking.IsAttacking = true;
            localNetwork.IsAttacking = true;
        }

        private void EndAttack()
        {
            if (inputAttacking == null || localNetwork == null)
                return;
            inputAttacking.IsAttacking = false;
            localNetwork.IsAttacking = false;
        }

        private void StartTakeDamage()
        {
            if (localNetwork != null)
                localNetwork.IsTakingDamage = true;
        }

        private void EndTakeDamage()
        {
            if (localNetwork != null)
                localNetwork.IsTakingDamage = false;
        }

        private void Revival()
        {
            if (localNetwork != null)
                localNetwork.Revival = false;
        }

        private void Death()
        {
            if (localCharacterController != null)
                localCharacterController.RunDeathAnimation = true;
        }

        private void PlayRunningSound()
        {
            sound.RunningSound();
        }

        private void PlaySwordSwingSound()
        {
            sound.SwordSwingSound();
        }
        private void StopSwordSwingSound()
        {
            sound.StopSwordSwingSound();
        }

        private void PlayHurtSound()
        {
            sound.HurtSound();
        }
        private void StopHurtSound()
        {
            sound.StopHurtSound();
        }

        private void PlayDeathSound()
        {
            sound.DeathSound();
        }
        private void StopDeathSound()
        {
            sound.StopDeathSound();
        }
    }
}
