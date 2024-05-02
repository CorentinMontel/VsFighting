using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Lib.Tools
{
    public class DistanceHelper
    {
        public static float AbsoluteDistance(Transform a, Transform b)
        {
            Vector2 aPosition = a.position;
            Vector2 bPosition = b.position;
            return Mathf.Sqrt(Mathf.Pow(bPosition.x - aPosition.x, 2) + Mathf.Pow(bPosition.y - aPosition.y, 2));
        }

        public static Vector2 ComputeDirection(Vector2 source, Vector2 target)
        {
            return (target - source).normalized;
        }

        public static bool IsFacing(Transform origin, Transform target)
        {
            return origin.position.x - target.position.x < 0;
        }
    }
}