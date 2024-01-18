using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{

    public static class BoundsExtensionMethods
    {

        /// <summary>
        ///    4           6
        ///    +-----------+
        ///   /|          /|
        /// 5/ |         / |
        /// +-----------+max(7)
        /// |  |min(0)  |  |
        /// |  +--------|--+2
        /// | /         | /
        /// |/          |/
        /// +-----------+
        /// 1           3
        /// </summary> <summary>
        ///
        /// </summary>
        public static Vector3[] vertices(this Bounds bounds)
        {
            // Min and max
            Vector3 v0 = bounds.min;
            Vector3 v7 = bounds.max;

            // Lower part
            Vector3 v1 = new Vector3(v0.x, v0.y, v7.z);
            Vector3 v2 = new Vector3(v7.x, v0.y, v0.z);
            Vector3 v3 = new Vector3(v7.x, v0.y, v7.z);

            // Upper part
            Vector3 v4 = new Vector3(v0.x, v7.y, v0.z);
            Vector3 v5 = new Vector3(v0.x, v7.y, v7.z);
            Vector3 v6 = new Vector3(v7.x, v7.y, v0.z);

            Vector3[] returnArray = { v0, v1, v2, v3, v4, v5, v6, v7 };
            return returnArray;
        }
    }
}
