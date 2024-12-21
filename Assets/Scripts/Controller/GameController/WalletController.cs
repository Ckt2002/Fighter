using Controller.Nakama_Controller;
using UnityEngine;

namespace Controller.GameController
{
    public class WalletController : MonoBehaviour
    {
        public static WalletController Instance;

        private GameController controller;

        private WalletNakama wallet;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            controller = GameController.Instance;

            wallet = WalletNakama.Instance;
        }

        public async void GetCoinWallet()
        {
            int coins = await wallet.GetUserWallet();
            controller.ProfileController.Money = coins;

            // Update UI
            controller.StoreUI.SetMoneyText(coins);
            controller.ProfileUI.SetMoneyText(coins);
        }
    }
}
