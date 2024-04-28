using UnityEngine;

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

        public static bool IsFacing(Transform origin, Transform target)
        {
            return origin.position.x - target.position.x < 0;
        }
    }
}