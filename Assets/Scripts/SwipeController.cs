using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    #region Private attributes
    private bool swipeUp, swipeDown, swipeUpLeft, swipeUpRight, swipeDownLeft, swipeDownRight;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private int deadzone;
    #endregion

    #region Public attributes
    public int desktopDeadzone;
    public int mobileDeadzone;
    #endregion

    #region Gets and Sets
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeUpRight { get { return swipeUpRight; } }
    public bool SwipeUpLeft { get { return swipeUpLeft; } }
    public bool SwipeDownRight { get { return swipeDownRight; } }
    public bool SwipeDownLeft { get { return swipeDownLeft; } }
    #endregion

    void Update()
    {
        /* Reset every bool after every frame */
        swipeUp = swipeDown = swipeUpLeft = swipeUpRight = swipeDownLeft = swipeDownRight = false;

        /* Desktop inputs */
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            startTouch = Input.mousePosition;
            deadzone = desktopDeadzone;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        /* Mobile inputs */
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouch = Input.touches[0].position;
                deadzone = mobileDeadzone;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }

        /* Calculate swipe distance */
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0) swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0)) swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        /* Check if the swipe crossed the deadzone */
        if (swipeDelta.magnitude > deadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (y > 0)
            {
                float nowTan = x / y;
                if (nowTan < Mathf.Tan(-30 * Mathf.Deg2Rad))
                {
                    swipeUpLeft = true;
                }
                else if (nowTan < Mathf.Tan(30 * Mathf.Deg2Rad))
                {
                    swipeUp = true;
                }
                else
                {
                    swipeUpRight = true;
                }
            }
            else
            {
                y = Mathf.Max(1, -y);

                float nowTan = x / y;
                if (nowTan < Mathf.Tan(-30 * Mathf.Deg2Rad))
                {
                    swipeDownLeft = true;
                }
                else if (nowTan < Mathf.Tan(30 * Mathf.Deg2Rad))
                {
                    swipeDown = true;
                }
                else
                {
                    swipeDownRight = true;
                }
            }
                Reset();
        }

    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
