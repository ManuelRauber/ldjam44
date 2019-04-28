using System;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Managers
{
    public class PointManager : MonoBehaviour
    {
        [Header("Variables")]
        public IntVariable Points;

        private int _initialXPosition;

        private void Start()
        {
            _initialXPosition = (int) transform.position.x;
        }

        private void Update()
        {
            Points.Value = Math.Max(Points.Value, (int) transform.position.x - _initialXPosition);
        }
    }
}
