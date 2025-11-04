using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        private VisualElement questItemIcon;
        public VisualElement root;
        public VisualElement mainMenuContainer;
        public VisualElement playerInfoContainer;
        public Label healthLabel;
        public Label potionsLabel;

        public List<Button> buttons = new List<Button>();
        public UIBaseState currentState;
        public UIMainMenuState mainMenuState;
        public UIDialogueState dialogueState;
        public UIQuestItemState questItemState;
        public int currentSelection = 0;

        void Awake()
        {
            uiDocumentCmp = GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;

            mainMenuContainer = root.Q<VisualElement>("main-menu-container");
            playerInfoContainer = root.Q<VisualElement>("player-info-container");
            healthLabel = playerInfoContainer.Q<Label>("health-label");
            potionsLabel = playerInfoContainer.Q<Label>("potions-label");
            questItemIcon = playerInfoContainer.Q<VisualElement>("quest-item-icon");

            mainMenuState = new UIMainMenuState(this);
            dialogueState = new UIDialogueState(this);
            questItemState = new UIQuestItemState(this);

        }
        void OnEnable()
        {
            EventManager.OnChangePlayerHealth += HandleChangePlayerhealth;
            EventManager.OnChangePlayerPotions += HandleChangePlayerPotions;
            EventManager.OnInteractNPC += HandleInteractNPC;
            EventManager.OnChestInteract += HandleInteractChest;
        }

        void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandleChangePlayerhealth;
            EventManager.OnChangePlayerPotions -= HandleChangePlayerPotions;
            EventManager.OnInteractNPC -= HandleInteractNPC;
            EventManager.OnChestInteract -= HandleInteractChest;
        }

        void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == 0)
            {
                currentState = mainMenuState;
                currentState.EnterState();
            }
            else
            {
                playerInfoContainer.style.display = DisplayStyle.Flex;
            }
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            currentState.SelectedButton();
        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed || buttons.Count == 0) return;

            buttons[currentSelection].RemoveFromClassList("active");

            Vector2 input = context.ReadValue<Vector2>();

            currentSelection += input.x > 0 ? 1 : -1;

            currentSelection = Mathf.Clamp(currentSelection, 0, buttons.Count - 1);
            buttons[currentSelection].AddToClassList("active");
        }

        private void HandleChangePlayerhealth(float newHealthPoints)
        {
            healthLabel.text = newHealthPoints.ToString();
        }

        private void HandleChangePlayerPotions(int newPotionCount)
        {
            potionsLabel.text = newPotionCount.ToString();
        }

        private void HandleInteractNPC(TextAsset inkJSON, GameObject npc)
        {
            currentState = dialogueState;
            currentState.EnterState();
            (currentState as UIDialogueState).SetStory(inkJSON, npc);
        }
        private void HandleInteractChest(QuestItemSO item, bool showUI)
        {
            questItemIcon.style.display = DisplayStyle.Flex;

            if (!showUI) return;

            currentState = questItemState;
            currentState.EnterState();
            (currentState as UIQuestItemState).SetQuestItemLabel(item.itemName);
        }
    }
}
