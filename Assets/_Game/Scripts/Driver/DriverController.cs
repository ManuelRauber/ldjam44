using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Driver
{
    public class DriverController : DriverBaseController
    {
        [Header("References")]
        public Transform EnemySensorTransform;

        [Header("Variables")]
        public float DriverSpeed = 5f;

        public IntVariable CurrentLane;
        public FloatVariable XPosition;
        public float SensorDistance = 3f;
        public float SensorScanDelay = 0.25f;

        private float _timeToNextSensorScan;

        private void Start()
        {
            _timeToNextSensorScan = Time.time;
            SwitchLane(CurrentLane);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchLane(CurrentLane + 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchLane(CurrentLane - 1);
            }

            if (HasEnemyInFront() && !IsSwitchingLanes && _timeToNextSensorScan > Time.time)
            {
                Debug.Log("Switching Lane");

                var nextLaneModifier = Random.Range(0, 100) > 50 ? 1 : -1;
                var nextLane = CurrentLane + nextLaneModifier;

                if (!CanSwitchLane(nextLane))
                {
                    nextLane = CurrentLane - nextLaneModifier;
                } 
                
                SwitchLane(nextLane);
            }
        }

        protected override void InternalLaneSwitch(int laneNumber)
        {
            CurrentLane.Value = laneNumber;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Rigidbody.velocity = new Vector3(DriverSpeed, 0, TargetZPosition - transform.position.z);
        }

        private void LateUpdate()
        {
            XPosition.Value = transform.position.x;
        }

        private bool HasEnemyInFront()
        {
            _timeToNextSensorScan = Time.time + SensorScanDelay;
            return Physics.Raycast(EnemySensorTransform.position, Vector3.right, SensorDistance, 1 << Layers.EnemiesMask);
        }

        private void OnDrawGizmosSelected()
        {
            if (!EnemySensorTransform)
            {
                return;
            }

            Gizmos.color = Color.blue;

            var sensorPosition = EnemySensorTransform.position;
            Gizmos.DrawLine(sensorPosition, sensorPosition + Vector3.right * SensorDistance);
        }
    }
}
