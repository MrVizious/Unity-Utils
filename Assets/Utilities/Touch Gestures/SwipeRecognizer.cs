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
    public bool isSwiping { get; private set; }

    private void Update()
    {
        if (Input.touchCount != 1)
        {
            isSwiping = false;
            return;
        }
        Touch touch = Input.GetTouch(0);
        Vector2 endTouchPosition = touch.position;
        Vector2 swipeDistance = endTouchPosition - startTouchPosition;
        Vector2 swipeDelta = endTouchPosition - lastTouchPosition;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startTouchPosition = endTouchPosition;
                lastTouchPosition = endTouchPosition;
                isSwiping = true;
                break;

            case TouchPhase.Moved:
                onSwipeUpdated?.Invoke(swipeDistance);
                onSwipeDeltaChanged?.Invoke(swipeDelta); // Fire delta event
                lastTouchPosition = endTouchPosition;
                break;

            case TouchPhase.Stationary:
                break;

            case TouchPhase.Ended:
                if (isSwiping)
                {
                    if (swipeDistance.magnitude >= minSwipeDistance)
                    {
                        onSwipeEnded?.Invoke(swipeDistance);
                    }
                    isSwiping = false;
                }
                break;

            case TouchPhase.Canceled:
                isSwiping = false;
                break;
        }
    }
}
