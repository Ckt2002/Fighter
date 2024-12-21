using Controller.Nakama_Controller;
using Nakama.TinyJson;
using System;
using System.Net.WebSockets;
using UnityEngine;

namespace Controller.MatchHandler
{
    public class SendMatchState : MonoBehaviour
    {
        public static SendMatchState Instance;

        [SerializeField] private NakamaConnection nakamaConnection;

        private void Awake()
        {
            Instance = this;
        }

        public async void SendState(long opCode, object state)
        {
            try
            {
                await nakamaConnection.Socket.SendMatchStateAsync(nakamaConnection.Match.Id, opCode,
                    state.ToJson());
            }
            catch (WebSocketException ex)
            {
                Debug.LogError("WebSocketException: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.LogError("ArgumentOutOfRangeException: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected exception: " + ex.Message);
            }
        }
    }
}
