using Controller.Nakama_Controller;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.GameController
{
    public class StorageController : MonoBehaviour
    {
        public static StorageController Instance { get; private set; }

        private GameController controller;
        private WalletController walletController;
        private ItemsController itemsController;

        private StorageNakama storage;
        private WalletNakama wallet;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            controller = GameController.Instance;
            walletController = WalletController.Instance;
            itemsController = ItemsController.Instance;

            storage = StorageNakama.Instance;
            wallet = WalletNakama.Instance;
        }

        public async void AddStorageItem(int index)
        {
            await storage.StorageItem("Store", index);
        }

        public async void GetItemsForStore(List<Item> items)
        {
            controller.StoreUI.ResetAllSlots();

            int slotIndex = 0;
            foreach (Item item in items)
            {
                if (await storage.GetItem("Store", items.IndexOf(item)))
                {
                    controller.StoreUI.SetItemSlot(item, slotIndex, items.IndexOf(item));
                    slotIndex++;
                }
            }
        }

        public async void GetItemsForInventory(List<Item> items)
        {
            controller.InventoryUI.ResetAllSlots();

            int slotIndex = 0;
            foreach (Item item in items)
            {
                if (await storage.GetItem("Purchased", items.IndexOf(item)))
                {
                    controller.InventoryUI.SetItemSlot(item, slotIndex, items.IndexOf(item));
                    slotIndex++;
                }
            }
        }

        public async void GetEquipedItemsForInventory(List<Item> items)
        {
            controller.EquipUI.ResetAllSlots();
            ItemsController.Instance.EquippedItems.Clear();
            int slotIndex = 0;
            foreach (Item item in items)
            {
                if (await storage.GetItem("Equip", items.IndexOf(item)))
                {
                    ItemsController.Instance.EquippedItems.Add(item);
                    controller.EquipUI.Equip(item, items.IndexOf(item));
                    slotIndex++;
                }
            }
        }

        public async void BuyItem(int itemIndex, int amount)
        {
            storage.DeleteItem("Store", itemIndex.ToString());

            await storage.StorageItem("Purchased", itemIndex);

            await wallet.UpdateWallet(-amount, $"Purchase Item {itemIndex}");

            walletController.GetCoinWallet();

            GetItemsForStore(itemsController.Items);

            GetItemsForInventory(itemsController.Items);
        }

        public async void Equip_UnequipItem(string fromStorage, string toStorage, int itemIndex)
        {
            storage.DeleteItem(fromStorage, itemIndex.ToString());

            await storage.StorageItem(toStorage, itemIndex);
            GetItemsForInventory(itemsController.Items);
        }
    }
}
