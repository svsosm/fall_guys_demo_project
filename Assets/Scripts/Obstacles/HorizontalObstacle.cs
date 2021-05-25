using UnityEngine;

namespace Obstacle.HorizontalObstacle
{
    public class HorizontalObstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleScriptableObject obstacle;
        private float speed;
        private float length;
        private Vector3 startPos;
        private Vector3 targetPos;
        private float lerpTime;

        private void Start()
        {
            speed = obstacle.speed;
            length = obstacle.movementLength;
            startPos = transform.position;
        }

        void Update()
        {
            if(obstacle.direction.Equals(Directions.LEFT))
            {
                MovementHorizontalObstacle(-length);
            }
            else
            {
                MovementHorizontalObstacle(length);
            }

        }

        void MovementHorizontalObstacle(float distance)
        {
            targetPos = new Vector3(startPos.x + distance, startPos.y, startPos.z);
            lerpTime = Mathf.PingPong(Time.time, speed) / speed;
            transform.position = Vector3.Lerp(startPos, targetPos, lerpTime);
        }


    }
}
