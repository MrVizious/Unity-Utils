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
        /// Replaces the current x or/and y values by the provided ones.
        /// </summary>
        /// <param name="v">Vector2 to change values from</param>
        /// <param name="x">float to place in the x value of v</param>
        /// <param name="y">float to place in the y value of v</param>
        /// <returns></returns>
        public static Vector2 With(this Vector2 v, float? x = null, float? y = null)
        {
            return new Vector2(x ?? v.x, y ?? v.y);
        }

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

        /// <summary>
        /// Returns the angle in a left hand system between two angles
        /// </summary>
        /// <param name="firstVector"></param>
        /// <param name="secondVector"></param>
        /// <returns></returns>
        public static float Angle360To(this Vector2 firstVector, Vector2 secondVector)
        {
            float signedAngle = Vector2.SignedAngle(secondVector, firstVector);
            if (signedAngle < 0)
            {
                signedAngle = 360 + signedAngle;
            }
            return signedAngle;
        }


        /// <summary>
        /// Turns a Vector3 array into a Vector2 array
        /// </summary>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector2[] toVector2Array(this Vector3[] v3)
        {
            return System.Array.ConvertAll<Vector3, Vector2>(v3, toVector2);
        }

        public static Vector2 toVector2(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }
        public static Vector2Int toVector2Int(this Vector2 v2)
        {
            return new Vector2Int((int)v2.x, (int)v2.y);
        }

        public static Vector2 Abs(this Vector2 vector2)
        {
            return new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
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
        /// Replaces the current x or/and y values by the provided ones.
        /// </summary>
        /// <param name="v">Vector2 to change values from</param>
        /// <param name="x">float to place in the x value of v</param>
        /// <param name="y">float to place in the y value of v</param>
        /// <param name="z">float to place in the z value of v</param>
        /// <returns></returns>
        public static Vector3 With(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? v.x, y ?? v.y, z ?? v.z);
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


        /// <summary>
        /// Turns a Vector3 array into a Vector2 array
        /// </summary>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3[] toVector3Array(this Vector2[] v2)
        {
            return System.Array.ConvertAll<Vector2, Vector3>(v2, toVector3);
        }

        public static Vector3 toVector3(this Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0f);
        }
        public static Vector3Int toVector3Int(this Vector3 v3)
        {
            return new Vector3Int((int)v3.x, (int)v3.y, (int)v3.z);
        }
        public static Vector3 Abs(this Vector3 vector3)
        {
            return new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
        }
        #endregion
    }
}
