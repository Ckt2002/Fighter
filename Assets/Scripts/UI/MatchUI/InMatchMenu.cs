using Controller.GameController;
using UnityEngine;

namespace UI.MatchUI
{
    public class InMatchMenu : MonoBehaviour
    {
        public void LeaveMatch()
        {
            MatchController.Instance.LeaveMatch();
        }
    }
}
