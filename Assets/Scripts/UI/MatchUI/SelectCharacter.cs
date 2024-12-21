using Data;
using System.Collections.Generic;
using Controller.MatchHandler;
using UnityEngine;

namespace UI.MatchUI
{
    public class SelectCharacter : MonoBehaviour
    {
        public static SelectCharacter Instance;

        [SerializeField] private List<CharacterRawImage> characters;
        // [SerializeField] private Button nextButton;
        // [SerializeField] private Button prevButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CharIndex = 0;
        }

        public void SelectNextCharacter()
        {
            characters[CharIndex].gameObject.SetActive(false);

            CharIndex = (CharIndex == characters.Count - 1) ? 0 : (CharIndex + 1);

            characters[CharIndex].gameObject.SetActive(true);
        }

        public void SelectPrevCharacter()
        {
            characters[CharIndex].gameObject.SetActive(false);

            CharIndex = (CharIndex == 0) ? (characters.Count - 1) : (CharIndex - 1);

            characters[CharIndex].gameObject.SetActive(true);
        }

        public void SendPlayerSelected()
        {
            var character = new SpawnPlayerState
            {
                charIndex = CharIndex
            };
            SendMatchState.Instance.SendState(OpCodes.SpawnPlayers, character);
        }

        #region Getter Setter

        public int CharIndex { private set; get; }

        #endregion
    }
}
