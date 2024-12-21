using Controller.GameController;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MatchUI
{
    public class MatchUI : MonoBehaviour
    {
        [SerializeField] private InputField matchIdInput;

        public void CreateMatch()
        {
            MatchController.Instance.CreateMatch();
        }

        public void JoinMatch()
        {
            if (matchIdInput.text.Equals(""))
            {
                GameController.Instance.Error.SetErrorMessage("You must enter match ID");
                GameController.Instance.Menu.ShowErrDialog();
            }
            else
            {
                MatchController.Instance.JoinMatch(matchIdInput.text);
            }
        }

        private void OnEnable()
        {
            matchIdInput.text = "";
        }

        public void GoToEquipment()
        {
        }
    }
}
