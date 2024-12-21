using Controller.GameController;
using Data;
using UnityEngine;

namespace Character.Remote
{
    public class RemoteController : MonoBehaviour
    {
        [SerializeField] private RemoteAnimation remoteAnimation;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SyncStat syncStat;
        [SerializeField] private CharacterSound sound;
        [SerializeField] private SyncPosition syncPosition;

        public string PlayerId { set; get; }
        public PlayerState PlayerStates { get; set; }

        public bool runDeathAnimation;

        private void Start()
        {
            runDeathAnimation = false;
            SyncAnimation.GetCharacterAnimation(remoteAnimation);
        }

        private void Update()
        {
            if (PlayerStates == null)
                return;

            syncStat.Sync(PlayerStates.currentHealth,
                PlayerStates.maxHealth, PlayerStates.armor, PlayerStates.damage, PlayerStates.speed);
            syncPosition.SyncPos(PlayerStates, spriteRenderer);

            if (PlayerStates.currentHealth <= 0 || !PlayerStates.isRunningAnimation)
                sound.StopRunningSound();

            SyncAnimation.SyncRevival(PlayerStates.revival, ref runDeathAnimation);

            if (PlayerStates.currentHealth <= 0 && PlayerStates.isDeathAnimation)
            {
                if (!BattleController.Instance.diedPlayers.Contains(PlayerId))
                    BattleController.Instance.diedPlayers.Add(PlayerId);
                if (runDeathAnimation)
                    return;
                runDeathAnimation = true;
                SyncAnimation.SyncDeath();
                BattleController.Instance.CheckPlayersAlive();
                return;
            }

            // Animation
            SyncAnimation.SyncGrounded(PlayerStates.isGrounded);
            SyncAnimation.SyncRunning(PlayerStates.isRunningAnimation
                                      && !PlayerStates.isJumpingAnimation);
            SyncAnimation.SyncFalling(PlayerStates.fallingVelocity);
            SyncAnimation.SyncJumping(PlayerStates.isJumpingAnimation);
            SyncAnimation.SyncRolling(PlayerStates.isRollingAnimation);

            SyncAnimation.SyncAttacking(PlayerStates.isAttackingAnimation, PlayerStates.currentAttackAnimation);
            SyncAnimation.SyncHurting(PlayerStates.isTakingDamageAnimation);

            PlayerStates = null;
        }
    }
}
