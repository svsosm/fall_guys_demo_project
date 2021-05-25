using UnityEngine;

namespace Obstacle.Rotator
{
    public class Rotator : MonoBehaviour
    {
        private float speed;
        [SerializeField] private ObstacleScriptableObject obstacle;

        private void Start()
        {
            speed = obstacle.speed;
        }

        void Update()
        {
            if(obstacle.direction.Equals(Directions.LEFT))
            {
                RotateObstacle(Vector3.up);
            }
            else
            {
                RotateObstacle(Vector3.down);
            }

        }

        void RotateObstacle(Vector3 direction)
        {
            transform.Rotate(direction * (speed * Time.deltaTime));
        }

    }

    
}
