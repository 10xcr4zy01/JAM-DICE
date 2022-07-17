using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Swipe { Tap, Up, Down, Left, Right }
public class PlayerInput : MonoBehaviour
{
    public static bool isStopInput;
    public KeyCode moveRight, moveLeft, jumpUp, slideDown, attack;


    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    private bool firstSwipe = false;
    private float SWIPE_THRESHOLD = 20f;

    private void Update()
    {
        if (isStopInput)
            return;

        #region Mobile Input
        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
                firstSwipe = false;
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                firstSwipe = false;
            }

            //Detects swipe after finger is released from screen
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPos = touch.position;
                OnTap();
                firstSwipe = false;
            }

            if (firstSwipe) return;

            //Detects Swipe while finger is still moving on screen
            if (touch.phase == TouchPhase.Moved)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }

        }
        #endregion

        #region PC Input 
        if (Input.GetKeyDown(moveRight))
        {
            PlayerManager.OnSwipe?.Invoke(Swipe.Right);
        }
        if (Input.GetKeyDown(moveLeft))
        {
            PlayerManager.OnSwipe?.Invoke(Swipe.Left);
        }
        if (Input.GetKeyDown(jumpUp))
        {
            PlayerManager.OnSwipe?.Invoke(Swipe.Up);
        }
        if (Input.GetKeyDown(slideDown))
        {
            PlayerManager.OnSwipe?.Invoke(Swipe.Down);
        }
        if (Input.GetKeyDown(attack))
        {
            IngameManager.Instance.DiceRoll();
        }
        #endregion
    }


    void DetectSwipe()
    {
        if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            if (fingerDownPos.y - fingerUpPos.y > 0)
            {
                OnSwipeUp();
            }
            else if (fingerDownPos.y - fingerUpPos.y < 0)
            {
                OnSwipeDown();
            }
            fingerUpPos = fingerDownPos;

        }
        else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            Debug.Log("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUpPos = fingerDownPos;

        }
        else
        {
            Debug.Log("No Swipe Detected!");
        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp()
    {
        PlayerManager.OnSwipe?.Invoke(Swipe.Up);
        firstSwipe = true;
    }

    void OnSwipeDown()
    {
        PlayerManager.OnSwipe?.Invoke(Swipe.Down);
        firstSwipe = true;
    }

    void OnSwipeLeft()
    {
        PlayerManager.OnSwipe?.Invoke(Swipe.Left);
        firstSwipe = true;
    }

    void OnSwipeRight()
    {
        PlayerManager.OnSwipe?.Invoke(Swipe.Right);
        firstSwipe = true;
    }

    private void OnTap()
    {
        if (VerticalMoveValue() < SWIPE_THRESHOLD && HorizontalMoveValue() < SWIPE_THRESHOLD)
            IngameManager.Instance.DiceRoll();
    }

}
