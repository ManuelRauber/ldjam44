using UnityEngine;

namespace LdJam44.Sandbox
{
    public class PlayerMotor : MonoBehaviour
    {
        [Header("References")]
        public Rigidbody Rigidbody;
        
        [Header("Variables")]
        public float MovementStrength = 30000;
        
        public void Move(Vector3 rawMovement)
        {
            Rigidbody.AddForce(rawMovement * MovementStrength * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
