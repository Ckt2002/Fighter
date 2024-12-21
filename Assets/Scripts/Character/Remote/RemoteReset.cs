using Character.General;
using Character.Local;
using UnityEngine;

namespace Character.Remote
{
    public class RemoteReset : MonoBehaviour
    {
        [SerializeField] private CharacterStat characterStat;
        [SerializeField] private RemoteController remoteController;
        [SerializeField] private RemoteAnimation remoteAnimation;

        // public void Reset()
        // {
        //     characterStat.CurrentHealth = characterStat.MaxHealth;
        //     remoteAnimation.PlayRevivalAnimation();
        //     remoteController.RunDeathAnimation = false;
        // }
    }
}
