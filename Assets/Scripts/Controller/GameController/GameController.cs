using Controller.MatchHandler;
using Controller.Nakama_Controller;
using System.Collections.Generic;
using UI;
using UI.Dialogs;
using UI.MatchUI;
using UI.Store;
using UnityEngine;

namespace Controller.GameController
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [Header("Nakama")][SerializeField] private NakamaConnection nakamaConnection;

        [Header("Controller")]
        [SerializeField]
        private MapsController mapController;

        [SerializeField] private MenuController menuController;
        [SerializeField] private AudioController audioController;
        [SerializeField] private ProfileController profileController;

        [Header("UI")][SerializeField] private ErrorDialog errorDialog;
        [SerializeField] private StoreUI storeUI;
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private EquipUI equipUI;
        [SerializeField] private ProfileUI profileUI;
        [SerializeField] private MatchUI matchUI;
        [SerializeField] private InMatchUI inMatchUI;
        [SerializeField] private RewardDialog rewardDialog;

        private Dictionary<string, GameObject> players;
        private Dictionary<string, string> playersName;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            playersName = new Dictionary<string, string>();
            players = new Dictionary<string, GameObject>();
        }

        private void Start()
        {
            nakamaConnection.InitializeClient();
            audioController.PlayMainMenuBackGround();
        }

        public void RegisterMatchHandlers()
        {
            MatchStateHandler.Instance.RegisterMatchHandler(nakamaConnection);
            MatchPresenceHandler.Instance.RegisterMatchHandler(nakamaConnection);
        }

        #region Getter Setter

        public ErrorDialog Error => errorDialog;

        public MenuController Menu => menuController;

        public NakamaConnection Nakama => nakamaConnection;

        public NakamaConnection NakamaConnection => nakamaConnection;

        public MapsController MapController => mapController;

        public MenuController MenuController => menuController;

        public AudioController AudioController => audioController;

        public ProfileController ProfileController => profileController;

        public ErrorDialog ErrorDialog => errorDialog;

        public StoreUI StoreUI
        {
            get => storeUI;
            set => storeUI = value;
        }

        public InventoryUI InventoryUI
        {
            get => inventoryUI;
            set => inventoryUI = value;
        }

        public EquipUI EquipUI
        {
            get => equipUI;
            set => equipUI = value;
        }

        public ProfileUI ProfileUI => profileUI;

        public MatchUI MatchUI => matchUI;

        public InMatchUI InMatchUI => inMatchUI;

        public RewardDialog RewardDialog => rewardDialog;

        public Dictionary<string, GameObject> Players => players;
        public Dictionary<string, string> PlayersName => playersName;

        public int PlayerReady { get; set; } = 0;

        public bool GameStart { get; set; } = false;

        #endregion
    }
}
