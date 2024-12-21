using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField]
    private AudioSource runningSource;
    [SerializeField]
    private AudioSource swordSwingSource;
    [SerializeField]
    private AudioSource hurtSource;
    [SerializeField]
    private AudioSource deathSource;

    public void RunningSound()
    {
        runningSource.Play();
    }

    public void StopRunningSound()
    {
        runningSource.Stop();
    }

    public void SwordSwingSound()
    {
        swordSwingSource.Play();
    }

    public void StopSwordSwingSound()
    {
        swordSwingSource.Stop();
    }

    public void HurtSound()
    {
        hurtSource.Play();
    }

    public void StopHurtSound()
    {
        hurtSource.Stop();
    }

    public void DeathSound()
    {
        deathSource.Play();
    }

    public void StopDeathSound()
    {
        deathSource.Stop();
    }
}
