using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    public class ErrorDialog : MonoBehaviour
    {
        [SerializeField] private Text errorMessage;

        public void SetErrorMessage(string message)
        {
            errorMessage.text = message;
        }
    }
}
