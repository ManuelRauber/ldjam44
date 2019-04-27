using LdJam44.Driver;
using UnityEngine;

namespace LdJam44.Enemies
{
    public class EnemyController : DriverBaseController
    {
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
            base.FixedUpdate();
            
            var lane = LaneManager.Lanes[LaneNumber];
            Rigidbody.velocity = new Vector3(
                lane.Reverse ? -lane.Speed : lane.Speed, 
                0, 
                TargetZPosition - transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.DestroyBarrier))
            {
                Debug.Log("Destroy Barrier hit. Going to die :-(");
                Destroy(gameObject, 15f);
            }
        }
    }
}
