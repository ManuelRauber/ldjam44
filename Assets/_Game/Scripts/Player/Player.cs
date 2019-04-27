using LdJam44.EventSystem;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Variables")]
        public IntVariable Life;

        public GameEvent PlayerHasBeenHitEvent;

        private void OnTriggerEnter(Collider other)
        {
            CollideWithEnemy(other.gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            CollideWithEnemy(other.gameObject);
        }

        private void CollideWithEnemy(GameObject other)
        {
            if (!other.CompareTag(Tags.Enemy))
            {
                return;
            }
            
            Life.Value -= 10;
            PlayerHasBeenHitEvent.Raise();
        }
    }
}
