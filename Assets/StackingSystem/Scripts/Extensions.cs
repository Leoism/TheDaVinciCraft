using UnityEngine;

namespace StackingSystem.Scripts
{
    public static class Extensions
    {
        public static int ManhattanDistance(this Vector3Int a, Vector3Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
        }
    }
}