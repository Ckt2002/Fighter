using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama.TinyJson;
using Newtonsoft.Json;
using UnityEngine;

namespace Controller.Nakama_Controller
{
    public class WalletNakama : MonoBehaviour
    {
        public static WalletNakama Instance;

        private NakamaConnection nakama;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            nakama = NakamaConnection.Instance;
        }

        public async Task UpdateWallet(int amount, string action)
        {
            var payload = new Dictionary<string, object>
            {
                { "coin", amount },
                { "action", action }
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            try
            {
                // Gọi API Golang
                var response = await nakama.Client.RpcAsync(nakama.Session, "updateWallet", jsonPayload);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error updating wallet: " + ex.Message);
            }
        }

        public async Task<int> GetUserWallet()
        {
            var account = await nakama.Client.GetAccountAsync(nakama.Session);
            var wallet = JsonParser.FromJson<Dictionary<string, int>>(account.Wallet);
            return wallet.ContainsKey("coins") ? wallet["coins"] : -1;
        }
    }
}
