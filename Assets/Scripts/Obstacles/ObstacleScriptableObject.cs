using UnityEngine;

namespace Obstacle
{
    public enum Obstacles
    {
        HorizontalObstacle,
        StaticObstacle,
        HalfDonutObstacle,
        RotatorObstacle

    }

    public enum Directions
    {
        LEFT,
        RIGHT
    }

    [CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacle")]
    public class ObstacleScriptableObject : ScriptableObject
    {
        public Obstacles obstacle;
        public Directions direction;
        public float speed;
        public float movementLength;

    }
}
