using UnityEngine;

namespace Platform.RotatingPlatform
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlatformScriptableObject platform;


        private void Start()
        {
            speed = platform.rotateSpeed;
        }

        void Update()
        {
            if(platform.direction.Equals(Directions.LEFT))
            {
                transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
            }
            else
            {
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
            }
        }


    }
}