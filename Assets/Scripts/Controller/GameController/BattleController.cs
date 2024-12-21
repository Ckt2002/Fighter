using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character.Local;
using Controller.Nakama_Controller;
using UI.MatchUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.GameController
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance;
        [SerializeField] private Text notifyText;

        public List<string> diedPlayers;
        public Dictionary<string, int> PlayersScore;

        private void Awake()
        {
            Instance = this;
            diedPlayers = new List<string>();
            PlayersScore = new Dictionary<string, int>();
        }

        public async void CheckPlayersAlive()
        {
            var alivePlayers = GameController.Instance.Players
                .Where(player => !diedPlayers.Contains(player.Key))
                .Select(player => player.Key)
                .ToList();
            if (alivePlayers.Count > 1)
                return;
            Debug.Log(PlayersScore.Count);
            string content;
            switch (alivePlayers.Count)
            {
                case 1:
                    content = $"{GameController.Instance.PlayersName[alivePlayers[0]]} WIN";
                    PlayersScore[alivePlayers[0]] += 1;
                    Debug.Log($"{alivePlayers[0]}: {PlayersScore[alivePlayers[0]]}");
                    break;
                case 0:
                    content = "DRAW";
                    break;
                default:
                    content = "";
                    break;
            }

            await ResultNotification(content);
        }

        private async Task ResultNotification(string content)
        {
            if (content.Equals(""))
                return;

            notifyText.text = content;
            notifyText.gameObject.SetActive(true);

            await Task.Delay(5000);

            notifyText.text = "";
            notifyText.gameObject.SetActive(false);
            ResetMatch();
        }

        private void ResetMatch()
        {
            var players = GameController.Instance.Players;
            foreach (var localReset in diedPlayers.Select(playerId => players[playerId].GetComponent<LocalReset>())
                         .Where(localReset => localReset != null))
            {
                localReset.Reset();
            }

            diedPlayers.Clear();

            if (PlayersScore.Values.Sum() < 3)
                return;
            Debug.Log(PlayersScore.Values.Sum());
            EndBattle();
        }

        private async void EndBattle()
        {
            Debug.Log("End battle");
            var maxScorePlayer = PlayersScore.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            // If local: receive reward
            if (maxScorePlayer.Equals(GameController.Instance.NakamaConnection.PlayerId))
            {
                await WalletNakama.Instance.UpdateWallet(10, "winner reward");
                GameController.Instance.RewardDialog.SetNotifyMessage(10);
                MenuController.Instance.ShowRewardDialog();
            }

            // Reset match
            var keys = PlayersScore.Keys.ToList();
            foreach (var key in keys)
            {
                PlayersScore[key] = 0;
            }

            GameController.Instance.GameStart = false;
            InMatchUI.Instance.backGroundMatchID.SetActive(true);
            InMatchUI.Instance.ResetInMatchReadyButton();
        }
    }
}
