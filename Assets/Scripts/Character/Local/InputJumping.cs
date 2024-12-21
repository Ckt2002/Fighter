using Character.General;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Local
{
    public class InputJumping : MonoBehaviour
    {
        [SerializeField] private CharacterStat stat;
        [SerializeField] private LocalCharacterController localCharacterController;
        [SerializeField] private LocalSensorManagement localSensorManagement;
        [SerializeField] private LocalNetwork localNetwork;

        private LocalAnimation localAnimation;
        private CharacterStat characterStat;
        private Rigidbody2D rb2d;

        private void Start()
        {
            localAnimation = localCharacterController.LocalAnimation;
            characterStat = localCharacterController.CharacterStat;
            rb2d = localCharacterController.Rb2d;
        }

        private void Update()
        {
            if (stat.CurrentHealth <= 0 || IsTypingInInputField())
                return;
            JumpInput();
        }

        private void JumpInput()
        {
            bool jumpCondition = Input.GetKeyDown(KeyCode.Space) && localSensorManagement.IsGrounded &&
                                !localNetwork.IsRolling && !localNetwork.IsTakingDamage;

            localNetwork.IsJumping = rb2d.velocity.y > 0;
            if (!jumpCondition)
                return;

            localAnimation.PlayJumpAnimation();
            rb2d.velocity = new Vector2(rb2d.velocity.x, characterStat.JumpForce);
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
