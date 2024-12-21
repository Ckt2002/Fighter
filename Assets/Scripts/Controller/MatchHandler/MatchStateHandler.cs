using Controller.Nakama_Controller;
using Data;
using Nakama;
using Nakama.TinyJson;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.MatchHandler
{
    public class MatchStateHandler : MonoBehaviour
    {
        public static MatchStateHandler Instance;

        [SerializeField] private GameController.GameController gameController;
        [SerializeField] private ChatManager chatManager;

        private Dictionary<string, PlayerState> playersStates;
        private string playerId = "";
        private int characterIndex = -1;
        private bool beginBattle;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            playersStates = new Dictionary<string, PlayerState>();
            beginBattle = false;
        }

        public void RegisterMatchHandler(NakamaConnection nakamaConnection)
        {
            if (nakamaConnection.Socket == null)
            {
                Debug.LogError("Nakama socket is null");
                return;
            }

            nakamaConnection.Socket.ReceivedMatchState += OnReceivedMatchState;
            nakamaConnection.Socket.ReceivedChannelMessage += OnReceivedChatMessage;
        }

        private void OnReceivedMatchState(IMatchState matchState)
        {
            switch (matchState.OpCode)
            {
                case OpCodes.SpawnPlayers:
                    ReceiveMatchState.ReceiveSpawnPlayer(matchState, gameController, ref playerId, ref characterIndex);
                    break;
                case OpCodes.PlayerState:
                    ReceiveMatchState.ReceivePlayerState(matchState, gameController, playersStates);
                    break;
                case OpCodes.Ready:
                    ReceiveMatchState.ReceiveReady(matchState, gameController, ref beginBattle);
                    break;
                case OpCodes.Cancel:
                    ReceiveMatchState.ReceiveCancelReady(matchState, gameController, ref beginBattle);
                    break;
                default:
                    Debug.Log("Unsupported op code");
                    break;
            }
        }

        private void OnReceivedChatMessage(IApiChannelMessage message)
        {
            if (!message.SenderId.Equals(gameController.NakamaConnection.PlayerId))
            {
                Dictionary<string, string> messageContent =
                JsonParser.FromJson<Dictionary<string, string>>(message.Content);

                chatManager.GetChatMessage(true, message.Username, messageContent["message"]);
            }
        }

        private void Update()
        {
            MatchSync.SyncSpawnPlayer(ref characterIndex, ref playerId, gameController);
            MatchSync.SyncPlayerStates(playersStates, gameController);
            MatchSync.SyncBeginBattle(ref beginBattle);
        }

        public Dictionary<string, PlayerState> PlayersStates => playersStates;
    }
}
