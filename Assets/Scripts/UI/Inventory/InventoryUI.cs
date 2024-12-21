using Data;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject backGround;

    [SerializeField] private GameObject equipContent;
    [SerializeField] private GameObject content;

    [SerializeField] private InventorySlot[] slots;

    private void Awake()
    {
        backGround.SetActive(true);
        slots = content.transform.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slots)
        {
            slot.HideSlot();
        }

        backGround.SetActive(false);
    }

    public void ResetAllSlots()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.HideSlot();
        }
    }

    public void SetItemSlot(Item item, int slotIndex, int itemIndex)
    {
        slots[slotIndex].ItemIndex = itemIndex;
        slots[slotIndex].ShowSlot();
        slots[slotIndex].GetAllDescriptionComponents();
        slots[slotIndex].SetSlotValue(item);
    }
}
