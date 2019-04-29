using System;
using LdJam44.EventSystem;
using LdJam44.Variables;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public GameObject BloodDecal;

        [Header("Variables")]
        public IntVariable Life;

        public int LifeDrainPerTwoSeconds = 1;
        
        public GameEvent PlayerHasBeenHitEvent;

        private float _timeToNextLifeDrain;

        private void Awake()
        {
            Collider.enabled = false;
            JointLine.IsEnabled = false;
            _timeToNextLifeDrain = Time.time + 2;
        }

        private void Update()
        {
            if (Time.time > _timeToNextLifeDrain)
            {
                Life.Value -= LifeDrainPerTwoSeconds;
                SpawnBloodDecal();

                if (Life.Value < 0)
                {
                    Life.Value = 0;
                }

                _timeToNextLifeDrain = Time.time + 2;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CollideWithEnemy(other.gameObject))
            {
                return;
            }

            CollideWithBloodPack(other);
        }

        private void SpawnBloodDecal()
        {
            var decal = Instantiate(BloodDecal, new Vector3(transform.position.x, 0.03f, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
            Destroy(decal, 5);
        }

        private void CollideWithBloodPack(Collider other)
        {
            if (other.CompareTag(Tags.BloodPack))
            {
                Life.Value = Math.Min(Life.InitialValue, Life.Value + 20);
                Destroy(other.gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            CollideWithEnemy(other.gameObject);
        }

        private bool CollideWithEnemy(GameObject other)
        {
            if (other.CompareTag(Tags.Enemy) || other.CompareTag(Tags.Driver))
            {
                Life.Value -= 10;
                PlayerHasBeenHitEvent.Raise();
                SpawnBloodDecal();

                return false;
            }

            return false;
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
