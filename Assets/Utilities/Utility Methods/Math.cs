using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
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

        public static Vector2 PolarToCartesianClockwise(float radius, float angle)
        {
            return PolarToCartesianCounterclockwise(radius, -angle);
        }
        public static Vector2 PolarToCartesianCounterclockwise(float radius, float angle)
        {
            angle = angle * Mathf.Deg2Rad;
            Vector2 returnVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            return returnVector * radius;
        }
    }

}