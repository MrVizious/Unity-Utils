// Created by: Javier Riera (https://mrvizious.github.io)
using Sirenix.Serialization;
using UltEvents;
using UnityEngine;

public class PinchRecognizer : DesignPatterns.Singleton<PinchRecognizer>
{
    protected override bool keepOldestInstance => false;
    [Tooltip("Maximum pinch distance as a percentage of the screen width (e.g., 0.5 for 50%).")]
    [Range(0.1f, 1f)]
    public float maxDistancePercentage = 0.5f;

    [Tooltip("Event triggered when a pinch is performed. Returns a float value between 0 and 1.")]
    [PreviouslySerializedAs("onPinchPerformed")]
    public LaunchableUltEvent<float> onPinchDeltaChanged = new LaunchableUltEvent<float>();


    private float previousDistance; // Previous frame's pinch distance

    public bool isPinching { get; private set; }       // Whether a pinch gesture is being performed
    private float maxDistance;     // Calculated maximum distance in pixels
    [Range(1f, 100f)]
    private float clampedDeltaMagnitude = 50f;

    private void Start()
    {
        // Calculate the maximum distance based on screen width and the percentage
        float biggestSide = Mathf.Max(Screen.width, Screen.height);
        maxDistance = biggestSide * maxDistancePercentage;
    }

    private void Update()
    {
        if (Input.touchCount < 2)
        {
            isPinching = false;
            return;
        }
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        // Calculate the current distance between the two touches
        float currentDistance = Vector2.Distance(touch1.position, touch2.position);

        if (!isPinching)
        {
            // First frame of pinch, initialize values but don't trigger events
            previousDistance = currentDistance;
            isPinching = true;
            return;
        }

        // Calculate the difference from the previous frame
        float deltaChange = (currentDistance - previousDistance) / maxDistance;

        // Update previous distance for the next frame
        previousDistance = currentDistance;

        // Fire events
        if (Mathf.Abs(deltaChange) > 0.0001f) // Avoid tiny floating-point updates
        {
            onPinchDeltaChanged?.Invoke(Mathf.Clamp(deltaChange, -clampedDeltaMagnitude, clampedDeltaMagnitude));
        }
        else
        {
            // Reset pinch state if fewer than two touches are detected
            isPinching = false;
        }
    }
}