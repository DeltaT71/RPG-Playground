using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIQuestItemState : UIBaseState
    {
        private VisualElement questItemContainer;
        private Label questItemText;
        private PlayerInput playerInputCmp;
        public UIQuestItemState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            playerInputCmp = GameObject.FindGameObjectWithTag(Constants.GAME_MANAGER_TAG).GetComponent<PlayerInput>();

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);

            questItemContainer = controller.root.Q<VisualElement>("quest-item-container");
            questItemText = controller.root.Q<Label>("quest-item-label");
            questItemContainer.style.display = DisplayStyle.Flex;

            EventManager.RaiseToggleUI(true);
        }

        public override void SelectedButton()
        {
            questItemContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);

            EventManager.RaiseToggleUI(false);
        }

        public void SetQuestItemLabel(string name)
        {
            questItemText.text = name;
        }
    }

}
