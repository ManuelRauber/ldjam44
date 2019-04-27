using LdJam44.Managers.Lanes;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Driver
{
    public class DriverController : MonoBehaviour
    {
        [Header("References")]
        public LaneManager LaneManager;

        public Rigidbody Rigidbody;

        [Header("Variables")]
        public float DriverSpeed = 5f;

        [Header("Diagnostics")]
        public IntVariable CurrentLane;

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

            TargetZPosition = LaneManager.Lanes[laneNumber].Position.z;

            CurrentLane.Value = laneNumber;
        }

        private void FixedUpdate()
        {
            Rigidbody.velocity = new Vector3(DriverSpeed, 0, TargetZPosition - transform.position.z);
        }
    }
}
