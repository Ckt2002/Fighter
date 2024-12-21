using Controller.GameController;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController Instance;

        [Header("Dialog")][SerializeField] private GameObject errorDialog;
        [SerializeField] private GameObject rewardDialog;

        [Header("Menu")][SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject createAccountForm;
        [SerializeField] private GameObject loginForm;

        [Header("In-game")][SerializeField] private GameObject homeUI;
        [SerializeField] private GameObject storeUI;
        [SerializeField] private GameObject matchUI;
        [SerializeField] private GameObject profileUI;
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private GameObject inMatchUI;

        [Header("Option")]
        [SerializeField] private Button optionButton;
        [SerializeField] private GameObject optionUI;

        [Header("Chat")]
        [SerializeField] private GameObject chatUI;

        private void Awake()
        {
            Instance = this;
        }

        #region Dialog

        public void ShowErrDialog()
        {
            errorDialog.SetActive(true);
        }

        public void HideErrDialog()
        {
            errorDialog.SetActive(false);
        }

        public void ShowRewardDialog()
        {
            rewardDialog.SetActive(true);
        }

        #endregion

        #region Main Menu

        public void OpenMainMenu()
        {
            loginForm.SetActive(false);
            createAccountForm.SetActive(false);
            mainMenu.SetActive(true);

            homeUI.SetActive(false);
        }

        public void OpenLoginForm()
        {
            mainMenu.SetActive(false);
            loginForm.SetActive(true);
            createAccountForm.SetActive(false);
        }

        public void OpenCreateAccountForm()
        {
            mainMenu.SetActive(false);
            loginForm.SetActive(false);
            createAccountForm.SetActive(true);
        }

        public void QuitGame()
        {
            AccountController.Instance.Logout();
            Application.Quit();
        }

        #endregion

        #region In-game Menu

        public void GoToHome()
        {
            loginForm.SetActive(false);
            createAccountForm.SetActive(false);
            mainMenu.SetActive(false);

            storeUI.SetActive(false);
            matchUI.SetActive(false);
            inventoryUI.SetActive(false);
            optionUI.SetActive(false);
            profileUI.SetActive(false);

            homeUI.SetActive(true);
        }

        public void GoToStore()
        {
            homeUI.SetActive(false);
            storeUI.SetActive(true);
        }

        public void GoToInventory()
        {
            homeUI.SetActive(false);
            inventoryUI.SetActive(true);
        }

        public void GoToMatch()
        {
            optionButton.gameObject.SetActive(true);
            homeUI.SetActive(false);
            matchUI.SetActive(true);
        }

        public void GoToProfile()
        {
            homeUI.SetActive(false);
            profileUI.SetActive(true);
        }

        public void GoToInMatch()
        {
            optionButton.gameObject.SetActive(false);
            homeUI.SetActive(false);
            matchUI.SetActive(false);
            inMatchUI.SetActive(true);
        }

        public void HideInMatchUI()
        {
            inMatchUI.SetActive(false);
        }

        public void ShowInMatchUI()
        {
            inMatchUI.SetActive(true);
        }

        #endregion

        #region Option

        public void GoToOption()
        {
            optionUI.SetActive(true);
        }

        public void ShowOptionButton()
        {
            optionButton.gameObject.SetActive(true);
        }

        public void HideOptionButton()
        {
            optionButton.gameObject.SetActive(false);
        }

        #endregion

        #region
        public void ShowChatBox(bool show)
        {
            chatUI.SetActive(show);
        }
        #endregion
    }
}
