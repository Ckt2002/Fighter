using Controller.GameController;
using Controller.MatchHandler;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MatchUI
{
    public class InMatchUI : MonoBehaviour
    {
        public static InMatchUI Instance;

        [SerializeField] public GameObject backGroundMatchID;
        [SerializeField] private Text matchIdText;
        [SerializeField] private Button copyIdButton;

        [SerializeField] private Button readyButton;
        [SerializeField] private Button cancelButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            copyIdButton.onClick.AddListener(CopyText);
        }

        private void Update()
        {
            readyButton.interactable = GameController.Instance.Players.Count > 1;
        }

        private void CopyText()
        {
            var textToCopy = matchIdText.text;
            GUIUtility.systemCopyBuffer = textToCopy;
        }

        public void SetMatchIdText(string matchId)
        {
            matchIdText.text = matchId;
        }

        public void Ready()
        {
            var ready = new ReadyState
            {
                ready = true,
            };
            SendMatchState.Instance.SendState(OpCodes.Ready, ready);

            GameController.Instance.PlayerReady += 1;

            CheckReady();
        }

        public void Cancel()
        {
            var ready = new ReadyState
            {
                ready = false,
            };
            SendMatchState.Instance.SendState(OpCodes.Cancel, ready);

            GameController.Instance.PlayerReady -= 1;

            ResetInMatchReadyButton();
        }

        private void CheckReady()
        {
            if (GameController.Instance.PlayerReady == GameController.Instance.Players.Count)
            {
                SetBattleUI();
                GameController.Instance.GameStart = true;
                return;
            }

            cancelButton.gameObject.SetActive(true);
            readyButton.gameObject.SetActive(false);
        }

        public void SetBattleUI()
        {
            backGroundMatchID.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            readyButton.gameObject.SetActive(false);
        }

        public void ResetInMatchReadyButton()
        {
            cancelButton.gameObject.SetActive(false);
            readyButton.gameObject.SetActive(true);
        }
    }
}
