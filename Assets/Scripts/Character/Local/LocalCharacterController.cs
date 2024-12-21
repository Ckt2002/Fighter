using Character.General;
using Controller.GameController;
using Controller.Nakama_Controller;
using UnityEngine;

namespace Character.Local
{
    public class LocalCharacterController : MonoBehaviour
    {
        [SerializeField] private LocalAnimation localAnimation;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CharacterStat characterStat;
        [SerializeField] private LocalNetwork localNetwork;
        [SerializeField] private Rigidbody2D rb2d;

        private NakamaConnection nakamaConnection;
        public int FacingDirection { get; set; }
        public bool RunDeathAnimation { get; set; }

        private void Start()
        {
            RunDeathAnimation = false;
            nakamaConnection = NakamaConnection.Instance;
        }

        private void Update()
        {
            localNetwork.CurrentHealth = characterStat.CurrentHealth;
            localNetwork.MaxHealth = characterStat.MaxHealth;
            localNetwork.Armor = characterStat.Armor;
            localNetwork.Damage = characterStat.Damage;
            localNetwork.Speed = characterStat.Speed;

            localNetwork.IsDeath = characterStat.CurrentHealth <= 0;
            if (characterStat.CurrentHealth <= 0)
            {
                if (!BattleController.Instance.diedPlayers.Contains(nakamaConnection.PlayerId))
                    BattleController.Instance.diedPlayers.Add(nakamaConnection.PlayerId);
                if (RunDeathAnimation)
                    return;

                RunDeathAnimation = true;
                localAnimation.PlayDeathAnimation();
                BattleController.Instance.CheckPlayersAlive();
                return;
            }

            localAnimation.SetFalling(rb2d.velocity.y);
            localNetwork.FallingVelocity = rb2d.velocity.y;
        }

        #region getter setter

        public LocalAnimation LocalAnimation => localAnimation;

        public CharacterStat CharacterStat => characterStat;

        public SpriteRenderer SpriteRenderer => spriteRenderer;

        public Rigidbody2D Rb2d => rb2d;

        #endregion
    }
}
