using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    public class RewardDialog : MonoBehaviour
    {
        [SerializeField] private Text notifyMessage;

        public void SetNotifyMessage(int amount)
        {
            notifyMessage.text = $"You received \n{amount} coins";
        }
    }
}
