using System;
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        [NonSerialized] public float healthPoints = 0f;
        public int potionCount = 1;
        [SerializeField] private float healAmount = 15f;
        private Animator animatorCmp;
        [NonSerialized] public Slider sliderCmp;
        public event UnityAction OnStartDefeated = () => { };
        private BubbleEvent bubbleEventCmp;
        private bool isDefeated = false;

        void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            sliderCmp = GetComponentInChildren<Slider>();
        }

        void OnEnable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
        }

        void OnDisable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
        }

        public void TakeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0);

            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerHealth(healthPoints);
            }

            if (sliderCmp != null)
            {
                sliderCmp.value = healthPoints;
            }

            if (healthPoints == 0)
            {
                Defeated();
            }
        }

        private void Defeated()
        {
            if (isDefeated) return;

            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartDefeated.Invoke();
            }

            isDefeated = true;
            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }

        private void HandleBubbleCompleteDefeat()
        {
            Destroy(gameObject);
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed || potionCount == 0) return;

            potionCount--;
            healthPoints += healAmount;

            EventManager.RaiseChangePlayerHealth(healthPoints);
            EventManager.RaiseChangePlayerPotionsCount(potionCount);
        }
    }
}

