using UnityEngine;

namespace LdJam44.Player
{
    public class JointLine : MonoBehaviour
    {
        [Header("References")]
        public SpringJoint ConnectedObject;

        public LineRenderer LineRenderer;

        private void Start()
        {
            LineRenderer.positionCount = 2;
        }

        private void LateUpdate()
        {
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, ConnectedObject.connectedBody.transform.position);
        }
    }
}