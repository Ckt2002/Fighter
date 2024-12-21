using Controller.Nakama_Controller;
using Nakama;
using System.Threading.Tasks;
using UnityEngine;

public class ChatNakama : MonoBehaviour
{
    public static ChatNakama Instance;

    private NakamaConnection nakama;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        nakama = NakamaConnection.Instance;
    }

    public async Task JoinChat(string matchId)
    {
        bool persistence = false;
        bool hidden = false;
        IChannel channel = await nakama.Socket.JoinChatAsync(matchId, ChannelType.Room, persistence, hidden);
        nakama.ChatChannel = channel;
    }

    public async Task LeaveChat()
    {
        string channelId = nakama.ChatChannel.Id;
        await nakama.Socket.LeaveChatAsync(channelId);
        nakama.ChatChannel = null;
    }
}
