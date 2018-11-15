using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressController : MonoBehaviour
{
    #region Public attributes
    public bool isTimed;
    public float time;
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

    void Update()
    {
        if (isPressed && isTimed)
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
        timeLeft = time;
    }

    void OnTriggerExit(Collider other)
    {
        if (!isTimed) PressButton(false);
    }

    void PressButton(bool press)
    {
        an.SetBool("isPressed", press);
        isPressed = press;
    }
}
