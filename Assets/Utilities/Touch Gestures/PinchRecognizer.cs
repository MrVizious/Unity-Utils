// Created by: Javier Riera (https://mrvizious.github.io)
using UltEvents;
using UnityEngine;

public class PinchRecognizer : DesignPatterns.Singleton<PinchRecognizer>
{
    protected override bool keepOldestInstance => false;
    [Tooltip("Maximum pinch distance as a percentage of the screen width (e.g., 0.5 for 50%).")]
    [Range(0.1f, 1f)]
    public float maxDistancePercentage = 0.5f;

    [Tooltip("Event triggered when a pinch is performed. Returns a float value between 0 and 1.")]
    public LaunchableUltEvent<float> onPinchPerformed = new LaunchableUltEvent<float>();


    private float initialDistance; // The initial distance between two fingers
    private bool isPinching;       // Whether a pinch gesture is being performed
    private float maxDistance;     // Calculated maximum distance in pixels

    private void Start()
    {
        // Calculate the maximum distance based on screen width and the percentage
        float biggestSide = Mathf.Max(Screen.width, Screen.height);
        maxDistance = biggestSide * maxDistancePercentage;
    }

    private void Update()
    {
        if (Input.touchCount == 2) // Only proceed if exactly two touches are detected
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calculate the current distance between the two touches
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            // Initialize the pinch gesture
            if (!isPinching)
            {
                initialDistance = currentDistance;
                isPinching = true;
            }

            // Calculate the pinch delta relative to the initial distance
            float pinchDelta = Mathf.Clamp01((currentDistance - initialDistance) / maxDistance);

            // Invoke the event with the normalized pinch value
            onPinchPerformed?.Invoke(pinchDelta);
        }
        else
        {
            // Reset pinch state if fewer than two touches are detected
            isPinching = false;
        }
    }
}