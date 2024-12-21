using Controller.Nakama_Controller;
using System.Threading.Tasks;
using UnityEngine;

namespace Controller.GameController
{
    public class AccountController : MonoBehaviour
    {
        public static AccountController Instance { get; private set; }

        private GameController controller;
        private StorageController storageController;
        private WalletController walletController;
        private ItemsController itemsController;

        private AccountNakama account;
        private DailyRewardNakama dailyReward;
        private WalletNakama wallet;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            controller = GameController.Instance;
            storageController = StorageController.Instance;
            walletController = WalletController.Instance;
            itemsController = ItemsController.Instance;

            account = AccountNakama.Instance;
            dailyReward = DailyRewardNakama.Instance;
            wallet = WalletNakama.Instance;
        }

        public async void CreateAccount(string email, string userName, string password)
        {
            await account.AccountCreate(email, password, userName);
            if (controller.Nakama.ErrorMessage.Equals(""))
                if (controller.Nakama.Session.Created)
                    await InitializeAccount(10, true);
                else
                    await HandleAccountCreationError("Account already exists please go to login!");
            else
                ShowErrorMessage(controller.Nakama.ErrorMessage);
        }

        public async void LoginAccount(string email, string password)
        {
            await account.AccountLogin(email, password);
            if (controller.Nakama.ErrorMessage.Equals(""))
                await InitializeAccount();
            else
                ShowErrorMessage(controller.Nakama.ErrorMessage);
        }

        private async Task InitializeAccount(int coinsForNewPlayer = 0, bool createNew = false)
        {
            controller.RegisterMatchHandlers();

            controller.AudioController.StopBackGroundMusic();
            controller.AudioController.StarPlayingList();

            int reward = await dailyReward.GetDailyRewardNotification();

            if (coinsForNewPlayer > 0)
                await wallet.UpdateWallet(coinsForNewPlayer, "create");

            walletController.GetCoinWallet();
            controller.MenuController.GoToHome();

            if (createNew)
                for (int i = 0; i < itemsController.Items.Count; i++)
                    storageController.AddStorageItem(i);

            storageController.GetItemsForStore(itemsController.Items);
            storageController.GetItemsForInventory(itemsController.Items);
            storageController.GetEquipedItemsForInventory(itemsController.Items);

            if (reward > 0)
            {
                controller.RewardDialog.SetNotifyMessage(reward);
                controller.MenuController.ShowRewardDialog();
            }

            controller.ProfileUI.SetProfile(controller.Nakama.Username, controller.Nakama.Email);
        }

        private async Task HandleAccountCreationError(string message)
        {
            await UserLogout();
            controller.ErrorDialog.SetErrorMessage(message);
            controller.MenuController.ShowErrDialog();
        }

        private void ShowErrorMessage(string errorMessage)
        {
            controller.ErrorDialog.SetErrorMessage(errorMessage);
            controller.MenuController.ShowErrDialog();
            controller.Nakama.ErrorMessage = "";
        }

        public async void Logout()
        {
            if (!controller.Nakama.PlayerId.Equals(""))
            {
                // Play main menu music
                controller.AudioController.StopPlayingList();
                controller.AudioController.PlayMainMenuBackGround();

                await UserLogout();
                controller.MenuController.OpenMainMenu();
            }
        }

        private async Task UserLogout()
        {
            await account.AccountLogout();
            controller.Nakama.PlayerId = "";
            controller.Nakama.Account = null;
        }
    }
}
