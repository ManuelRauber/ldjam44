using System.Collections;
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
            StartCoroutine(DoPlayerDispatch());
        }

        public IEnumerator DoPlayerDispatch()
        {
            Rigidbody.isKinematic = true;
            yield return new WaitForEndOfFrame();
            
            transform.SetParent(null);
            yield return new WaitForEndOfFrame();
            
            Rigidbody.MovePosition(PlayerDispatchPoint.position);
            yield return new WaitForEndOfFrame();
            
            Rigidbody.isKinematic = false;
        }

        public void DoorsOpenSignal()
        {
            JointLine.IsEnabled = true;
        }
    }
}
