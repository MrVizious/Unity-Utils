using UnityEngine;

namespace ExtensionMethods
{

    public static class RigidbodyExtensionMethods
    {
        public static void NormalizeVelocity(this Rigidbody rb, float magnitude = 1)
        {
#if UNITY_6000_0_OR_NEWER
            rb.linearVelocity = rb.linearVelocity.normalized * magnitude;
#else
            rb.velocity = rb.velocity.normalized * magnitude;
#endif
        }

        /// <summary>
        /// Sets velocity to be in the current magnitude but in the given direction
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="direction"></param>
        public static void ChangeDirection(this Rigidbody rb, Vector3 direction)
        {
#if UNITY_6000_0_OR_NEWER
            rb.linearVelocity = direction.normalized * rb.linearVelocity.magnitude;
#else
            rb.velocity = direction.normalized * rb.velocity.magnitude;
#endif
        }

        /// <summary>
        /// Sets velocity to 0
        /// </summary>
        /// <param name="rb"></param>
        public static void Stop(this Rigidbody rb)
        {
#if UNITY_6000_0_OR_NEWER
            rb.linearVelocity = Vector3.zero;
#else
            rb.velocity = Vector3.zero;
#endif
        }

        public static void MovePositionInDirection(this Rigidbody rb, Vector3 direction)
        {
            rb.MovePosition(rb.transform.position + direction);
        }

        public static void MovePositionInDirection(this Rigidbody2D rb, Vector2 direction)
        {
            rb.MovePosition(rb.transform.position + (Vector3)direction);
        }
    }
}