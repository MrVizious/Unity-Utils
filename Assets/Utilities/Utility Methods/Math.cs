namespace UtilityMethods
{
    public static class Math
    {

        /// <summary>
        /// Takes a value from a given input range and maps it to a new value within a desired output range
        /// </summary>
        /// <param name="value">Current input value</param>
        /// <param name="inputMinValue">Minimum value in input range</param>
        /// <param name="inputMaxValue">Maximum value in input range</param>
        /// <param name="outputMinValue">Minimum value in output range</param>
        /// <param name="outputMaxValue">Maximum value in output range</param>
        /// <returns></returns>
        public static float Remap(this float value,
                                  float inputMinValue, float inputMaxValue,
                                  float outputMinValue, float outputMaxValue)
        {
            return (value - inputMinValue) / (inputMaxValue - inputMinValue) * (outputMaxValue - outputMinValue) + outputMinValue;
        }
    }
}