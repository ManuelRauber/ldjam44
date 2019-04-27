using System;
using UnityEngine;

namespace LdJam44.Sandbox
{
    public class Driver : MonoBehaviour
    {
        [Header("References")]
        public LaneManager LaneManager;

        public Rigidbody Rigidbody;

        [Header("Variables")]
        public float DriverSpeed = 5f;

        [Header("Diagnostics")]
        [SerializeField]
        private int CurrentLane;

        [SerializeField]
        private float TargetZPosition;

        private void Start()
        {
            SwitchLane(CurrentLane);
        }

        public void SwitchLane(int laneNumber)
        {
            if (laneNumber < 0 || laneNumber >= LaneManager.Lanes.Length)
            {
                Debug.LogWarning($"Can not switch to lane {laneNumber}!");
                return;
            }

            TargetZPosition = LaneManager.Lanes[laneNumber].z;

            CurrentLane = laneNumber;
        }

        private void FixedUpdate()
        {
            Rigidbody.velocity = new Vector3(DriverSpeed, 0, TargetZPosition - transform.position.z);
        }
    }
}
