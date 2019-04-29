using LdJam44.World;
using UnityEditor;
using UnityEngine;

namespace LdJam44._Game.Editor
{
    [CustomEditor(typeof(TreeSpawner))]
    public class TreeSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var treeSpawner = (TreeSpawner)target;
            
            if (GUILayout.Button("Spawn Trees"))
            {
                treeSpawner.SpawnTrees();
            }
        }
    }
}
