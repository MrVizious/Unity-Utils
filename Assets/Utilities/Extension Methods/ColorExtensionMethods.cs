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

        public static Color RandomColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        public static Color FromString(string colorString)
        {
            Color returnColor;
            if (ColorUtility.TryParseHtmlString(colorString, out returnColor))
            {
                return returnColor;
            }
            return Color.magenta;
        }
    }

}