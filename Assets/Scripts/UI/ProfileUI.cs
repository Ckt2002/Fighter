using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProfileUI : MonoBehaviour
    {
        [SerializeField] private Text moneyText;
        //[SerializeField] private Text playerId;
        [SerializeField] private Text username;
        [SerializeField] private Text email;

        public void SetMoneyText(int money) { moneyText.text = money.ToString(); }

        public void SetProfile(string username, string email)
        {
            this.username.text = username;
            this.email.text = email;
        }
    }
}
