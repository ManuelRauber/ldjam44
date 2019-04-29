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

        public void StartupSequenceDone()
        {
            Collider.enabled = true;
        }

        public void StartupTimelineDone()
        {
            StartupSequenceDoneEvent.Raise();
        }

        public void PlayerDispatchedSignal()
        {
            transform.SetParent(null, true);
            transform.position = PlayerDispatchPoint.position;
        }

        public void DoorsOpenSignal()
        {
            JointLine.IsEnabled = true;
        }
    }
}
