using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        private Canvas canvasCmp;
        private Reward rewardCmp;
        public TextAsset inkJSON;
        public QuestItemSO desiredQuestItem;
        public bool hasQuestItem = false;

        void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
            rewardCmp = GetComponentInChildren<Reward>();
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("NPCItems"))
            {
                List<string> npcItems = PlayerPrefsUtility.GetString("NPCItems");
                npcItems.ForEach(CheckNPCQuestItem);
            }
        }

        void OnTriggerEnter()
        {
            canvasCmp.enabled = true;
        }

        void OnTriggerExit()
        {
            canvasCmp.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasCmp.enabled) return;
            if (inkJSON == null)
            {
                Debug.Log("Please add an ink file to the NPC.");
            }

            EventManager.RaiseInteractNPC(inkJSON, gameObject);
        }

        public bool CheckPlayerForQuestItem()
        {
            if (hasQuestItem) return true;

            Inventory inventoryCmp = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<Inventory>();

            hasQuestItem = inventoryCmp.HasItem(desiredQuestItem);

            if (rewardCmp != null && hasQuestItem)
            {
                rewardCmp.SendReward();
            }

            return hasQuestItem;
        }

        private void CheckNPCQuestItem(string itemName)
        {
            if (itemName == desiredQuestItem.itemName)
            {
                hasQuestItem = true;
            }
        }
    }
}
