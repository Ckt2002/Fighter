using Data;
using UnityEngine;

namespace Controller
{
    public class MapsController : MonoBehaviour
    {
        [SerializeField] private MapData[] maps;

        public void EnableMapAt(int index)
        {
            maps[index].Map.SetActive(true);
        }

        public void UnableMapAt(int index)
        {
            maps[index].Map.SetActive(false);
        }

        public void UnableAllMaps()
        {
            for (int i = 0; i < maps.Length; i++)
            {
                maps[i].Map.SetActive(false);
            }
        }
    }
}
