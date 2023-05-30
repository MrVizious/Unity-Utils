using UnityEngine;

namespace ExtensionMethods
{

    public static class ColorExtensionMethods
    {

        public static Color WithAlpha(this Color value, float newAlpha)
        {
            value.a = newAlpha;
            return value;
        }

        public static Color WithRed(this Color value, float newRed)
        {
            value.r = newRed;
            return value;
        }

        public static Color WithGreen(this Color value, float newGreen)
        {
            value.g = newGreen;
            return value;
        }
        public static Color WithBlue(this Color value, float newBlue)
        {
            value.b = newBlue;
            return value;
        }
    }

}