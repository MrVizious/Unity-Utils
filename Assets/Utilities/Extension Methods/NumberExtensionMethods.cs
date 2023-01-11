using UnityEngine;

namespace ExtensionMethods
{

    public static class NumberExtensionMethods
    {

        public static int WithRandomSign(this int value, float negativeProbability = 0.5f)
        {
            return Random.value < negativeProbability ? -value : value;
        }
        public static float WithRandomSign(this float value, float negativeProbability = 0.5f)
        {
            return Random.value < negativeProbability ? -value : value;
        }
    }

}