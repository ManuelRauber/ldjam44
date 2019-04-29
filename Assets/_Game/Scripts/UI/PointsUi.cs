using LdJam44.Variables;
using TMPro;
using UnityEngine.EventSystems;

namespace LdJam44.UI
{
    public class PointsUi : UIBehaviour
    {
        public TextMeshProUGUI Text;
        public IntVariable Points;

        private void Update()
        {
            Text.text = Points.Value.ToString();
        }
    }
}
