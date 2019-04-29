using System;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Managers
{
    public class PointManager : MonoBehaviour
    {
        [Header("Variables")]
        public IntVariable Points;

        private int _startPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.StartSign))
            {
                _startPoint = (int)transform.position.x;
            }
        }

        private void Update()
        {
            if (_startPoint > 0)
            {
                Points.Value = Math.Max(Points.Value, (int)transform.position.x - _startPoint);
            }
        }
    }
}
