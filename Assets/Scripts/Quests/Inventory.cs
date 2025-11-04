using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Quest
{
    public class Inventory : MonoBehaviour
    {
        public List<QuestItemSO> items = new List<QuestItemSO>();

        void OnEnable()
        {
            EventManager.OnChestInteract += HandleTreasureChestUnlocked;
        }

        void OnDisable()
        {
            EventManager.OnChestInteract -= HandleTreasureChestUnlocked;
        }

        public void HandleTreasureChestUnlocked(QuestItemSO newItem, bool showUI)
        {
            items.Add(newItem);
        }

        public bool HasItem(QuestItemSO desiredItem)
        {
            bool itemFound = false;

            items.ForEach((QuestItemSO item) =>
            {
                if (desiredItem.name == item.name) itemFound = true;
            });

            return itemFound;
        }
    }
}

