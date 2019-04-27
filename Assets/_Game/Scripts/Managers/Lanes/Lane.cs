using System;
using UnityEngine;

namespace LdJam44.Managers.Lanes
{
    [Serializable]
    public class Lane
    {
        public Vector3 Position;
        public float Speed = 5;
        public bool Reverse;
    }
}
