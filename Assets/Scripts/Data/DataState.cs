using System;

namespace Data
{
    [Serializable]
    public class SpawnPlayerState
    {
        public int charIndex;
    }

    [Serializable]
    public class ReadyState
    {
        public bool ready;
    }

    [Serializable]
    public class PlayerState
    {
        // Position, direction
        public float x;
        public float y;
        public bool flipX;

        // Movement type
        public float fallingVelocity;
        public bool isRunningAnimation;
        public bool isRollingAnimation;
        public bool isGrounded;
        public bool isJumpingAnimation;

        // Attacking
        public bool isAttackingAnimation;
        public int currentAttackAnimation;

        // TakeDamage
        public bool isTakingDamageAnimation;

        // Status
        public bool isDeathAnimation;
        public bool revival;

        // Stat
        public int armor;
        public int maxHealth;
        public int currentHealth;
        public int damage;
        public float speed;
    }
}
