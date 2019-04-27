using LdJam44.EventSystem;
using UnityEditor;
using UnityEngine;

namespace LdJam44._Game.Editor
{
    [CustomEditor(typeof(GameEventListener))]
    public class GameEventListenerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var gameEventListener = (GameEventListener)target;
            
            if (GUILayout.Button("Raise Event"))
            {
                gameEventListener.Event.Raise();
            }
        }
    }
}
