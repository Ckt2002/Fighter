using Controller.GameController;
using UnityEngine;

namespace UI
{
    public class HomeLogout : MonoBehaviour
    {
        public void Logout()
        {
            AccountController.Instance.Logout();
        }
    }
}
