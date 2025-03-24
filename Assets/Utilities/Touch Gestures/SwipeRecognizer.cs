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

    [Tooltip("Event triggered when a swipe delta occurs. Returns a Vector2 representing the per-frame swipe movement.")]
    public LaunchableUltEvent<Vector2> onSwipeDeltaChanged = new LaunchableUltEvent<Vector2>();

    public LaunchableUltEvent<Vector2> onSwipeEnded = new LaunchableUltEvent<Vector2>();

    private Vector2 startTouchPosition;
    private Vector2 lastTouchPosition;

    [Range(1f, 100f)]
    public float clampedDeltaMagnitude = 50f;

    public bool isSwiping { get; private set; }
    private bool wasMultiTouch = false; // Tracks if multi-touch occurred

    private void Update()
    {
        int touchCount = Input.touchCount;

        // If more than one finger is detected, mark multi-touch and cancel any swipe
        if (touchCount > 1)
        {
            wasMultiTouch = true;
            isSwiping = false;
            return;
        }

        // If no fingers are touching the screen, reset the multi-touch flag
        if (touchCount == 0)
        {
            wasMultiTouch = false;
            return;
        }

        // If multi-touch occurred previously, prevent swiping until all fingers are lifted
        if (wasMultiTouch) return;

        Touch touch = Input.GetTouch(0);
        Vector2 endTouchPosition = touch.position;
        Vector2 swipeDistance = endTouchPosition - startTouchPosition;
        Vector2 swipeDelta = Vector2.ClampMagnitude(endTouchPosition - lastTouchPosition, clampedDeltaMagnitude);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startTouchPosition = endTouchPosition;
                lastTouchPosition = endTouchPosition;
                isSwiping = false;
                break;

            case TouchPhase.Moved:
                onSwipeUpdated?.Invoke(swipeDistance);
                onSwipeDeltaChanged?.Invoke(swipeDelta);
                lastTouchPosition = endTouchPosition;
                isSwiping = true;
                break;

            case TouchPhase.Stationary:
                break;

            case TouchPhase.Ended:
                if (isSwiping && swipeDistance.magnitude >= minSwipeDistance)
                {
                    onSwipeEnded?.Invoke(swipeDistance);
                }
                isSwiping = false;
                break;

            case TouchPhase.Canceled:
                isSwiping = false;
                break;
        }
    }
}
