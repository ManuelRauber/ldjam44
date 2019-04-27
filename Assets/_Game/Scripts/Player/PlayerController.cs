using UnityEngine;

namespace LdJam44.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        public PlayerMotor PlayerMotor;

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            PlayerMotor.Move(new Vector3(0, 0, -horizontalMovement));
        }
    }
}
