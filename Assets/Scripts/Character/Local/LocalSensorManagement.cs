using Character.General;
using UnityEngine;

namespace Character.Local
{
    public class LocalSensorManagement : MonoBehaviour
    {
        [SerializeField] private LocalSensor groundSensor;
        [SerializeField] private LocalAnimation localAnimation;
        [SerializeField] private CharacterStat characterStat;
        [SerializeField] private LocalNetwork localNetwork;

        public bool IsGrounded { get; private set; } = true;

        private void Update()
        {
            if (characterStat.CurrentHealth <= 0)
                return;

            localNetwork.IsGrounded = groundSensor.State();
            IsGrounded = groundSensor.State();
            localAnimation.Grounded(IsGrounded);
        }
    }
}
