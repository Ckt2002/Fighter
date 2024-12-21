using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nakama;
using Newtonsoft.Json;
using UnityEngine;

namespace Controller.Nakama_Controller
{
    public class StorageNakama : MonoBehaviour
    {
        public static StorageNakama Instance;

        private NakamaConnection nakama;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            nakama = NakamaConnection.Instance;
        }

        public async Task StorageItem(string storageName, int itemIndex)
        {
            var jsonData = JsonConvert.SerializeObject(new Dictionary<string, object> { });

            var storageObjectWrite = new WriteStorageObject
            {
                Collection = storageName,
                Key = itemIndex.ToString(),
                Value = jsonData,
                PermissionRead = 1,
                PermissionWrite = 1,
            };

            await nakama.Client.WriteStorageObjectsAsync(nakama.Session, new[] { storageObjectWrite });
        }

        public async Task<bool> GetItem(string storageName, int itemIndex)
        {
            var readObjectId = new StorageObjectId
            {
                Collection = storageName,
                Key = itemIndex.ToString(),
                UserId = nakama.Session.UserId
            };

            var result = await nakama.Client.ReadStorageObjectsAsync(nakama.Session, new[] { readObjectId });

            return result.Objects.Any();
        }

        public async void DeleteItem(string collectionsName, string keyName)
        {
            var payload = new Dictionary<string, object>
            {
                { "collectionsName", collectionsName },
                { "keyName", keyName }
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            try
            {
                // Gọi API Golang
                var response = await nakama.Client.RpcAsync(nakama.Session, "deleteItem", jsonPayload);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error deleting item: " + ex.Message);
            }
        }
    }
}
