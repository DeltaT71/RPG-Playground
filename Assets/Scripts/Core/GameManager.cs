using System;
using System.Collections.Generic;
using RPG.Character;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        private List<string> sceneEnemyIDs = new List<string>();
        private List<GameObject> enemiesAlive = new List<GameObject>();

        private PlayerInput playerInputCmp;

        void Awake()
        {
            playerInputCmp = GetComponent<PlayerInput>();
        }

        void Start()
        {
            List<GameObject> sceneEnemies = new List<GameObject>();

            sceneEnemies.AddRange(GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG));

            sceneEnemies.ForEach((GameObject sceneEnemy) =>
            {
                EnemyController enemyControllerCmp = sceneEnemy.GetComponent<EnemyController>();
                sceneEnemyIDs.Add(enemyControllerCmp.enemyID);
            });

        }
        void OnEnable()
        {
            EventManager.OnPortalEnter += HandlePortalEnter;
            EventManager.OnCutsceneUpdated += HandleCutsceneUpdated;
        }
        void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;
            EventManager.OnCutsceneUpdated -= HandleCutsceneUpdated;
        }

        private void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            PlayerController playeControllerCmp = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat("Health", playeControllerCmp.healthCmp.healthPoints);
            PlayerPrefs.SetInt("Potions", playeControllerCmp.healthCmp.potionCount);
            PlayerPrefs.SetFloat("Damage", playeControllerCmp.combatCmp.damagePoints);
            PlayerPrefs.SetInt("Weapon", (int)playeControllerCmp.weapon);
            PlayerPrefs.SetInt("SceneIndex", nextSceneIndex);

            enemiesAlive.AddRange(GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG));

            sceneEnemyIDs.ForEach(SaveDefeatedEnemies);

            Inventory inventoryCmp = player.GetComponent<Inventory>();

            inventoryCmp.items.ForEach(SaveQuestItem);

            List<GameObject> NPCs = new List<GameObject>(
                GameObject.FindGameObjectsWithTag(Constants.NPC_QUEST_TAG)
            );

            NPCs.ForEach(SaveNPCItem);
        }

        private void SaveDefeatedEnemies(string ID)
        {
            bool isAlive = false;

            enemiesAlive.ForEach((enemy) =>
            {
                string enemyID = enemy.GetComponent<EnemyController>().enemyID;

                if (enemyID == ID)
                {
                    isAlive = true;
                }
            });

            if (isAlive) return;

            List<string> enemiesDefeated = PlayerPrefsUtility.GetString("EnemiesDefeated");

            if (!enemiesDefeated.Contains(ID))
            {
                enemiesDefeated.Add(ID);
            }

            PlayerPrefsUtility.SetString("EnemiesDefeated", enemiesDefeated);
        }

        private void SaveQuestItem(QuestItemSO item)
        {
            List<string> playerItems = PlayerPrefsUtility.GetString("PlayerItems");

            if (!playerItems.Contains(item.name))
            {
                playerItems.Add(item.name);
            }

            PlayerPrefsUtility.SetString("PlayerItems", playerItems);
        }

        private void SaveNPCItem(GameObject npc)
        {
            NPCController npcControllerCmp = npc.GetComponent<NPCController>();

            if (!npcControllerCmp.hasQuestItem) return;

            List<string> npcItems = PlayerPrefsUtility.GetString("NPCItems");

            if (!npcItems.Contains(npcControllerCmp.desiredQuestItem.itemName))
            {
                npcItems.Add(npcControllerCmp.desiredQuestItem.itemName);
            }

            PlayerPrefsUtility.SetString("NPCItems", npcItems);

        }

        private void HandleCutsceneUpdated(bool isEnabled)
        {
            playerInputCmp.enabled = isEnabled;
        }
    }

}
