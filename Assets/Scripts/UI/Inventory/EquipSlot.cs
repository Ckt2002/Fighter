using Controller.GameController;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private new Text name;
    [SerializeField] private Button unequipButton;

    private int itemIndex;
    private Item item;

    private void Awake()
    {
        HideUIComponents();
    }

    public void Equip(Item item, int itemIndex)
    {
        this.item = item;
        unequipButton.gameObject.SetActive(true);
        name.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        name.text = item.name;
        this.itemIndex = itemIndex;
    }

    public void Unequip()
    {
        StorageController.Instance.Equip_UnequipItem("Equip", "Purchased", itemIndex);
        ItemsController.Instance.EquippedItems.Remove(item);
        ResetSlot();
        HideUIComponents();
    }

    public void HideUIComponents()
    {
        name.gameObject.SetActive(false);
        icon.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    public void ResetSlot()
    {
        icon.sprite = null;
        this.item = null;
        name.text = "";
        itemIndex = -1;
    }
}
