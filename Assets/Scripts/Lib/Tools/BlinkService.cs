using UnityEngine;

namespace Lib.Tools
{
    public class BlinkService
    {
        public static Color BlinkUpdateColor(Color sourceColor, float factor = 3)
        {
            float progress = (sourceColor.r + Time.deltaTime * factor);
            
            if (progress >= 1)
            {
                progress = 0.1f;
            }

            return new Color(progress, progress, progress, 1);
        }
    }
}