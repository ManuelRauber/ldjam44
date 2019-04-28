using System;
using LdJam44.Managers.Lanes;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Driver
{
    public abstract class DriverBaseController : MonoBehaviour
    {
        [Header("References")]
        public Rigidbody Rigidbody;

        [Header("Diagnostics")]
        [SerializeField]
        protected float TargetZPosition;

        [SerializeField]
        protected bool IsSwitchingLanes;
        
        [SerializeField]
        protected float MaxAllowedSpeed;

        [Header("Variables")]
        public float LaneSwitchTolerance = 0.25f;

        public LanesVariable Lanes;

        public virtual void SwitchLane(int laneNumber)
        {
            if (!CanSwitchLane(laneNumber))
            {
                return;
            }

            IsSwitchingLanes = true;

            TargetZPosition = Lanes.Value[laneNumber].Position.z;

            InternalLaneSwitch(laneNumber);
        }

        protected bool CanSwitchLane(int laneNumber)
        {
            return laneNumber >= 0 && laneNumber < Lanes.Value.Length && !IsSwitchingLanes;
        }

        protected virtual void FixedUpdate()
        {
            IsSwitchingLanes = Math.Abs(TargetZPosition - transform.position.z) >= LaneSwitchTolerance;
        }

        protected abstract void InternalLaneSwitch(int laneNumber);
    }
}
