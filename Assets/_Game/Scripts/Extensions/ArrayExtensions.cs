using UnityEngine;

namespace LdJam44.Extensions
{
    public static class ArrayExtensions
    {
        public static T PickOne<T>(this T[] array)
            where T : class
        {
            return array?[Random.Range(0, array.Length)];
        }
    }
}
