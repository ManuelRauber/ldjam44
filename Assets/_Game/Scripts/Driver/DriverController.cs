using System.Linq;
using LdJam44.Extensions;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Driver
{
    public class DriverController : DriverBaseController
    {
        [Header("Variables")]
        public float DriverSpeed = 5f;

        public float SensorScanDelay = 0.25f;
        public float BreakModifier = 0.5f;

        public IntVariable CurrentLane;
        public FloatVariable XPosition;
        public DriverSensors Sensors;
        public float LaneSpeedSwitchModifier = 1.75f;
        public AudioSource AudioSource;
        public AudioClip[] AudioClips;

        [Header("Diagnostics")]
        [SerializeField]
        private float CurrentSpeed;

        [SerializeField]
        private float TBreakInterpolator;

        [SerializeField]
        private bool IsBreaking;

        private float _timeToNextSensorScan;
        private bool _isInitialLaneSwitchDone;

        private void Start()
        {
            MaxAllowedSpeed = DriverSpeed;
            _timeToNextSensorScan = Time.time;
            SwitchLane(CurrentLane);
        }

        private void Update()
        {
            TBreakInterpolator = Mathf.Clamp01(TBreakInterpolator + BreakModifier * Time.deltaTime);

            if (IsGameOver)
            {
                MaxAllowedSpeed = 0;
                return;
            }

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

            if (_isInitialLaneSwitchDone)
            {
                AudioSource.PlayOneShot(AudioClips.PickOne());
            }

            _isInitialLaneSwitchDone = true;

            StopBreaking();
        }

        private void StopBreaking()
        {
            CurrentSpeed = MaxAllowedSpeed;
            MaxAllowedSpeed = DriverSpeed;
            TBreakInterpolator = 0;
            IsBreaking = false;
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
                if (IsBreaking)
                {
                    StopBreaking();
                }

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

            if (Lanes.Value[CurrentLane].Reverse)
            {
                return;
            }

            if (IsBreaking)
            {
                return;
            }

            IsBreaking = true;
            CurrentSpeed = DriverSpeed;
            MaxAllowedSpeed = Lanes.Value[CurrentLane].Speed;
            TBreakInterpolator = 0;
        }

        protected override void InternalLaneSwitch(int laneNumber)
        {
            CurrentLane.Value = laneNumber;
            Debug.Log($"Switched to lane {laneNumber}");
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Rigidbody.velocity = new Vector3(
                Mathf.Lerp(CurrentSpeed, MaxAllowedSpeed, TBreakInterpolator),
                0,
                (TargetZPosition - transform.position.z) * LaneSpeedSwitchModifier);
        }

        private void LateUpdate()
        {
            XPosition.Value = transform.position.x;
        }
    }
}
