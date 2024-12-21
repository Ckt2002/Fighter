using Controller.GameController;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CreateAccountForm : MonoBehaviour
    {
        [SerializeField] private InputField emailInput;
        [SerializeField] private InputField userNameInput;
        [SerializeField] private InputField passwordInput;

        private AccountController accountController;

        private void Start()
        {
            accountController = AccountController.Instance;
        }

        public void Create()
        {
            string email = emailInput.text;
            string username = userNameInput.text;
            string password = passwordInput.text;

            accountController.CreateAccount(email, username, password);

            ResetForm();
        }

        public void ResetForm()
        {
            emailInput.text = "";
            userNameInput.text = "";
            passwordInput.text = "";
        }
    }
}
