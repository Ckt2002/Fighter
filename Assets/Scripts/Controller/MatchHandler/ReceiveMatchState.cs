using System.Collections.Generic;
using System.Text;
using Data;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;

namespace Controller.MatchHandler
{
    public class ReceiveMatchState : MonoBehaviour
    {
        public static void ReceiveSpawnPlayer(IMatchState matchState, GameController.GameController gameController,
            ref string playerId, ref int characterIndex)
        {
            var spawnJson = Encoding.UTF8.GetString(matchState.State);
            var spawnPlayerState = spawnJson.FromJson<SpawnPlayerState>();
            if (!gameController.Players.ContainsKey(matchState.UserPresence.UserId))
            {
                gameController.Players.Add(matchState.UserPresence.UserId, null);
            }

            if (gameController.Players[matchState.UserPresence.UserId] != null)
                return;
            playerId = matchState.UserPresence.UserId;
            characterIndex = spawnPlayerState.charIndex;
        }

        public static void ReceivePlayerState(IMatchState matchState, GameController.GameController gameController,
            Dictionary<string, PlayerState> playersStates)
        {
            var playerJson = Encoding.UTF8.GetString(matchState.State);
            var playerState = playerJson.FromJson<PlayerState>();

            if (!gameController.Players.ContainsKey(matchState.UserPresence.UserId))
            {
                Debug.LogError("Can't find player id");
                return;
            }

            var playerId = matchState.UserPresence.UserId;
            if (!playersStates.ContainsKey(playerId))
            {
                playersStates.TryAdd(playerId, playerState);
                return;
            }

            playersStates[playerId] = playerState;
        }

        public static void ReceiveReady(IMatchState matchState, GameController.GameController gameController,
            ref bool beginBattle)
        {
            if (!gameController.Players.ContainsKey(matchState.UserPresence.UserId))
                return;
            gameController.PlayerReady += 1;
            if (gameController.PlayerReady == gameController.Players.Count)
                beginBattle = true;
        }

        public static void ReceiveCancelReady(IMatchState matchState, GameController.GameController gameController,
            ref bool beginBattle)
        {
            if (!gameController.Players.ContainsKey(matchState.UserPresence.UserId))
                return;
            gameController.PlayerReady -= 1;
        }
    }
}
