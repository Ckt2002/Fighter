using Controller.MatchHandler;
using Data;
using UnityEngine;

namespace Character.Local
{
    public class LocalNetwork : MonoBehaviour
    {
        public bool IsFacingLeft { get; set; }

        public float FallingVelocity { get; set; }
        public bool IsRunningAnimation { get; set; }
        public bool IsRolling { get; set; }
        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }

        public bool IsAttacking { get; set; }
        public int CurrentAttackAnimation { get; set; }

        public bool IsTakingDamage { get; set; }

        public bool IsDeath { get; set; }
        public bool Revival { get; set; }

        public int Armor { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }

        private void Start()
        {
            IsDeath = false;
        }

        private void FixedUpdate()
        {
            var position = transform.position;

            var playerState = new PlayerState()
            {
                x = position.x,
                y = position.y,
                flipX = IsFacingLeft,

                fallingVelocity = FallingVelocity,
                isRunningAnimation = IsRunningAnimation,
                isRollingAnimation = IsRolling,
                isGrounded = IsGrounded,
                isJumpingAnimation = IsJumping,

                isAttackingAnimation = IsAttacking,
                currentAttackAnimation = CurrentAttackAnimation,

                isTakingDamageAnimation = IsTakingDamage,

                isDeathAnimation = IsDeath,
                revival = Revival,

                maxHealth = MaxHealth,
                currentHealth = CurrentHealth,
                armor = Armor,
                damage = Damage,
                speed = Speed,
            };

            SendMatchState.Instance.SendState(OpCodes.PlayerState, playerState);
        }
    }
}
