using LdJam44.Sandbox;
using UnityEditor;
using UnityEngine;

namespace LdJam44._Game.Sandbox.Editor
{
    [CustomEditor(typeof(Driver))]
    public class DriverEditor : UnityEditor.Editor
    {
        private int _laneToSwitchTo;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var driver = (Driver) target;

            _laneToSwitchTo = EditorGUILayout.IntField("Lane to switch to", _laneToSwitchTo);
            
            if (GUILayout.Button("Set lane"))
            {
                driver.SwitchLane(_laneToSwitchTo);
            }
        }
    }
}
