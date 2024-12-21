using UnityEngine;

namespace Controller
{
    public class ProfileController : MonoBehaviour
    {
        public static ProfileController Instance;

        private int money;

        private void Awake()
        {
            Instance = this;
        }

        public int Money
        {
            set => this.money = value;
            get => this.money;
        }
    }
}
