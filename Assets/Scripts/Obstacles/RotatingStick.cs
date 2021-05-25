using UnityEngine;

namespace Obstacle.RotatingStick
{
    public class RotatingStick : MonoBehaviour
    {
        private Rigidbody colRb;
        private Vector3 dir;
        [SerializeField] private float appliedForce;
        private const string playerTag = "Player";

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag.Equals(playerTag))
            {
                colRb = collision.gameObject.GetComponent<Rigidbody>();
                dir = (transform.position - collision.GetContact(0).point).normalized;
                colRb.AddForce(-dir * appliedForce, ForceMode.Force);
            }

        }


    }

}