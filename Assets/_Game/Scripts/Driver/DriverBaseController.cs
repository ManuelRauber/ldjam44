using System;
using LdJam44.Managers.Lanes;
using UnityEngine;

namespace LdJam44.Driver
{
    public abstract class DriverBaseController : MonoBehaviour
    {
        [Header("References")]
        public LaneManager LaneManager;

        public Rigidbody Rigidbody;

        [Header("Diagnostics")]
        [SerializeField]
        protected float TargetZPosition;

        [SerializeField]
        protected bool IsSwitchingLanes;

        [Header("Variables")]
        public float LaneSwitchTolerance = 0.25f;

        public void SwitchLane(int laneNumber)
        {
            if (!CanSwitchLane(laneNumber))
            {
                return;
            }

            IsSwitchingLanes = true;

            TargetZPosition = LaneManager.Lanes[laneNumber].Position.z;

            InternalLaneSwitch(laneNumber);
        }

        protected bool CanSwitchLane(int laneNumber)
        {
            return laneNumber >= 0 && laneNumber < LaneManager.Lanes.Length && !IsSwitchingLanes;
        }

        protected virtual void FixedUpdate()
        {
            IsSwitchingLanes = Math.Abs(TargetZPosition - transform.position.z) >= LaneSwitchTolerance;
        }

        protected abstract void InternalLaneSwitch(int laneNumber);
    }
}