using Character.General;
using Controller.Nakama_Controller;
using UI.MatchUI;
using UnityEngine;

namespace Controller.GameController
{
    public class MatchController : MonoBehaviour
    {
        public static MatchController Instance { get; private set; }

        private GameController gameController;

        private MatchNakama match;
        private ChatNakama chat;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            gameController = GameController.Instance;
            match = MatchNakama.Instance;
            chat = ChatNakama.Instance;
        }

        public async void CreateMatch()
        {
            await match.CreateMatch();
            await chat.JoinChat(gameController.NakamaConnection.MatchId);
            gameController.MenuController.GoToInMatch();
            gameController.InMatchUI.SetMatchIdText(gameController.Nakama.Match.Id);
            gameController.MapController.EnableMapAt(0);

            GetLocalCharacter(SelectCharacter.Instance.CharIndex);
        }

        public async void JoinMatch(string matchId)
        {
            if (await match.JoinMatch(matchId))
            {
                await chat.JoinChat(gameController.NakamaConnection.MatchId);
                gameController.MenuController.GoToInMatch();
                gameController.InMatchUI.SetMatchIdText(gameController.Nakama.Match.Id);
                gameController.MapController.EnableMapAt(0);

                GetLocalCharacter(SelectCharacter.Instance.CharIndex);


                foreach (Nakama.IUserPresence player in gameController.NakamaConnection.Match.Presences)
                {
                    gameController.PlayersName.TryAdd(player.UserId, player.Username);
                    BattleController.Instance.PlayersScore.TryAdd(player.UserId, 0);
                    gameController.Players.TryAdd(player.UserId, null);
                }

                SelectCharacter.Instance.SendPlayerSelected();
                return;
            }

            gameController.ErrorDialog.SetErrorMessage(gameController.Nakama.ErrorMessage);
            gameController.MenuController.ShowErrDialog();
            gameController.Nakama.ErrorMessage = "";
        }

        private void GetLocalCharacter(int charIndex)
        {
            GameObject character;
            int armor = 0;
            int health = 0;
            int damage = 0;
            int speed = 0;
            foreach (var item in ItemsController.Instance.EquippedItems)
            {
                armor += item.armor;
                health += item.health;
                damage += item.damage;
                speed += item.speed;
            }

            character =
                CharactersController.Instance.GetLocalCharacter(charIndex);
            character.transform.position = new Vector3(-6, -1, 0);
            character.GetComponent<SpriteRenderer>().flipX = false;
            character.SetActive(true);
            character.GetComponent<CharacterStat>().SetStatOffset(armor, health, damage, speed);
            gameController.Players.TryAdd(gameController.NakamaConnection.PlayerId, character);
            gameController.PlayersName.TryAdd(gameController.NakamaConnection.PlayerId,
                gameController.NakamaConnection.Username);
            BattleController.Instance.PlayersScore.TryAdd(gameController.NakamaConnection.PlayerId, 0);
        }

        public async void LeaveMatch()
        {
            await match.LeaveMatch();
            await chat.LeaveChat();
            gameController.PlayerReady = 0;
            gameController.GameStart = false;
            InMatchUI.Instance.backGroundMatchID.SetActive(true);
            InMatchUI.Instance.ResetInMatchReadyButton();
            MenuController.Instance.ShowOptionButton();
            gameController.MapController.UnableAllMaps();
            gameController.MenuController.GoToMatch();
        }
    }
}
