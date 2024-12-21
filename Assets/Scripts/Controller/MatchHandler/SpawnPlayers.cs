using Controller.GameController;
using UnityEngine;

namespace Controller.MatchHandler
{
    public class SpawnPlayers : MonoBehaviour
    {
        public static SpawnPlayers Instance;

        private void Awake()
        {
            Instance = this;
        }

        public GameObject Spawn(int charIndex, bool isLocal)
        {
            GameObject character;
            character = isLocal
                ? CharactersController.Instance.GetLocalCharacter(charIndex)
                : CharactersController.Instance.GetRemoteCharacter(charIndex);

            return Instantiate(character, new Vector3(-6, -1, 0), Quaternion.identity);
        }
    }
}
