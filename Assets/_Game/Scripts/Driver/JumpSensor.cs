using System;
using UnityEngine;

namespace LdJam44.Driver
{
    public class JumpSensor : MonoBehaviour
    {
        public event Action JumpRequested;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Enemy))
            {
                Debug.Log("Jump Trigger Enter");
                JumpRequested?.Invoke();
            }
        }
    }
}
