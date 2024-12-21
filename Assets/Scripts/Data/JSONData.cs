using Newtonsoft.Json;

namespace Data
{
    [System.Serializable]
    public class CoinsResponse
    {
        public int coinsReceived;
    }

    [System.Serializable]
    public class DailyRewardResponse
    {
        // ReSharper disable once InconsistentNaming
        public int coins_received;
    }

    [System.Serializable]
    public class PlayerNumberResponse
    {
        [JsonProperty("playerNumber")] public int PlayerNumber { get; set; }
    }
}
