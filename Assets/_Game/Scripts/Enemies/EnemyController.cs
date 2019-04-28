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

        private void Start()
        {
            SwitchLane(LaneNumber);
        }

        protected override void InternalLaneSwitch(int laneNumber)
        {
            LaneNumber = laneNumber;
        }
        
        protected override void FixedUpdate()
        {
            var lane = Lanes.Value[LaneNumber];
            Rigidbody.velocity = new Vector3(
                lane.Reverse ? -lane.Speed : lane.Speed, 
                0, 
                TargetZPosition - transform.position.z);

            if (Math.Abs(DriverXPosition - transform.position.x) > 40)
            {
                Debug.Log("Destroying EnemyController");
                Destroy(gameObject);
            }
        }
    }
}
