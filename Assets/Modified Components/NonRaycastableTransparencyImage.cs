using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;

public class NonRaycastableTransparencyImage : Image
{
    protected override void OnEnable()
    {
        base.OnEnable();
        alphaHitTestMinimumThreshold = 0.001f;
        if (!sprite.texture.isReadable) Debug.LogWarning("Texture is not read/write enabled! The transparency check won't work!");
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        bool result = base.IsRaycastLocationValid(screenPoint, eventCamera) && color.a >= alphaHitTestMinimumThreshold;
        if (!result)
        {
            return false;
        }
        if (this.fillMethod == FillMethod.Radial360)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out localPoint);
            Vector2 radialFillOriginVector;
            switch (this.fillOrigin)
            {
                // Bottom
                default:
                    radialFillOriginVector = Vector2.down;
                    break;
                // Right
                case 1:
                    radialFillOriginVector = Vector2.right;
                    break;
                // Top
                case 2:
                    radialFillOriginVector = Vector2.up;
                    break;
                // Left
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
            if (clickAngle <= fillAmount * 360f) return true;
            return false;
        }

        // TODO: Add support for other fillable image types
        return false;
    }
}