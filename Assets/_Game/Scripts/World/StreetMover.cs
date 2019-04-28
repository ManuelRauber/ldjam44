using System.Collections.Generic;
using UnityEngine;

namespace LdJam44.World
{
    public class StreetMover : MonoBehaviour
    {
        [Header("References")]
        public Camera Camera;

        public GameObject[] StreetTiles;

        [Header("Variables")]
        public float StreetXPlacement = 9.49f;

        [Header("Diagnostics")]
        [SerializeField]
        private int CurrentX;

        private Queue<GameObject> StreetQueue = new Queue<GameObject>();

        private void Start()
        {
            InitializeStreets();
        }

        private void InitializeStreets()
        {
            for (var index = 0; index < StreetTiles.Length; index++)
            {
                var streetTile = StreetTiles[index];
                streetTile.transform.position = new Vector3(CurrentX * StreetXPlacement, 0, 0);
                CurrentX = index;
                
                StreetQueue.Enqueue(streetTile);
            }

            CurrentX--;
        }

        private void PlaceLastStreetToFront()
        {
            CurrentX++;
            var streetTile = StreetQueue.Dequeue();
            streetTile.transform.position = new Vector3(CurrentX * StreetXPlacement, 0, 0);

            var treeAreas = streetTile.GetComponentsInChildren<TreeSpawner>();
            foreach (var treeSpawner in treeAreas)
            {
                treeSpawner.RepositionTrees();
            }
            
            StreetQueue.Enqueue(streetTile);
        }

        private void Update()
        {
            if (Camera.transform.position.x >= (CurrentX - 2) * StreetXPlacement)
            {
                PlaceLastStreetToFront();
            }
        }
    }
}
