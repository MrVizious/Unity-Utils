using UnityEngine;

namespace ExtensionMethods
{
    public static class RectTransformExtensionMethods
    {
        public static UnityEngine.Vector2 GetBoundingBox(this RectTransform rect)
        {
            Vector3[] worldCorners = new Vector3[4];
            rect.GetWorldCorners(worldCorners);

            // Initialize min and max values
            float minX = worldCorners[0].x;
            float maxX = worldCorners[0].x;
            float minY = worldCorners[0].y;
            float maxY = worldCorners[0].y;

            // Iterate through the corners to find the min and max values
            for (int i = 1; i < 4; i++)
            {
                if (worldCorners[i].x < minX) minX = worldCorners[i].x;
                if (worldCorners[i].x > maxX) maxX = worldCorners[i].x;
                if (worldCorners[i].y < minY) minY = worldCorners[i].y;
                if (worldCorners[i].y > maxY) maxY = worldCorners[i].y;
            }

            // Calculate the width and height of the bounding box
            float width = maxX - minX;
            float height = maxY - minY;
            return new Vector2(width, height);
        }
    }
}