using UnityEngine;

namespace Player.SwerveInput
{
    public class SwerveInputSystem : MonoBehaviour
    {
        private float _lastFrameFingerPositionX;
        private float _moveFactorX;
        public float MoveFactorX => _moveFactorX;

        private void Update()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            SwerveInputForPC();
#else
            SwerveInputForMobile();
#endif
        }

        #region Swerve Input For PC
        void SwerveInputForPC()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
                _lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _moveFactorX = 0f;
            }
        }
        #endregion

        #region Swerve Input For Mobile
        void SwerveInputForMobile()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    _moveFactorX = touch.deltaPosition.x;  //give horizontal distance
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    _moveFactorX = 0f;
                }
            }
        }
        #endregion
    }
}