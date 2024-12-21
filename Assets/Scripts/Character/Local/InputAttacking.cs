using Character.General;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Local
{
    public class InputAttacking : MonoBehaviour
    {
        [SerializeField] private bool isBandit = false;
        [SerializeField] private LocalCharacterController localCharacterController;
        [SerializeField] private CharacterStat stat;
        [SerializeField] private LocalNetwork localNetwork;

        private LocalAnimation localAnimation;

        private const float MinAttackInterval = 0.25f;
        private const float MaxAttackDuration = 0.8f;
        private const int MaxAttackCombo = 3;

        public bool IsAttacking { get; set; } = false;

        private float TimeSinceAttack { get; set; }
        private int CurrentAttackAnimation { get; set; }

        private void Start()
        {
            localAnimation = localCharacterController.LocalAnimation;
            TimeSinceAttack = 0f;
            CurrentAttackAnimation = 0;
        }

        private void Update()
        {
            if (stat.CurrentHealth <= 0 || IsTypingInInputField())
                return;
            AttackInput();
        }

        private void AttackInput()
        {
            TimeSinceAttack += Time.deltaTime;
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!Input.GetMouseButtonDown(0) || TimeSinceAttack <= MinAttackInterval || IsAttacking)
                return;

            if (isBandit)
            {
                PlayAttack(-1);
                localNetwork.CurrentAttackAnimation = -1;
                return;
            }

            HeroAttack();
        }

        private void HeroAttack()
        {
            ++CurrentAttackAnimation;
            if (CurrentAttackAnimation > MaxAttackCombo)
                CurrentAttackAnimation = 1;
            if (TimeSinceAttack > MaxAttackDuration)
                CurrentAttackAnimation = 1;
            localNetwork.CurrentAttackAnimation = CurrentAttackAnimation;
            PlayAttack(CurrentAttackAnimation);
        }

        private void PlayAttack(int currentAttackAnimation)
        {
            localAnimation.PlayAttackAnimation(currentAttackAnimation);
            localNetwork.CurrentAttackAnimation = currentAttackAnimation;
            TimeSinceAttack = 0.0f;
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
