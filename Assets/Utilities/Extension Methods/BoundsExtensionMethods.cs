using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ExtensionMethods
{

    public static class BoundsExtensionMethods
    {

        public enum BoundsSpace
        {
            Local,
            World
        }

        public enum VertexPosition
        {
            BottomLeftFront,
            BottomRightFront,
            BottomLeftBack,
            BottomRightBack,
            TopLeftFront,
            TopRightFront,
            TopLeftBack,
            TopRightBack
        }


        /// <summary>
        /// <code>
        ///    6           max(7)
        ///    +-----------+
        ///   /|          /|
        /// 4/ |        5/ |
        /// +-----------+  |
        /// |  |2       |  |
        /// |  +--------|--+ 3
        /// | /         | /
        /// |/          |/
        /// +-----------+
        /// min(0)      1
        /// </code>
        /// </summary>
        public static Vector3[] GetVertices(this Bounds bounds)
        {
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            Vector3 v0 = min;
            Vector3 v1 = new Vector3(max.x, min.y, min.z);
            Vector3 v2 = new Vector3(min.x, min.y, max.z);
            Vector3 v3 = new Vector3(max.x, min.y, max.z);
            Vector3 v4 = new Vector3(min.x, max.y, min.z);
            Vector3 v5 = new Vector3(max.x, max.y, min.z);
            Vector3 v6 = new Vector3(min.x, max.y, max.z);
            Vector3 v7 = max;

            Vector3[] returnArray = { v0, v1, v2, v3, v4, v5, v6, v7 };
            return returnArray;
        }

        public static Vector3 GetLocalVertex(this Bounds bounds, VertexPosition vertexPosition, BoundsSpace boundsSpace, Transform transform = null)
        {
            if (boundsSpace == BoundsSpace.World && transform == null)
            {
                throw new ArgumentException("Can't get local vertex from a world space bounds if no Transform is supplied");
            }
            Vector3 vertex;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            switch (vertexPosition)
            {
                case VertexPosition.BottomLeftFront:
                    vertex = min;
                    break;
                case VertexPosition.BottomRightFront:
                    vertex = new Vector3(max.x, min.y, min.z);
                    break;
                case VertexPosition.BottomLeftBack:
                    vertex = new Vector3(min.x, min.y, max.z);
                    break;
                case VertexPosition.BottomRightBack:
                    vertex = new Vector3(max.x, min.y, max.z);
                    break;
                case VertexPosition.TopLeftFront:
                    vertex = new Vector3(min.x, max.y, min.z);
                    break;
                case VertexPosition.TopRightFront:
                    vertex = new Vector3(max.x, max.y, min.z);
                    break;
                case VertexPosition.TopLeftBack:
                    vertex = new Vector3(min.x, max.y, max.z);
                    break;
                case VertexPosition.TopRightBack:
                    vertex = max;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid vertex position " + nameof(vertexPosition));
            }
            if (boundsSpace == BoundsSpace.World)
            {
                vertex = transform.InverseTransformPoint(vertex);
            }

            return vertex;
        }

        public static Vector3 GetWorldVertex(this Bounds bounds, VertexPosition vertexPosition, BoundsSpace boundsSpace, Transform transform = null)
        {
            if (boundsSpace == BoundsSpace.Local && transform == null)
            {
                throw new ArgumentException("Can't get world vertex from a local space bounds if no Transform is supplied");
            }
            Vector3 vertex;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            switch (vertexPosition)
            {
                case VertexPosition.BottomLeftFront:
                    vertex = min;
                    break;
                case VertexPosition.BottomRightFront:
                    vertex = new Vector3(max.x, min.y, min.z);
                    break;
                case VertexPosition.BottomLeftBack:
                    vertex = new Vector3(min.x, min.y, max.z);
                    break;
                case VertexPosition.BottomRightBack:
                    vertex = new Vector3(max.x, min.y, max.z);
                    break;
                case VertexPosition.TopLeftFront:
                    vertex = new Vector3(min.x, max.y, min.z);
                    break;
                case VertexPosition.TopRightFront:
                    vertex = new Vector3(max.x, max.y, min.z);
                    break;
                case VertexPosition.TopLeftBack:
                    vertex = new Vector3(min.x, max.y, max.z);
                    break;
                case VertexPosition.TopRightBack:
                    vertex = max;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid vertex position " + nameof(vertexPosition));
            }
            if (boundsSpace == BoundsSpace.Local)
            {
                vertex = transform.TransformPoint(vertex);
                //vertex = transform.rotation * vertex;
            }

            return vertex;
        }

        public static void Visualize(this Bounds bounds, Color color, BoundsSpace boundsSpace, Transform transform = null)
        {
            Color originalColor = Gizmos.color;
            if (boundsSpace == BoundsSpace.Local && transform == null)
            {
                throw new ArgumentException("Can't get world vertex from a local space bounds if no Transform is supplied");
            }
            Gizmos.color = color;

            Vector3[] vertices = new Vector3[8];
            vertices[0] = bounds.GetWorldVertex(VertexPosition.BottomLeftFront, boundsSpace, transform);
            vertices[1] = bounds.GetWorldVertex(VertexPosition.BottomRightFront, boundsSpace, transform);
            vertices[2] = bounds.GetWorldVertex(VertexPosition.BottomLeftBack, boundsSpace, transform);
            vertices[3] = bounds.GetWorldVertex(VertexPosition.BottomRightBack, boundsSpace, transform);
            vertices[4] = bounds.GetWorldVertex(VertexPosition.TopLeftFront, boundsSpace, transform);
            vertices[5] = bounds.GetWorldVertex(VertexPosition.TopRightFront, boundsSpace, transform);
            vertices[6] = bounds.GetWorldVertex(VertexPosition.TopLeftBack, boundsSpace, transform);
            vertices[7] = bounds.GetWorldVertex(VertexPosition.TopRightBack, boundsSpace, transform);

            // Draw bottom square
            Gizmos.DrawLine(vertices[0], vertices[1]);
            Gizmos.DrawSphere(vertices[0], 0.01f);
            Gizmos.DrawLine(vertices[1], vertices[3]);
            Gizmos.DrawLine(vertices[3], vertices[2]);
            Gizmos.DrawLine(vertices[2], vertices[0]);

            // Draw top square
            Gizmos.DrawLine(vertices[4], vertices[5]);
            Gizmos.DrawLine(vertices[5], vertices[7]);
            Gizmos.DrawLine(vertices[7], vertices[6]);
            Gizmos.DrawLine(vertices[6], vertices[4]);

            // Draw vertical lines (sides)
            Gizmos.DrawLine(vertices[0], vertices[4]);
            Gizmos.DrawLine(vertices[1], vertices[5]);
            Gizmos.DrawLine(vertices[2], vertices[6]);
            Gizmos.DrawLine(vertices[3], vertices[7]);

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(vertices[0], 0.05f);
            Gizmos.DrawSphere(vertices[7], 0.05f);


            Gizmos.color = originalColor;
        }

    }
}
