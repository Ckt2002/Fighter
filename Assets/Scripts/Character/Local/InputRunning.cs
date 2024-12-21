using Character.General;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Local
{
    public class InputRunning : MonoBehaviour
    {
        #region Variables

        [SerializeField] private CharacterStat stat;
        [SerializeField] private LocalCharacterController localCharacterController;
        [SerializeField] private LocalSensorManagement localSensorManagement;
        [SerializeField] private LocalNetwork localNetwork;
        [SerializeField] private InputAttacking inputAttacking;
        [SerializeField] private CharacterSound sound;

        private LocalAnimation localAnimation;
        private SpriteRenderer spriteRenderer;
        private CharacterStat characterStat;
        private Rigidbody2D rb2d;

        #endregion

        private void Start()
        {
            localAnimation = localCharacterController.LocalAnimation;
            spriteRenderer = localCharacterController.SpriteRenderer;
            characterStat = localCharacterController.CharacterStat;
            rb2d = localCharacterController.Rb2d;
            localCharacterController.FacingDirection = spriteRenderer.flipX ? -1 : 1;
        }

        private void Update()
        {
            if (stat.CurrentHealth <= 0 || IsTypingInInputField())
                return;
            RunInput();
        }

        private void RunInput()
        {
            float movement = Input.GetAxis("Horizontal");

            if (movement != 0)
            {
                spriteRenderer.flipX = movement < 0;
                localNetwork.IsFacingLeft = movement < 0;
                localCharacterController.FacingDirection = movement < 0 ? -1 : 1;
            }

            bool movementCondition = !localNetwork.IsRolling
                                    && !localNetwork.IsTakingDamage
                                    && !inputAttacking.IsAttacking;

            localNetwork.IsRunningAnimation = movementCondition
                                              && movement != 0
                                              && localSensorManagement.IsGrounded;

            localAnimation.MovementAnimation(movementCondition
                                             && movement != 0
                                             && localSensorManagement.IsGrounded);

            if (movementCondition)
                rb2d.velocity = new Vector2(movement * characterStat.Speed, rb2d.velocity.y);

            if (!localNetwork.IsRunningAnimation)
                sound.StopRunningSound();

            if (inputAttacking.IsAttacking && localSensorManagement.IsGrounded)
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
        }

        private bool IsTypingInInputField()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null;
            }
            return false;
        }
    }
}
