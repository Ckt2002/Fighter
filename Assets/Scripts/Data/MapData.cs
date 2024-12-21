using System.Linq;
using UnityEngine;

namespace Data
{
    public class MapData : MonoBehaviour
    {
        [SerializeField] private GameObject map;

        [SerializeField] private GameObject spawnPosFather;

        [SerializeField] private Transform[] spawnPos;

        private void Awake()
        {
            spawnPos = spawnPosFather.GetComponentsInChildren<Transform>()
                .Where(t => t != spawnPosFather.transform)
                .ToArray();
        }

        public GameObject Map => map;
    }
}
