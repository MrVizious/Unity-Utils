using System.Linq;
using UnityEngine;

namespace ExtensionMethods
{
    public static class MeshExtensionMethods
    {
        public static void FlipNormals(this Mesh mesh, bool recalculateNormals = true)
        {
            mesh.triangles = mesh.triangles.Reverse().ToArray();
            if (recalculateNormals) mesh.RecalculateNormals();
        }
    }
}