using UnityEngine;

namespace LdJam44.Managers
{
    public class LaneManager : MonoBehaviour
    {
        [Header("Variables")]
        public Vector3[] Lanes;

        [SerializeField]
        private float LaneSize = 1;

        private void OnDrawGizmosSelected()
        {
            if (Lanes == null)
            {
                return;
            }
            
            foreach (var lane in Lanes)
            {
                Gizmos.color = Color.red;
                
                var leftLaneStrip = lane - new Vector3(0, 0, LaneSize / 2);
                var rightLaneStrip = lane + new Vector3(0, 0, LaneSize / 2);
                
                Gizmos.DrawLine(leftLaneStrip - Vector3.right * 100, leftLaneStrip + Vector3.right * 100);
                Gizmos.DrawLine(rightLaneStrip - Vector3.right * 100, rightLaneStrip + Vector3.right * 100);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(lane - Vector3.right * 100, lane + Vector3.right * 100);
            }
        }
    }
}
