using Controller;
using Controller.GameController;
using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class StoreSlot : MonoBehaviour
    {
        private int itemIndex;
        private Item item;

        [SerializeField] private Image icon;
        [SerializeField] private Text itemName;

        [SerializeField] private GameObject description;
        private readonly List<Text> titles = new List<Text>();
        private readonly List<Text> stats = new List<Text>();

        [SerializeField] private Button button;
        [SerializeField] private Text buttonHeader;

        public void GetAllDescriptionComponents()
        {
            for (int i = 0; i < description.transform.childCount; i++)
            {
                if (i % 2 == 0)
                {
                    titles.Add(description.transform.GetChild(i).GetComponent<Text>());
                    continue;
                }

                stats.Add(description.transform.GetChild(i).GetComponent<Text>());
            }
        }

        public void SetSlotValue(Item item)
        {
            this.item = item;
            icon.sprite = item.icon;
            itemName.text = item.itemName;
            SetDescriptions(item);
            button.interactable = ProfileController.Instance.Money >= item.coins;
            buttonHeader.text = $"Buy {item.coins}$";
        }

        private void SetDescriptions(Item item)
        {
            Dictionary<ItemType, (string, string)> itemTypesTitles = new Dictionary<ItemType, (string, string)>
            {
                { ItemType.Armor, ("Armor", "Health") },
                { ItemType.Shield, ("Armor", "Health") },
                { ItemType.Helmet, ("Armor", "Health") },
                { ItemType.Weapon, ("Damage", "Speed") },
                { ItemType.Boot, ("Health", "Speed") },
            };

            Dictionary<string, int> itemTypesStats = new Dictionary<string, int>
            {
                { "Armor", item.armor },
                { "Health", item.health },
                { "Damage", item.damage },
                { "Speed", item.speed },
            };

            ItemType type = item.itemType;

            if (itemTypesTitles.TryGetValue(type, out var titlesDict))
            {
                string[] titleValues = { titlesDict.Item1, titlesDict.Item2 };

                foreach (var title in titles)
                {
                    title.text = titleValues[titles.IndexOf(title)];

                    if (itemTypesStats.TryGetValue(title.text, out var stat))
                        stats[titles.IndexOf(title)].text = stat.ToString();
                }
            }
        }

        public void BuyItem()
        {
            // Update wallet
            StorageController.Instance.BuyItem(itemIndex, item.coins);

            gameObject.SetActive(false);
        }

        public void ShowSlot()
        {
            transform.gameObject.SetActive(true);
        }

        public void HideSlot()
        {
            transform.gameObject.SetActive(false);
        }

        public int ItemIndex
        {
            set => this.itemIndex = value;
            get => this.itemIndex;
        }
    }
}
