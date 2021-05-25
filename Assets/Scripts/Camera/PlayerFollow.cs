using UnityEngine;

namespace Player.ThirdPersonCamera
{
    public class PlayerFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 runnerOffset;
        [SerializeField] private Vector3 paintingOffset;
        [SerializeField] private Vector3 paintingOffsetForMobile;
        [SerializeField] private float smoothSpeed;

        private Animator cameraTransition;
        private Vector3 desiredPos;
        private Camera camera;
        private float fieldOfViewForPortrait = 115f;
        private float fieldOfViewForLandscape = 64f;

        private void Start()
        {
            cameraTransition = GetComponent<Animator>();
            camera = Camera.main;
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.gameState.Equals(GameState.RUNNER))
            {
                desiredPos = target.position + runnerOffset;
            }
            else
            {
#if UNITY_EDITOR || UNITY_STANDALONE

                desiredPos = target.position + paintingOffset;
#else
                //set camera field view for mobile landscape and portrait mode
                if (Input.deviceOrientation.Equals(DeviceOrientation.Portrait))
                {
                    desiredPos = target.position + paintingOffsetForMobile;
                    camera.fieldOfView = fieldOfViewForPortrait;
                }
                else if(Input.deviceOrientation.Equals(DeviceOrientation.LandscapeRight) || Input.deviceOrientation.Equals(DeviceOrientation.LandscapeLeft))
                {
                    desiredPos = target.position + paintingOffset;
                    camera.fieldOfView = fieldOfViewForLandscape; 
                }
#endif
                cameraTransition.enabled = true;
            }

            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothPos;
        }
    }
}
