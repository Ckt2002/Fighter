using System.Threading.Tasks;
using UnityEngine;

namespace Controller.Nakama_Controller
{
    public class AccountNakama : MonoBehaviour
    {
        public static AccountNakama Instance;

        private NakamaConnection nakama;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            nakama = NakamaConnection.Instance;
        }

        public async Task AccountCreate(string emailInput, string passwordInput, string usernameInput)
        {
            try
            {
                nakama.Session =
                    await nakama.Client.AuthenticateEmailAsync(emailInput, passwordInput, usernameInput, true);

                nakama.Account =
                    await nakama.Client.GetAccountAsync(nakama.Session);

                nakama.PlayerId = nakama.Account.User.Id;
                nakama.Email = nakama.Account.Email;
                nakama.Username = nakama.Account.User.Username;

                await nakama.ConnectSocketAsync();
            }
            catch (System.Exception e)
            {
                nakama.ErrorMessage = e.Message;
                Debug.LogError("Error creating account: " + e.Message);
            }
        }

        public async Task AccountLogin(string emailInput, string passwordInput)
        {
            try
            {
                nakama.Session = await nakama.Client.AuthenticateEmailAsync(emailInput, passwordInput, null, false);
                nakama.Account = await nakama.Client.GetAccountAsync(nakama.Session);

                PlayerPrefs.SetString("nakamaSessionToken", nakama.Session.AuthToken);
                PlayerPrefs.Save();

                nakama.PlayerId = nakama.Account.User.Id;
                nakama.Email = nakama.Account.Email;
                nakama.Username = nakama.Account.User.Username;

                await nakama.ConnectSocketAsync();
            }
            catch (System.Exception e)
            {
                nakama.ErrorMessage = e.Message;
                Debug.LogError("Error creating account: " + e.Message);
            }
        }

        public async Task AccountLogout()
        {
            try
            {
                if (nakama.Socket != null)
                {
                    await nakama.Socket.CloseAsync();
                }

                if (nakama.Session != null)
                {
                    await nakama.Client.SessionLogoutAsync(nakama.Session);
                }

                nakama.Session = null;
                nakama.PlayerId = "";
                nakama.Email = "";
                nakama.Username = "";
                nakama.Account = null;

                PlayerPrefs.DeleteKey("nakamaSessionToken");
            }
            catch (System.Exception e)
            {
                nakama.ErrorMessage = e.Message;
                Debug.LogError("Error logging out: " + e.Message);
            }
        }
    }
}
