using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;
using System.Collections.Generic;

namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {

        [SerializeField] private QuestItemSO questItem;
        public Animator animatorCmp;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;

        void Start()
        {
            if (PlayerPrefs.HasKey("PlayerItems"))
            {
                List<string> playerItems = PlayerPrefsUtility.GetString("PlayerItems");

                playerItems.ForEach(CheckItem);
            }
        }

        private void OnTriggerEnter()
        {
            isInteractable = true;
        }

        private void OnTriggerExit()
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened || !context.performed) return;

            animatorCmp.SetBool(Constants.TRESURE_CHEST_SHAKE_ANIMATION_PARAM, false);
            hasBeenOpened = true;

            EventManager.RaiseChestInteract(questItem, true);
        }

        private void CheckItem(string itemName)
        {
            if (itemName != questItem.name) return;

            hasBeenOpened = true;
            animatorCmp.SetBool(Constants.TRESURE_CHEST_SHAKE_ANIMATION_PARAM, false);

            EventManager.RaiseChestInteract(questItem, false);
        }
    }
}

