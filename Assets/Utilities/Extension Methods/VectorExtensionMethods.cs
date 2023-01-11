using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{

    public static class VectorExtensionMethods
    {

        //
        //  SECTION: Vector2
        //
        #region Vector2
        /// <summary>
        /// Replaces the x component in the vector by the provided float x
        /// </summary>
        /// <param name="v">Vector2 to change x value from</param>
        /// <param name="x">float to place in the x value of v</param>
        /// <returns></returns>
        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        /// <summary>
        /// Replaces the y component in the vector by the provided float y
        /// </summary>
        /// <param name="v">Vector2 to change y value from</param>
        /// <param name="y">float to place in the y value of v</param>
        /// <returns></returns>
        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        /// <summary>
        /// Creates a new Vector3 by adding the given z value in the z position
        /// </summary>
        /// <param name="v">Vector2 to transform into Vector3</param>
        /// <param name="z">float to place in the z value of v</param>
        /// <returns></returns>
        public static Vector3 WithZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        /// <summary>
        /// Finds the position closest to the given one.
        /// </summary>
        /// <param name="otherPositions">Other positions in the same space</param>
        /// <returns>Closest position.</returns>
        public static Vector2 GetClosest(this Vector2 position, IEnumerable<Vector3> otherPositions)
        {
            Vector2 closest = Vector2.zero;
            float shortestDistance = Mathf.Infinity;

            foreach (Vector2 otherPosition in otherPositions)
            {
                float distance = Vector2.Distance(otherPosition, position);

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest;
        }
        #endregion


        //
        //  SECTION: Vector3
        //
        #region Vector3
        /// <summary>
        /// Transforms a Vector3 into a Vector2 with the original xy components
        /// </summary>
        /// <param name="v"></param>
        /// <returns>New Vector2 with the xy components from the Vector3</returns>
        public static Vector2 xy(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        /// <summary>
        /// Replaces the x component in the vector by the provided float x
        /// </summary>
        /// <param name="v">Vector3 to change x value from</param>
        /// <param name="x">float to place in the x value of v</param>
        /// <returns></returns>
        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        /// <summary>
        /// Replaces the y component in the vector by the provided float y
        /// </summary>
        /// <param name="v">Vector3 to change y value from</param>
        /// <param name="y">float to place in the y value of v</param>
        /// <returns></returns>
        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        /// <summary>
        /// Replaces the z component in the vector by the provided float z
        /// </summary>
        /// <param name="v">Vector3 to change z value from</param>
        /// <param name="z">float to place in the z value of v</param>
        /// <returns></returns>
        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        /// <summary>
        /// Finds the position closest to the given one.
        /// </summary>
        /// <param name="otherPositions">Other positions in the same space</param>
        /// <returns>Closest position.</returns>
        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            Vector3 closest = Vector3.zero;
            float shortestDistance = Mathf.Infinity;

            foreach (Vector3 otherPosition in otherPositions)
            {
                float distance = Vector3.Distance(otherPosition, position);

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest;
        }
        #endregion
    }
}
