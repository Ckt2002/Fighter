using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class StoreUI : MonoBehaviour
    {
        [SerializeField] private Text moneyText;

        [SerializeField] private GameObject backGround;

        [SerializeField] private GameObject content;

        [SerializeField] private StoreSlot[] slots;

        private void Awake()
        {
            backGround.SetActive(true);
            slots = content.transform.GetComponentsInChildren<StoreSlot>();
            foreach (StoreSlot slot in slots)
            {
                slot.HideSlot();
            }

            backGround.SetActive(false);
        }

        public void SetMoneyText(int money)
        {
            moneyText.text = money.ToString();
        }

        public void ResetAllSlots()
        {
            foreach (StoreSlot slot in slots)
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
}
