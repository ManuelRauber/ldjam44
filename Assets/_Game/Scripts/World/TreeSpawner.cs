using System;
using LdJam44.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LdJam44.World
{
    public class TreeSpawner : MonoBehaviour
    {
        [Header("References")]
        public GameObject[] TreePrefabs;

        [Header("Variables")]
        public float Width;

        public float Height;
        public int TreeCount = 15;
        public bool SpawnOnAwake = true;

        private void Awake()
        {
            if (SpawnOnAwake)
            {
                SpawnTrees();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(transform.position, new Vector3(Width, 0.5f, Height));
        }

        public void SpawnTrees()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(transform.GetChild(i).gameObject);
#else
                Destroy(transform.GetChild(i).gameObject);
#endif
            }

            var position = transform.position;

            for (var i = 0; i < TreeCount; i++)
            {
                var tree = Instantiate(
                    TreePrefabs.PickOne(),
                    RandomTreePosition(position),
                    RandomTreeRotation()
                );

                tree.transform.SetParent(transform);
            }
        }

        private static Quaternion RandomTreeRotation()
        {
            return Quaternion.Euler(0, Random.Range(0, 360), 0);
        }

        private Vector3 RandomTreePosition(Vector3 center)
        {
            return new Vector3(
                Random.Range(center.x - Width / 2, center.x + Width / 2),
                center.y,
                Random.Range(center.z - Height / 2, center.z + Height / 2)
            );
        }

        public void RepositionTrees()
        {
            var position = transform.position;
            
            for (var i = 0; i < transform.childCount; i++)
            {
                var tree = transform.GetChild(i);
                tree.position = RandomTreePosition(position);
                tree.rotation = RandomTreeRotation();
            } 
        }
    }
}
