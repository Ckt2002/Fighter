using Character.General;
using Character.Local;
using UnityEngine;

namespace Character.Remote
{
    public class SyncAnimation : MonoBehaviour
    {
        private static RemoteAnimation _remoteAnimation;
        private static bool _attackTemp = false;
        private static bool _takeDamageTemp = false;
        private static bool _revivalTemp = false;

        public static void GetCharacterAnimation(RemoteAnimation animation)
        {
            _remoteAnimation = animation;
        }

        public static void SyncRunning(bool isMoving)
        {
            _remoteAnimation.MovementAnimation(isMoving);
        }

        public static void SyncGrounded(bool isGrounded)
        {
            _remoteAnimation.Grounded(isGrounded);
        }

        public static void SyncFalling(float fallingVelocity)
        {
            _remoteAnimation.SetFalling(fallingVelocity);
        }

        public static void SyncJumping(bool isJumping)
        {
            _remoteAnimation.PlayJumpAnimation(isJumping);
        }

        public static void SyncRolling(bool isRolling)
        {
            _remoteAnimation.PlayRollAnimation(isRolling);
        }

        public static void SyncAttacking(bool isAttacking, int currentAttacking)
        {
            if (_attackTemp == isAttacking)
                return;
            _attackTemp = isAttacking;
            if (!isAttacking)
                return;

            _remoteAnimation.PlayAttackAnimation(currentAttacking);
        }

        public static void SyncHurting(bool isHurt)
        {
            if (_takeDamageTemp == isHurt)
                return;
            _takeDamageTemp = isHurt;
            if (!isHurt)
                return;
            _remoteAnimation.PlayHurtAnimation();
        }

        public static void SyncDeath()
        {
            _remoteAnimation.PlayDeathAnimation();
        }

        public static void SyncRevival(bool revival, ref bool isDeath)
        {
            if (revival == _revivalTemp)
                return;
            _revivalTemp = revival;
            // Debug.Log("revival " + revival);
            if (!revival)
                return;
            _remoteAnimation.PlayRevivalAnimation();
            isDeath = false;
            // Debug.Log("Revive");
        }
    }
}
