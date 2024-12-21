using UnityEngine;

public class TestEnemyTakeDamage : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int Hurt = Animator.StringToHash("Hurt");

    public void TakeDamage()
    {
        animator.SetTrigger(Hurt);
    }
}
