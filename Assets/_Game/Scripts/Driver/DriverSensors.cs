using System;
using System.Linq;
using LdJam44.Managers.Lanes;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Driver
{
    public class DriverSensors : MonoBehaviour
    {
        [Header("Variables")]
        public LanesVariable Lanes;

        public IntVariable DriverLane;
        public float SensorDistance = 7f;
        public float NonDriverLaneSensorMultiplier = 1.5f;

        public SensorsScanResult Scan()
        {
            return new SensorsScanResult()
            {
                Results = Lanes.Value.Select((lane, index) =>
                {
                    var position = transform.position;
                    return new SensorScanResult()
                    {
                        LaneNumber = index,
                        HasHit = Physics.Raycast(
                            new Vector3(position.x, position.y, lane.Position.z),
                            Vector3.right,
                            GetSensorDistance(index), 1 << Layers.EnemiesMask
                        )
                    };
                }).ToArray()
            };
        }

        private void OnDrawGizmosSelected()
        {
            for (var index = 0; index < Lanes.Value.Length; index++)
            {
                var lane = Lanes.Value[index];
                DrawSensorGizmo(lane, index);
            }
        }

        private float GetSensorDistance(int laneNumber)
        {
            return DriverLane == laneNumber ? SensorDistance : SensorDistance * NonDriverLaneSensorMultiplier;
        }

        private void DrawSensorGizmo(Lane lane, int laneNumber)
        {
            Gizmos.color = Color.blue;

            var sensorDistance = GetSensorDistance(laneNumber);

            var sensorOrigin = transform.position;
            var sensorPosition = new Vector3(sensorOrigin.x, sensorOrigin.y, lane.Position.z);
            Gizmos.DrawLine(sensorPosition, sensorPosition + Vector3.right * sensorDistance);
        }
    }
}
