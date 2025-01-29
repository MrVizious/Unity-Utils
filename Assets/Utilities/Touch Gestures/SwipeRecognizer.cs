using UnityEngine;
using UltEvents;
using Sirenix.OdinInspector;

public class SwipeRecognizer : DesignPatterns.Singleton<SwipeRecognizer>
{
    protected override bool keepOldestInstance => false;
    [Tooltip("Minimum swipe distance in pixels to trigger the swipe event.")]
    public float minSwipeDistance = 50f;

    [Tooltip("Event triggered when a swipe is performed. Returns a Vector2 representing the swipe distance (end - start).")]
    public LaunchableUltEvent<Vector2> onSwipeUpdated = new LaunchableUltEvent<Vector2>();
    public LaunchableUltEvent<Vector2> onSwipeEnded = new LaunchableUltEvent<Vector2>();

    private Vector2 startTouchPosition;
    private bool isSwiping;

    private void Update()
    {
        // Check if there is at least one touch
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 endTouchPosition = touch.position;
            Vector2 swipeDistance = endTouchPosition - startTouchPosition;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Start tracking the swipe gesture
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    onSwipeUpdated?.Invoke(swipeDistance);
                    break;
                case TouchPhase.Stationary:
                    // Continue tracking (optional, depending on your needs)
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        // Only trigger the event if the swipe distance exceeds the threshold
                        if (swipeDistance.magnitude >= minSwipeDistance)
                        {
                            onSwipeEnded?.Invoke(swipeDistance);
                        }

                        isSwiping = false;
                    }
                    break;

                case TouchPhase.Canceled:
                    // Reset swipe if the touch is canceled
                    isSwiping = false;
                    break;
            }
        }
    }
}
