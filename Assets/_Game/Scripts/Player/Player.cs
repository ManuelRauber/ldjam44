using LdJam44.EventSystem;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Player
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        public Collider Collider;

        public GameEvent StartupSequenceDoneEvent;
        public Transform PlayerDispatchPoint;
        public JointLine JointLine;
        public Rigidbody Rigidbody;

        [Header("Variables")]
        public IntVariable Life;

        public GameEvent PlayerHasBeenHitEvent;

        private void Awake()
        {
            Collider.enabled = false;
            JointLine.IsEnabled = false;
        }

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
            if (other.CompareTag(Tags.Enemy) || other.CompareTag(Tags.Driver))
            {
                Life.Value -= 10;
                PlayerHasBeenHitEvent.Raise();
            }
        }

        public void StartupTimelineDone()
        {
            StartupSequenceDoneEvent.Raise();
        }

        public void PlayerDispatchedSignal()
        {
            Rigidbody.isKinematic = true;
            Destroy(GetComponent<Animator>());
            transform.SetParent(null);
            Rigidbody.MovePosition(PlayerDispatchPoint.position);
            Rigidbody.isKinematic = false;
            transform.rotation = Quaternion.identity;
            
            Collider.enabled = true;
        }

        public void DoorsOpenSignal()
        {
            JointLine.IsEnabled = true;
        }
    }
}
