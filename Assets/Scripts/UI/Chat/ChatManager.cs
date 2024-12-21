using Controller.GameController;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private ChatMessage localMessagePrefab;
    [SerializeField] private ChatMessage remoteMessagePrefab;
    [SerializeField] private RectTransform chatContent;
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private GameController gameController;

    private bool newMessage = false;
    private string userSend = "";
    private string messageContent = "";

    private void Update()
    {
        if (gameController.NakamaConnection.MatchId.Equals("") || gameController.NakamaConnection.ChatChannel == null)
            return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameController.MenuController.ShowChatBox(true);

            SendMessage();
        }

        if (newMessage)
        {

            SetRemoteChat(userSend, messageContent);
            newMessage = false;
            userSend = messageContent = "";
        }
    }

    private async void SendMessage()
    {
        if (!chatInput.text.Equals(""))
        {
            string channelId = gameController.NakamaConnection.ChatChannel.Id;

            Dictionary<string, string> messageContent = new Dictionary<string, string>
            {
                { "message", chatInput.text }
            };

            Nakama.IChannelMessageAck messageSendAck =
                await gameController.NakamaConnection.Socket.WriteChatMessageAsync(
                    channelId, Nakama.TinyJson.JsonWriter.ToJson(messageContent));

            SetLocalChat();
        }
    }

    private void AddMessage(ChatMessage messagePrefab, string username, string content)
    {
        ChatMessage newMessage = Instantiate(messagePrefab, chatContent);

        newMessage.SetContent(username, content);
    }

    private void SetLocalChat()
    {
        string localUsername = gameController.NakamaConnection.Username;
        string messageContent = chatInput.text;

        AddMessage(localMessagePrefab, localUsername, messageContent);

        chatInput.text = string.Empty;
    }

    public void GetChatMessage(bool newMessage, string username, string message)
    {
        this.newMessage = newMessage;
        userSend = username;
        messageContent = message;
    }

    public void SetRemoteChat(string username, string message)
    {
        gameController.MenuController.ShowChatBox(true);
        AddMessage(remoteMessagePrefab, username, message);
    }
}
