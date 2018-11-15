using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressController : MonoBehaviour
{
    #region Public attributes
    public bool oneTimePressed; /* If the button is pressed and can never be unpressed */
    public bool isTimed; /* If the button has a timer */
    public float time; /* Time that the button will be pressed */
    #endregion

    #region Private attributes
    private Animator an;
    private bool isPressed;
    private float timeLeft;
    #endregion

    void Start()
    {
        an = GetComponent<Animator>();
    }

    public void PressButton(bool press)
    {
        an.SetBool("isPressed", press);
        isPressed = press;
    }

    void Update()
    {
        if (!oneTimePressed && isPressed && isTimed)
        {
            timeLeft -= Time.deltaTime;

            /* Catch how many seconds are left */
            // int seconds = Mathf.RoundToInt(timeLeft % 60);

            if (timeLeft < 0)
            {
                PressButton(false);
                timeLeft = time;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PressButton(true);

        /* Restart the timer */
        if (!oneTimePressed && isTimed) timeLeft = time;
    }

    void OnTriggerExit(Collider other)
    {
        if (!isTimed && !oneTimePressed) PressButton(false);
    }
}
