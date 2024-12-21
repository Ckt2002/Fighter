using System.Collections.Generic;
using System.Linq;
using Character.Remote;
using Controller.GameController;
using Data;
using UI.MatchUI;
using UnityEngine;

namespace Controller.MatchHandler
{
    public class MatchSync : MonoBehaviour
    {
        public static void SyncSpawnPlayer(ref int characterIndex, ref string playerId,
            GameController.GameController gameController)
        {
            if (characterIndex <= -1 || playerId.Equals(""))
                return;

            var selectedCharacter =
                CharactersController.Instance.GetRemoteCharacter(characterIndex);
            selectedCharacter.SetActive(true);
            gameController.Players[playerId] = selectedCharacter;
            selectedCharacter.GetComponent<SpriteRenderer>().flipX = false;
            selectedCharacter.transform.position = new Vector3(-6, -1, 0);
            selectedCharacter.GetComponent<RemoteController>().PlayerId = playerId;
            playerId = "";
            characterIndex = -1;
        }

        public static void SyncPlayerStates(Dictionary<string, PlayerState> playersStates,
            GameController.GameController gameController)
        {
            if (playersStates.Count <= 0)
                return;

            foreach (var playerStates in playersStates.ToList())
            {
                var playerId = playerStates.Key;
                if (!gameController.Players.ContainsKey(playerId))
                    continue;
                var character = gameController.Players[playerId];
                character.GetComponent<RemoteController>().PlayerStates = playerStates.Value;
            }
        }

        public static void SyncBeginBattle(ref bool beginBattle)
        {
            if (!beginBattle)
                return;
            InMatchUI.Instance.SetBattleUI();
            MenuController.Instance.HideOptionButton();
            GameController.GameController.Instance.GameStart = true;
            beginBattle = false;
        }
    }
}
