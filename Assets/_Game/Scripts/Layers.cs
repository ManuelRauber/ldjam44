using UnityEngine;

namespace LdJam44
{
    public static class Layers
    {
        public static readonly string Enemies = "Enemies";
        public static readonly int EnemiesMask = LayerMask.NameToLayer(Enemies);
    }
}
