using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.GameController
{
    public class ItemsController : MonoBehaviour
    {
        public static ItemsController Instance { get; private set; }

        [SerializeField] private List<Item> items;
        private List<Item> equippedItems;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            equippedItems = new();
        }

        public List<Item> Items => items;

        public List<Item> EquippedItems => equippedItems;
    }
}
