using Nakama;
using System.Threading.Tasks;
using UnityEngine;

namespace Controller.Nakama_Controller
{
    public class NakamaConnection : MonoBehaviour
    {
        public static NakamaConnection Instance;

        private const string Scheme = "http";
        private const string Host = "127.0.0.1";
        private const int Port = 7350;
        private const string ServerKey = "defaultkey";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void InitializeClient()
        {
            Client ??= new Client(Scheme, Host, Port, ServerKey, UnityWebRequestAdapter.Instance);
        }

        public async Task ConnectSocketAsync()
        {
            Socket ??= Client.NewSocket();

            await Socket.ConnectAsync(Session, true, 30);
        }

        #region Getter Setter

        public IClient Client { private set; get; }

        public ISession Session { set; get; }

        public ISocket Socket { private set; get; }

        public IMatch Match { set; get; }

        public IChannel ChatChannel { set; get; }

        public IApiAccount Account { set; get; }

        public string PlayerId { set; get; } = "";

        public string Email { set; get; } = "";

        public string Username { set; get; } = "";

        public string ErrorMessage { set; get; } = "";

        public string MatchId { set; get; } = "";

        #endregion
    }
}
