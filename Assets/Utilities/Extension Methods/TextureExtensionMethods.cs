using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class TextureExtensionMethods
    {
        public static Sprite ToSprite(this Texture2D texture)
        {
            if (texture == null) throw new ArgumentNullException("The given texture is null, it can't be turned into a Sprite");
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }


        /// <summary>
        /// Scales the Texture2D to the specified width and height.
        /// </summary>
        /// <param name="source">The source Texture2D.</param>
        /// <param name="newWidth">The desired width.</param>
        /// <param name="newHeight">The desired height.</param>
        /// <returns>A new scaled Texture2D.</returns>
        public static Texture2D Scale(this Texture2D source, int newWidth, int newHeight)
        {
            // Adjust dimensions to be multiples of 4
            newWidth = Mathf.CeilToInt(newWidth / 4f) * 4;
            newHeight = Mathf.CeilToInt(newHeight / 4f) * 4;

            Texture2D scaledTexture = new Texture2D(newWidth, newHeight, source.format, false);
            Color[] newPixels = new Color[newWidth * newHeight];

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    float xRatio = x / (float)newWidth;
                    float yRatio = y / (float)newHeight;
                    int srcX = Mathf.FloorToInt(xRatio * source.width);
                    int srcY = Mathf.FloorToInt(yRatio * source.height);

                    newPixels[y * newWidth + x] = source.GetPixel(srcX, srcY);
                }
            }

            scaledTexture.SetPixels(newPixels);
            scaledTexture.Apply();
            return scaledTexture;
        }

        /// <summary>
        /// Scales the Texture2D by a given scale factor.
        /// </summary>
        /// <param name="source">The source Texture2D.</param>
        /// <param name="scaleFactor">The factor to scale the texture. Must be greater than 0.</param>
        /// <returns>A new scaled Texture2D.</returns>
        public static Texture2D Scale(this Texture2D source, float scaleFactor)
        {
            if (scaleFactor <= 0f)
            {
                Debug.LogError("Scale factor must be greater than 0.");
                return source;
            }
            if (scaleFactor == 1f)
            {
                Debug.Log("Scale factor is 1, so no modification has to be made");
                return source;
            }
            if (!source.isReadable)
            {
                throw new System.InvalidOperationException($"Texture '{source.name}' is not readable. Make sure the texture is marked as readable in import settings.");
            }

            int newWidth = Mathf.Max(1, Mathf.RoundToInt(source.width * scaleFactor));
            int newHeight = Mathf.Max(1, Mathf.RoundToInt(source.height * scaleFactor));

            return source.Scale(newWidth, newHeight);
        }

    }
}
