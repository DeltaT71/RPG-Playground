using RPG.Quest;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePlayerPotions;
        public static event UnityAction<TextAsset, GameObject> OnInteractNPC;
        public static event UnityAction<QuestItemSO, bool> OnChestInteract;
        public static event UnityAction<bool> OnToggleUI;
        public static event UnityAction<RewardSO> OnReward;
        public static event UnityAction<Collider, int> OnPortalEnter;
        public static event UnityAction<bool> OnCutsceneUpdated;

        public static void RaiseChangePlayerHealth(float newHealthPoints) => OnChangePlayerHealth?.Invoke(newHealthPoints);
        public static void RaiseChangePlayerPotionsCount(int newPotionCount) => OnChangePlayerPotions?.Invoke(newPotionCount);
        public static void RaiseInteractNPC(TextAsset inkJSON, GameObject npc) => OnInteractNPC?.Invoke(inkJSON, npc);
        public static void RaiseChestInteract(QuestItemSO item, bool showUi) => OnChestInteract?.Invoke(item, showUi);
        public static void RaiseToggleUI(bool isOpen) => OnToggleUI?.Invoke(isOpen);
        public static void RaiseReward(RewardSO reward) => OnReward?.Invoke(reward);
        public static void RaisePortalEnter(Collider player, int nextSceneIndex) => OnPortalEnter?.Invoke(player, nextSceneIndex);
        public static void RaiseCutsceneUpdated(bool isEnabled) => OnCutsceneUpdated?.Invoke(isEnabled);


    }
}

