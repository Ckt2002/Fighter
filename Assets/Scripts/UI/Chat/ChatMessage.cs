using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text messageContent;

    public void SetContent(string name, string content)
    {
        messageContent.text = $"{name}: {content}";
    }
}
