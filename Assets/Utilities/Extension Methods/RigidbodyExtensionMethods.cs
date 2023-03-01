using UnityEngine;

namespace ExtensionMethods
{

    public static class RigidbodyExtensionMethods
    {
        public static void NormalizeVelocity(this Rigidbody rb, float magnitude = 1)
        {
            rb.velocity = rb.velocity.normalized * magnitude;
        }

        /// <summary>
        /// Sets velocity to be in the current magnitude but in the given direction
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="direction"></param>
        public static void ChangeDirection(this Rigidbody rb, Vector3 direction)
        {
            rb.velocity = direction.normalized * rb.velocity.magnitude;
        }

        /// <summary>
        /// Sets velocity to 0
        /// </summary>
        /// <param name="rb"></param>
        public static void Stop(this Rigidbody rb)
        {
            rb.velocity = Vector3.zero;
        }
    }
}