using System.Threading.Tasks;
using Data;
using Nakama;
using UnityEngine;

namespace Controller.Nakama_Controller
{
    public class DailyRewardNakama : MonoBehaviour
    {
        public static DailyRewardNakama Instance;

        private NakamaConnection nakama;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            nakama = NakamaConnection.Instance;
        }

        public async Task CreateDailyRewardStorage()
        {
            var writeStorage = new WriteStorageObject
            {
                Collection = "reward",
                Key = "daily",
                Value = "{\"last_claim\":0}",
                PermissionRead = 1,
                PermissionWrite = 0,
            };

            await nakama.Client.WriteStorageObjectsAsync(nakama.Session, new[] { writeStorage });
        }

        public async Task<int> GetDailyRewardNotification()
        {
            try
            {
                var response = await nakama.Client.RpcAsync(nakama.Session, "dailyRewards", "");
                var rewardResponse = JsonUtility.FromJson<DailyRewardResponse>(response.Payload);
                return rewardResponse.coins_received;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error notify daily reward: " + ex.Message);
                return -1;
            }
        }
    }
}
