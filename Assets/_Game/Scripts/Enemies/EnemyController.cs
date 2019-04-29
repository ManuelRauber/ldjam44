using System;
using LdJam44.Driver;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Enemies
{
    public class EnemyController : DriverBaseController
    {
        [Header("References")]
        public FloatVariable DriverXPosition;

        [Header("Diagnostics")]
        public int LaneNumber;

        public bool IsIntroMode;

        private void Start()
        {
            SwitchLane(LaneNumber);
        }

        protected override void InternalLaneSwitch(int laneNumber)
        {
            LaneNumber = laneNumber;
            var lane = Lanes.Value[LaneNumber];
            MaxAllowedSpeed = lane.Reverse ? -lane.Speed : lane.Speed;
        }

        protected override void FixedUpdate()
        {
            Rigidbody.velocity = new Vector3(MaxAllowedSpeed, 0, TargetZPosition - transform.position.z);

            if (Math.Abs(DriverXPosition - transform.position.x) > 40 && !IsIntroMode)
            {
                Destroy(gameObject);
            }
        }
    }
}
