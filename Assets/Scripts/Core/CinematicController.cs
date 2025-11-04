using RPG.Utility;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Core
{
    public class CinematicController : MonoBehaviour
    {
        PlayableDirector playableDirectorCmp;
        Collider colliderCmp;

        [SerializeField] private bool customPlayOnAwake = false;

        void Awake()
        {
            playableDirectorCmp = GetComponent<PlayableDirector>();
            colliderCmp = GetComponent<Collider>();
        }

        void Start()
        {
            colliderCmp.enabled = !PlayerPrefs.HasKey("SceneIndex");

            if (!customPlayOnAwake) return;

            colliderCmp.enabled = false;
            playableDirectorCmp.Play();
        }
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            colliderCmp.enabled = false;
            playableDirectorCmp.Play();
        }

        void OnEnable()
        {
            playableDirectorCmp.played += HandlePlay;
            playableDirectorCmp.stopped += HandleStopped;

        }

        void OnDisable()
        {
            playableDirectorCmp.played -= HandlePlay;
            playableDirectorCmp.stopped -= HandleStopped;
        }

        private void HandlePlay(PlayableDirector pb)
        {
            EventManager.RaiseCutsceneUpdated(false);
        }

        private void HandleStopped(PlayableDirector pb)
        {
            EventManager.RaiseCutsceneUpdated(true);
        }
    }

}
