using RPG.Core;
using UnityEngine;

namespace RPG.Quest
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private RewardSO reward;
        private bool rewardTakan = false;

        public void SendReward()
        {
            if (rewardTakan) return;

            rewardTakan = true;
            EventManager.RaiseReward(reward);
        }
    }
}

