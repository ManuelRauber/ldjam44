using UnityEngine;

namespace LdJam44.Player
{
    public class JointLine : MonoBehaviour
    {
        [Header("References")]
        public SpringJoint ConnectedObject;

        public LineRenderer LineRenderer;

        public bool IsEnabled
        {
            get => LineRenderer.enabled;
            set => LineRenderer.enabled = value;
        }
        
        private void LateUpdate()
        {
            if (!ConnectedObject.connectedBody)
            {
                LineRenderer.positionCount = 0;
                return;
            }

            if (ConnectedObject.connectedBody && LineRenderer.positionCount != 2)
            {
                LineRenderer.positionCount = 2;
            }
            
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, ConnectedObject.connectedBody.transform.position);
        }
    }
}
