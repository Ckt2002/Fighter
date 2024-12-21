using Controller.GameController;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoginForm : MonoBehaviour
    {
        [SerializeField] private InputField emailInput;
        [SerializeField] private InputField passwordInput;

        private AccountController accountController;

        private void Start()
        {
            accountController = AccountController.Instance;
        }

        public void Login()
        {
            var email = emailInput.text;
            var password = passwordInput.text;

            accountController.LoginAccount(email, password);

            ResetForm();
        }

        public void ResetForm()
        {
            emailInput.text = "";
            passwordInput.text = "";
        }
    }
}
