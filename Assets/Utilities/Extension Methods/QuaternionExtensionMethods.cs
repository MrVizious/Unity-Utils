using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{

    public static class QuaternionExtensionMethods
    {

        public static Quaternion CopyFromQuaternion(Quaternion quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        public static bool SimilarTo(this Quaternion quaternionA, Quaternion quaternionB, float acceptablePercentageDifference)
        {
            return (1 - Mathf.Abs(Quaternion.Dot(quaternionA, quaternionB)) < acceptablePercentageDifference);
        }
    }
}
