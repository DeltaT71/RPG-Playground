using RPG.Utility;
using UnityEngine;

namespace RPG.Untility
{
    public class Billboard : MonoBehaviour
    {
        private GameObject cam;

        void Awake()
        {
            cam = GameObject.FindGameObjectWithTag(Constants.CAMERA_TAG);
        }

        void LateUpdate()
        {
            Vector3 cameraDirection = transform.position + cam.transform.forward;

            transform.LookAt(cameraDirection);
        }
    }
}

