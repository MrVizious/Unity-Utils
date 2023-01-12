/*
 * 
 * Code by Javier Riera Chirivella
 * Inspired in this video from the GDC 2016
 * https://www.youtube.com/watch?v=tu-Qe66AvtY&t=518s&ab_channel=GDC
 * by Squirrel Eiserloh
 *
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraShake : MonoBehaviour
{
    public bool decreaseTrauma = true;
    public float decreaseTraumaRate = 0.1f;
    public float maxAngle = 10f;
    public float maxOffset = 1f;
    public float timeMultiplier = 5f;
    public TraumaScalation traumaScalation;
    public Dimension dimension;

    private Transform pivot;

    [SerializeField]
    private float _trauma;
    public float trauma
    {
        get
        {
            return _trauma;
        }
        set
        {
            _trauma = Mathf.Clamp01(value);
        }
    }
    public enum TraumaScalation
    {
        Linear,
        Squared,
        Cubed
    }
    public enum Dimension
    {
        _2D,
        _3D
    }

    private void Start()
    {
        if (pivot == null)
        {
            pivot = transform.parent;
        }
    }

    private void Update()
    {
        float traumaScaled;
        switch (traumaScalation)
        {
            case TraumaScalation.Linear:
                traumaScaled = trauma;
                break;
            case TraumaScalation.Squared:
                traumaScaled = trauma * trauma;
                break;
            case TraumaScalation.Cubed:
                traumaScaled = trauma * trauma * trauma;
                break;
            default:
                traumaScaled = trauma;
                break;
        }
        transform.rotation = pivot.rotation;
        transform.position = pivot.position;

        switch (dimension)
        {
            case Dimension._3D:
                // Rotate up
                transform.RotateAround(pivot.position, pivot.up,
                                           (Mathf.PerlinNoise(Time.time * timeMultiplier + 0, Time.time * timeMultiplier) - 0.5f)
                                           * maxAngle * traumaScaled);
                // Rotate right
                transform.RotateAround(pivot.position, pivot.right,
                                           (Mathf.PerlinNoise(Time.time * timeMultiplier + 1, Time.time * timeMultiplier) - 0.5f)
                                           * maxAngle * traumaScaled);
                break;
            case Dimension._2D:
                // Change position
                transform.position = pivot.position +
                    (Vector3)new Vector2(
                        Mathf.PerlinNoise(Time.time * timeMultiplier + 0, Time.time * timeMultiplier) - 0.5f,
                        Mathf.PerlinNoise(Time.time * timeMultiplier + 1, Time.time * timeMultiplier) - 0.5f
                    ) * maxOffset * traumaScaled;
                break;
        }
        // Rotate forward
        transform.RotateAround(pivot.position, pivot.forward,
                                   (Mathf.PerlinNoise(Time.time * timeMultiplier + 2, Time.time * timeMultiplier) - 0.5f)
                                   * maxAngle * traumaScaled);



        if (decreaseTrauma)
        {
            trauma -= decreaseTraumaRate * Time.deltaTime;
        }
    }

    public void AddTrauma(float amountToIncrease)
    {
        trauma += amountToIncrease;
    }

    public void ResetTrauma()
    {
        trauma = 0;
    }
}




#if UNITY_EDITOR
[CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CameraShake cs = (CameraShake)target;
        if (GUILayout.Button("Max Shake"))
        {
            cs.trauma = 1;
        }
        if (GUILayout.Button("Small Shake"))
        {
            cs.trauma += 0.2f;
        }
        if (GUILayout.Button("Big Shake"))
        {
            cs.trauma += 0.4f;
        }
    }
}
#endif