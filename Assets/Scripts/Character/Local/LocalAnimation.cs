using System.Collections.Generic;
using UnityEngine;

namespace Character.Local
{
    public class LocalAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly Dictionary<string, int> Hashes = new()
        {
            { "Death", Animator.StringToHash("Death") },
            { "Revival", Animator.StringToHash("Revival") },
            { "Grounded", Animator.StringToHash("Grounded") },
            { "Move", Animator.StringToHash("Move") },
            { "Jump", Animator.StringToHash("Jump") },
            { "Fall", Animator.StringToHash("Fall") },
            { "Roll", Animator.StringToHash("Roll") },
            { "Falling", Animator.StringToHash("Falling") },
            { "Attack", Animator.StringToHash("Attack") },
            { "Hurt", Animator.StringToHash("Hurt") }
        };

        public void PlayDeathAnimation()
        {
            animator.SetTrigger(Hashes["Death"]);
        }

        public void PlayRevivalAnimation()
        {
            animator.SetTrigger(Hashes["Revival"]);
        }

        public void Grounded(bool isGrounded)
        {
            animator.SetBool(Hashes["Grounded"], isGrounded);
        }

        public void MovementAnimation(bool isMoving)
        {
            animator.SetBool(Hashes["Move"], isMoving);
        }

        public void PlayJumpAnimation()
        {
            animator.SetTrigger(Hashes["Jump"]);
        }

        public void PlayFallAnimation()
        {
            animator.SetTrigger(Hashes["Fall"]);
        }

        public void PlayRollAnimation()
        {
            animator.SetTrigger(Hashes["Roll"]);
        }

        public void SetFalling(float fall)
        {
            animator.SetFloat(Hashes["Falling"], fall);
        }

        public void PlayAttackAnimation(int number)
        {
            if (number < 0)
            {
                animator.SetTrigger(Hashes["Attack"]);
                return;
            }

            animator.SetTrigger(Animator.StringToHash($"Attack{number}"));
        }

        public void PlayHurtAnimation()
        {
            animator.SetTrigger(Hashes["Hurt"]);
        }
    }
}
