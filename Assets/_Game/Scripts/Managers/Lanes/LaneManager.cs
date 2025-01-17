using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Managers.Lanes
{
    public class LaneManager : MonoBehaviour
    {
        [Header("Variables")]
        public LanesVariable Lanes;

        public FloatVariable LaneWidth;

        private void OnDrawGizmos()
        {
            if (Lanes == null)
            {
                return;
            }
            
            foreach (var lane in Lanes.Value)
            {
                var position = lane.Position;
                
                Gizmos.color = Color.red;
                
                var leftLaneStrip = position - new Vector3(0, 0, LaneWidth / 2);
                var rightLaneStrip = position + new Vector3(0, 0, LaneWidth / 2);
                
                Gizmos.DrawLine(leftLaneStrip - Vector3.right * 100, leftLaneStrip + Vector3.right * 100);
                Gizmos.DrawLine(rightLaneStrip - Vector3.right * 100, rightLaneStrip + Vector3.right * 100);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(position - Vector3.right * 100, position + Vector3.right * 100);
            }
        }
    }
}
