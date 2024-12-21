using Controller.GameController;
using Data;
using System.Collections.Generic;
using UnityEngine;

public class EquipUI : MonoBehaviour
{
    public static EquipUI Instance;

    [SerializeField] private List<EquipSlot> slots;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.ResetSlot();
            slot.HideUIComponents();
        }
    }

    public void Equip(Item item, int itemIndex)
    {
        int slotIndex = 0;
        switch (item.itemType)
        {
            case ItemType.Helmet:
                //Debug.Log("Helmet");
                slotIndex = 0;
                break;
            case ItemType.Armor:
                //Debug.Log("Armor");
                slotIndex = 1;
                break;
            case ItemType.Weapon:
                //Debug.Log("Weapon");
                slotIndex = 2;
                break;
            case ItemType.Shield:
                //Debug.Log("Shield");
                slotIndex = 3;
                break;
            case ItemType.Boot:
                Debug.Log("Boot");
                slotIndex = 4;
                break;
        }

        // UpdateUI
        slots[slotIndex].Equip(item, itemIndex);

        // Update database
        StorageController.Instance.Equip_UnequipItem("Purchased", "Equip", itemIndex);

        // Update list
        ItemsController.Instance.EquippedItems.Add(item);
    }
}
