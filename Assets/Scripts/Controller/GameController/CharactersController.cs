using System.Collections.Generic;
using UnityEngine;

namespace Controller.GameController
{
    public class CharactersController : MonoBehaviour
    {
        public static CharactersController Instance;

        [SerializeField] private List<GameObject> localCharacters;
        [SerializeField] private List<GameObject> remoteCharacters;

        [SerializeField] private List<GameObject> spawnedLocal = new List<GameObject>();
        [SerializeField] private List<GameObject> spawnedHeroRemote = new List<GameObject>();
        [SerializeField] private List<GameObject> spawnedBanditRemote = new List<GameObject>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            SpawnLocal();

            SpawnRemote(remoteCharacters[0], spawnedHeroRemote, 3);

            SpawnRemote(remoteCharacters[1], spawnedBanditRemote, 3);
        }

        private void SpawnLocal()
        {
            foreach (var localChar in localCharacters)
            {
                GameObject character = Instantiate(localChar, new Vector3(-6, -1, 0), Quaternion.identity);
                character.SetActive(false);
                spawnedLocal.Add(character);
            }
        }

        private void SpawnRemote(GameObject characterPrefab, List<GameObject> spawnedList, int spawnNumber)
        {
            for (int i = 0; i < spawnNumber; i++)
            {
                GameObject character = Instantiate(characterPrefab, new Vector3(-6, -1, 0), Quaternion.identity);
                character.SetActive(false);
                spawnedList.Add(character);
            }
        }

        public GameObject GetLocalCharacter(int characterIndex)
        {
            return spawnedLocal[characterIndex];
        }

        public GameObject GetRemoteCharacter(int characterIndex)
        {
            List<GameObject> characterList = characterIndex == 0 ? spawnedHeroRemote : spawnedBanditRemote;

            foreach (var character in characterList)
            {
                if (!character.activeSelf)
                {
                    return character;
                }
            }

            return null;
        }

        //public void RemoveSelectedCharacter()
        //{
        //    selectedLocalCharacter = null;
        //}
    }
}
