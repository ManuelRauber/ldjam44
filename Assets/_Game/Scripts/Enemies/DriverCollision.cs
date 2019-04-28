using UnityEngine;

namespace LdJam44.Enemies
{
    public class DriverCollision : MonoBehaviour
    {
        [Header("References")]
        public EnemyController EnemyController;

        public Rigidbody Rigidbody;
        public Collider Collider;

        [Header("Variables")]
        public float UpForce = 1500f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Driver) || other.CompareTag(Tags.Player))
            {
                Destroy(EnemyController);
                Destroy(Collider);
                Rigidbody.constraints = RigidbodyConstraints.None;
                Rigidbody.AddForce(Vector3.up * UpForce, ForceMode.VelocityChange); 
                Rigidbody.AddTorque(Random.insideUnitSphere * UpForce, ForceMode.VelocityChange);
                Rigidbody.useGravity = true;
            }
        }

        private void Update()
        {
            if (transform.position.y < -5)
            {
                Destroy(gameObject);
            }
        }
    }
}
