using LdJam44.UI;
using UnityEditor;
using UnityEngine;

namespace LdJam44._Game.Editor
{
    [CustomEditor(typeof(BloodSplatter))]
    public class BloodSplatterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var bloodSplatter = (BloodSplatter)target;

            if (GUILayout.Button("Set Alpha 1"))
            {
                foreach (var image in bloodSplatter.BloodSplatterImages)
                {
                    var color = image.color;
                    image.color = new Color(color.r, color.g, color.b, 1);
                }
            }
            
            if (GUILayout.Button("Set Alpha 0"))
            {
                foreach (var image in bloodSplatter.BloodSplatterImages)
                {
                    var color = image.color;
                    image.color = new Color(color.r, color.g, color.b, 0);
                }
            }
        }
    }
}
