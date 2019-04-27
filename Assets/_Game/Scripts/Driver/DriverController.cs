using System.Linq;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Driver
{
    public class DriverController : DriverBaseController
    {
        [Header("Variables")]
        public float DriverSpeed = 5f;

        public float SensorScanDelay = 0.25f;

        public IntVariable CurrentLane;
        public FloatVariable XPosition;
        public DriverSensors Sensors;

        private float _timeToNextSensorScan;

        [Header("Diagnostics")]
        [SerializeField]
        private float MaxAllowedSpeed;

        private void Start()
        {
            _timeToNextSensorScan = Time.time;
            SwitchLane(CurrentLane);
        }

        private void Update()
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchLane(CurrentLane + 1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchLane(CurrentLane - 1);
            }
#endif

            SwitchLaneIfNecessary();
        }

        public override void SwitchLane(int laneNumber)
        {
            base.SwitchLane(laneNumber);
            MaxAllowedSpeed = DriverSpeed;
        }

        private void SwitchLaneIfNecessary()
        {
            if (IsSwitchingLanes || Time.time < _timeToNextSensorScan)
            {
                return;
            }

            _timeToNextSensorScan = Time.time + SensorScanDelay;

            var scanResult = Sensors.Scan();

            if (!scanResult.Results.Single(p => p.LaneNumber == CurrentLane).HasHit)
            {
                return;
            }

            var nextLaneModifier = Random.Range(0, 100) > 50 ? 1 : -1;
            var nextLane = CurrentLane + nextLaneModifier;

            if (CanSwitchLane(nextLane) && !scanResult.Results.Single(p => p.LaneNumber == nextLane).HasHit)
            {
                SwitchLane(nextLane);
                return;
            }
            
            nextLane = CurrentLane - nextLaneModifier;
            
            if (CanSwitchLane(nextLane) && !scanResult.Results.Single(p => p.LaneNumber == nextLane).HasHit)
            {
                SwitchLane(nextLane);
                return;
            }

            MaxAllowedSpeed = Lanes.Value[CurrentLane].Speed;
        }

        protected override void InternalLaneSwitch(int laneNumber)
        {
            CurrentLane.Value = laneNumber;
            Debug.Log($"Switched to lane {laneNumber}");
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Rigidbody.velocity = new Vector3(MaxAllowedSpeed, 0, TargetZPosition - transform.position.z);
        }

        private void LateUpdate()
        {
            XPosition.Value = transform.position.x;
        }
    }
}
