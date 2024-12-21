using Character.General;
using Character.Local;
using Controller.GameController;
using Controller.Nakama_Controller;
using Nakama;
using System.Collections.Generic;
using UI.MatchUI;
using UnityEngine;

namespace Controller.MatchHandler
{
    public class MatchPresenceHandler : MonoBehaviour
    {
        public static MatchPresenceHandler Instance;

        [SerializeField] private GameController.GameController gameController;
        private readonly Queue<string> playersLeave = new Queue<string>();
        private bool sendSelected;
        private bool checkLeaving;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            checkLeaving = false;
            sendSelected = false;
        }

        public void RegisterMatchHandler(NakamaConnection nakamaConnection)
        {
            if (nakamaConnection == null)
            {
                Debug.LogError("nakama is null");
                return;
            }

            nakamaConnection.Socket.ReceivedMatchPresence += OnReceivedMatchPresence;
        }

        private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresences)
        {
            foreach (var playerJoin in matchPresences.Joins)
            {
                if (playerJoin.UserId.Equals(gameController.NakamaConnection.PlayerId))
                    continue;
                gameController.PlayersName.TryAdd(playerJoin.UserId, playerJoin.Username);
                gameController.Players.TryAdd(playerJoin.UserId, null);
                BattleController.Instance.PlayersScore.TryAdd(playerJoin.UserId, 0);
                sendSelected = true;
            }

            foreach (var playerLeave in matchPresences.Leaves)
            {
                checkLeaving = true;

                if (gameController.Players.ContainsKey(playerLeave.UserId))
                    playersLeave.Enqueue(playerLeave.UserId);
                if (MatchStateHandler.Instance.PlayersStates.ContainsKey(playerLeave.UserId))
                    MatchStateHandler.Instance.PlayersStates.Remove(playerLeave.UserId);
            }
        }

        private void Update()
        {
            while (playersLeave.Count > 0)
            {

                var id = playersLeave.Dequeue();
                if (id.Equals(gameController.NakamaConnection.PlayerId))
                {
                    gameController.Players[id].GetComponent<LocalReset>().Reset();
                    gameController.Players[id].GetComponent<CharacterStat>().ResetStat();
                }
                gameController.Players[id].SetActive(false);
                gameController.Players.Remove(id);
                gameController.PlayersName.Remove(id);
                BattleController.Instance.PlayersScore.Remove(id);
            }

            if (!checkLeaving)
                return;
            if (gameController.Players.ContainsKey(gameController.NakamaConnection.PlayerId))
                gameController.Players[gameController.NakamaConnection.PlayerId].GetComponent<LocalReset>().Reset();
            InMatchUI.Instance.ResetInMatchReadyButton();
            gameController.GameStart = false;
            InMatchUI.Instance.backGroundMatchID.SetActive(true);
            gameController.PlayerReady = 0;
            checkLeaving = false;
        }

        private void FixedUpdate()
        {
            if (!sendSelected)
                return;

            SelectCharacter.Instance.SendPlayerSelected();
            sendSelected = false;
        }
    }
}
