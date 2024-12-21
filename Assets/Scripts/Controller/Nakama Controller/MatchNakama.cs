using Controller.MatchHandler;
using Data;
using Nakama;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controller.Nakama_Controller
{
    public class MatchNakama : MonoBehaviour
    {
        public static MatchNakama Instance;

        private NakamaConnection nakama;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            nakama = NakamaConnection.Instance;
        }

        public async Task<bool> JoinMatch(string matchId)
        {
            Dictionary<string, object> payload = new Dictionary<string, object>
            {
                { "matchId", matchId }
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);

            try
            {
                IApiRpc response = await nakama.Client.RpcAsync(nakama.Session, "getMatchPlayerNumber", jsonPayload);

                if (response.Payload.Equals("match id invalid"))
                {
                    nakama.ErrorMessage = "Match id invalid!";
                    return false;
                }

                PlayerNumberResponse playerNumberResponse = JsonConvert.DeserializeObject<PlayerNumberResponse>(response.Payload);

                if (playerNumberResponse is { PlayerNumber: 2 })
                //playerNumberResponse != null && playerNumberResponse.PlayerNumber == 2
                {
                    nakama.ErrorMessage = "Match is full!";
                    return false;
                }
                else
                {
                    nakama.ErrorMessage = "";
                    nakama.Match = await nakama.Socket.JoinMatchAsync(matchId);
                    nakama.MatchId = matchId;
                    return true;
                }
            }
            catch (ApiResponseException e)
            {
                nakama.ErrorMessage = e.Message;
                return false;
            }
        }

        public async Task CreateMatch()
        {
            try
            {
                nakama.Match = await nakama.Socket.CreateMatchAsync();
                nakama.MatchId = nakama.Match.Id;
            }
            catch (ApiResponseException e)
            {
                Debug.LogError("Failed to create match: " + e.Message);
            }
        }

        public async Task LeaveMatch()
        {
            List<string> keysToRemove = new List<string>();

            MatchStateHandler.Instance.PlayersStates.Clear();
            GameController.GameController.Instance.PlayersName.Clear();
            foreach (KeyValuePair<string, GameObject> player in GameController.GameController.Instance.Players)
            {
                player.Value.SetActive(false);
                player.Value.transform.position = new Vector3(0f, 0f);
                keysToRemove.Add(player.Key);
            }

            foreach (string key in keysToRemove)
            {
                GameController.GameController.Instance.Players.Remove(key);

                if (GameController.GameController.Instance.Players.ContainsKey(key))
                    Debug.Log(GameController.GameController.Instance.Players[key]);
            }

            await nakama.Socket.LeaveMatchAsync(nakama.MatchId);
            nakama.Match = null;
            nakama.MatchId = "";
            GameController.GameController.Instance.MenuController.ShowChatBox(false);
        }
    }
}
