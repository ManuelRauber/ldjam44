using UnityEngine;

namespace LdJam44.Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [Header("References")]
        public Rigidbody Rigidbody;

        [Header("Variables")]
        public float MovementStrength = 30000;

        private bool _isMovementEnabled = true;

        public void Move(Vector3 rawMovement)
        {
            if (_isMovementEnabled)
            {
                Rigidbody.AddForce(rawMovement * MovementStrength * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }

        public void GameOver()
        {
            _isMovementEnabled = false;
        }
    }
}
