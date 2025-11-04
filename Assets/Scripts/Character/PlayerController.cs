using System;
using PRG.Character;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSO stats;
        [NonSerialized] public Health healthCmp;
        [NonSerialized] public Combat combatCmp;
        private GameObject axeWeapon;
        private GameObject swordWeapon;
        public Weapons weapon = Weapons.Axe;

        void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} Missing Scriptable Object for stats!");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
            axeWeapon = GameObject.FindGameObjectWithTag(Constants.AXE_TAG);
            swordWeapon = GameObject.FindGameObjectWithTag(Constants.SWORD_TAG);
        }

        void OnEnable()
        {
            EventManager.OnReward += HandleReward;
        }

        void OnDisable()
        {
            EventManager.OnReward += HandleReward;
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("Health"))
            {
                healthCmp.healthPoints = PlayerPrefs.GetFloat("Health");
                healthCmp.potionCount = PlayerPrefs.GetInt("Potions");
                combatCmp.damagePoints = PlayerPrefs.GetFloat("Damage");
                weapon = (Weapons)PlayerPrefs.GetInt("Weapon");

                NavMeshAgent agentCmp = GetComponent<NavMeshAgent>();
                Portal portalCmp = FindObjectOfType<Portal>();

                agentCmp.Warp(portalCmp.spawnPoint.position);
                transform.rotation = portalCmp.spawnPoint.rotation;
            }
            else
            {
                healthCmp.healthPoints = stats.health;
                combatCmp.damagePoints = stats.damage;
            }

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePlayerPotionsCount(healthCmp.potionCount);

            SetWeapon();
        }

        private void HandleReward(RewardSO reward)
        {
            healthCmp.healthPoints += reward.bonusHealth;
            healthCmp.potionCount += reward.bonusPotions;
            combatCmp.damagePoints += reward.bonusDamage;

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePlayerPotionsCount(healthCmp.potionCount);

            if (reward.forceWeaponSwap)
            {
                weapon = reward.weapon;
                SetWeapon();
            }
        }

        private void SetWeapon()
        {
            if (weapon == Weapons.Axe)
            {
                axeWeapon.SetActive(true);
                swordWeapon.SetActive(false);
            }
            else
            {
                axeWeapon.SetActive(false);
                swordWeapon.SetActive(true);
            }
        }
    }
}

