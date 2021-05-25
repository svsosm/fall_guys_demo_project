using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Obstacle.HalfDonut
{
    public class HalfDonut : MonoBehaviour
    {
        [SerializeField] private Transform movingStick;
        [SerializeField] private Vector3 startPos;
        [SerializeField] private Vector3 targetPos;
        [SerializeField] ObstacleScriptableObject obstacle;
        private float _speed;
        private float lerpTime;

        private void Start()
        {
            _speed = obstacle.speed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            lerpTime = Mathf.PingPong(Time.time, _speed) / _speed;
            movingStick.localPosition = Vector3.Lerp(startPos, targetPos, lerpTime);
        }
    }
}
