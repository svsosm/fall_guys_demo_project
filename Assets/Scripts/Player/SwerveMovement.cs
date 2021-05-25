using UnityEngine;

namespace Player.SwerveInput.SwerveMovement
{
    public class SwerveMovement : MonoBehaviour
    {
        private SwerveInputSystem _swerveInputSystem;

        [Header("Swerve Settings")]
        [SerializeField] private float swerveSpeed;
        [SerializeField] private float maxSwerveAmount;

        private float swerveAmount;
        private Rigidbody rb;

        private void Awake()
        {
            _swerveInputSystem = GetComponent<SwerveInputSystem>();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            swerveAmount = swerveSpeed * (_swerveInputSystem.MoveFactorX * Time.fixedDeltaTime);
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount); //limit swerve amount

            Vector3 rot = new Vector3(0, swerveAmount, 0);
            rb.MoveRotation(Quaternion.Euler(rot) * rb.rotation);
        }
    }
}