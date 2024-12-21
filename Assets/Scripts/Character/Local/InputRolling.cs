using Character.General;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Local
{
    public class InputRolling : MonoBehaviour
    {
        [SerializeField] private CharacterStat stat;
        [SerializeField] private LocalCharacterController localCharacterController;
        [SerializeField] private LocalSensorManagement localSensorManagement;
        [SerializeField] private LocalNetwork localNetwork;
        [SerializeField] private InputAttacking inputAttacking;

        private LocalAnimation localAnimation;
        private CharacterStat characterStat;
        private Rigidbody2D rb2d;

        public bool IsRolling { get; set; }

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
            RollInput();
        }

        private void RollInput()
        {
            bool rollCondition = Input.GetKeyDown(KeyCode.LeftShift) && !IsRolling &&
                                localSensorManagement.IsGrounded && !localNetwork.IsTakingDamage &&
                                !inputAttacking.IsAttacking;
            if (rollCondition)
                localAnimation.PlayRollAnimation();

            if (IsRolling)
                rb2d.velocity = new Vector2(localCharacterController.FacingDirection * characterStat.RollForce,
                    rb2d.velocity.y);
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
