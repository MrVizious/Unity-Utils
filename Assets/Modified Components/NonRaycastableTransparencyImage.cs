using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NonRaycastableTransparencyImage : Image
{
    public bool debug = false;
    public UnityEvent onSpriteChanged = new UnityEvent();

    public new Sprite sprite
    {
        get => base.sprite;
        set
        {
            if (base.sprite != value)
            {
                base.sprite = value;
                onSpriteChanged.Invoke();
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        alphaHitTestMinimumThreshold = 0.01f;
        if (debug)
        {
            Debug.Log($"Alpha threshold: {alphaHitTestMinimumThreshold}", this);
        }

        // Check if sprite is null or texture is not readable
        if (sprite == null) return;
        if (!sprite.texture.isReadable)
        {
            Debug.LogWarning("Texture is not read/write enabled! The transparency check won't work!");
        }

#if UNITY_EDITOR
        // Ensure the sprite is using FullRect and the max size is adjusted
        EnsureProperSettings();
#endif
    }

#if UNITY_EDITOR
    public void EnsureProperSettings()
    {
        if (sprite == null)
        {
            Debug.LogError("Sprite is not assigned.");
            return;
        }

        // Get the asset path of the sprite's texture
        string assetPath = AssetDatabase.GetAssetPath(sprite.texture);

        // Load the texture importer for the sprite's texture
        TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

        if (textureImporter == null)
        {
            Debug.LogError("Failed to load TextureImporter.");
            return;
        }

        // Ensure Read/Write is enabled
        if (!textureImporter.isReadable)
        {
            textureImporter.isReadable = true;
            Debug.Log("Read/Write was enabled for the texture.");
        }

        // Read the current import settings for the texture
        TextureImporterSettings textureSettings = new TextureImporterSettings();
        textureImporter.ReadTextureSettings(textureSettings);

        // Check if the sprite is using Full Rect mesh type
        if (textureSettings.spriteMeshType != SpriteMeshType.FullRect)
        {
            // If not, change it to Full Rect
            textureSettings.spriteMeshType = SpriteMeshType.FullRect;
            textureImporter.SetTextureSettings(textureSettings);
            Debug.Log("Sprite mesh type was set to Full Rect.");
        }

        // Get the actual size of the full texture (not the sprite's size if it's part of a multi-sprite texture)
        int width = sprite.texture.width;   // This is the full texture size
        int height = sprite.texture.height; // This is the full texture size

        // Calculate the required max size (must be a power of 2)
        int requiredMaxSize = Mathf.Max(width, height);
        int nextPowerOfTwo = Mathf.NextPowerOfTwo(requiredMaxSize);

        // Adjust max texture size if necessary
        if (textureImporter.maxTextureSize != nextPowerOfTwo)
        {
            textureImporter.maxTextureSize = nextPowerOfTwo;
            Debug.Log($"Max texture size adjusted to {nextPowerOfTwo} to fit the texture dimensions ({width}x{height}).");
        }

        // Apply the changes by re-importing the asset
        EditorUtility.SetDirty(textureImporter);
        textureImporter.SaveAndReimport();
    }
#endif




    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        // General alpha threshold check for the whole image
        if (color.a < alphaHitTestMinimumThreshold || sprite == null || !sprite.texture.isReadable)
        {
            return false;
        }

        // Convert the screen point to local coordinates relative to the RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out Vector2 localPoint);

        // Check the pixel alpha value at the clicked location
        Vector2 uv = LocalPointToUV(localPoint);
        Color pixelColor = GetPixelColor(uv);
        if (debug)
        {
            Debug.Log($"Pixel color is {pixelColor}", this);
        }
        if (pixelColor.a < alphaHitTestMinimumThreshold)
        {
            return false; // Pixel is too transparent
        }

        // Perform radial fill method checks
        if (!IsInFilledArea(localPoint))
        {
            return false; // Click is outside the filled area
        }

        if (debug)
        {
            Debug.Log($"Is raycast valid location", this);
        }
        // If all checks pass, return true
        return true;
    }

    private bool IsInFilledArea(Vector2 localPoint)
    {
        // Handle the Radial360 fill method check
        if (this.fillMethod == FillMethod.Radial360)
        {
            Vector2 radialFillOriginVector;
            switch (this.fillOrigin)
            {
                default:
                    radialFillOriginVector = Vector2.down;
                    break;
                case 1:
                    radialFillOriginVector = Vector2.right;
                    break;
                case 2:
                    radialFillOriginVector = Vector2.up;
                    break;
                case 3:
                    radialFillOriginVector = Vector2.left;
                    break;
            }

            float clickAngle = 0f;
            if (fillClockwise)
            {
                clickAngle = radialFillOriginVector.Angle360To(localPoint);
            }
            else
            {
                clickAngle = localPoint.Angle360To(radialFillOriginVector);
            }

            if (clickAngle > fillAmount * 360f)
            {
                return false; // Click is outside the filled area
            }
        }

        return true;
    }

    private Vector2 LocalPointToUV(Vector2 localPoint)
    {
        // Convert local point to normalized UV coordinates based on the RectTransform
        float x = (localPoint.x + rectTransform.pivot.x * rectTransform.rect.width) / rectTransform.rect.width;
        float y = (localPoint.y + rectTransform.pivot.y * rectTransform.rect.height) / rectTransform.rect.height;

        // Flip the Y-axis to match texture coordinates (UV origin is bottom-left)
        return new Vector2(x, y);
    }

    private Color GetPixelColor(Vector2 uv)
    {
        // Get the pixel position on the texture
        Texture2D texture = sprite.texture;
        Rect textureRect = sprite.textureRect;

        // Convert UV coordinates to texture pixel coordinates
        int x = Mathf.FloorToInt(uv.x * textureRect.width + textureRect.x);
        int y = Mathf.FloorToInt(uv.y * textureRect.height + textureRect.y);

        // Ensure the coordinates are within the texture bounds
        if (x < 0 || x >= texture.width || y < 0 || y >= texture.height)
            return new Color(0, 0, 0, 0); // Return fully transparent if out of bounds

        // Return the pixel color at the calculated position
        return texture.GetPixel(x, y);
    }
}
