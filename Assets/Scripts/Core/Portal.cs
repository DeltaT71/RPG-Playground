using RPG.Utility;
using UnityEngine;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int nextSceneIndex;

        public Transform spawnPoint;

        private Collider colliderCmp;

        void Awake()
        {
            colliderCmp = GetComponent<Collider>();
        }
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            colliderCmp.enabled = false;

            EventManager.RaisePortalEnter(other, nextSceneIndex);


            SceneTransition.Initiate(nextSceneIndex);
        }
    }

}
