using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressController : MonoBehaviour
{
    #region Public attributes
    public bool oneTimePressed; /* If the button is pressed and can never be unpressed */
    public bool isTimed; /* If the button has a timer */
    public float time; /* Time that the button will be pressed */
    public GameObject interactableObject;
    #endregion

    #region Private attributes
    private Animator an;
    private bool isPressed;
    private float timeLeft;
    private int previousSeconds;
    private ButtonInteractionController bic;
    #endregion

    void Start()
    {
        an = GetComponent<Animator>();
        bic = interactableObject.GetComponent<ButtonInteractionController>();
        if (!bic) print("The object associated with this button doesn't have a ButtonInterectionController.");
    }

    public void PressButton(bool press)
    {
        an.SetBool("isPressed", press);
        isPressed = press;
        bic.ActivateButtonInteration();
    }

    void Update()
    {
        if (!oneTimePressed && isPressed && isTimed)
        {
            timeLeft -= Time.deltaTime;

            /* Catch how many seconds are left */
            int secondsLeft = Mathf.CeilToInt(timeLeft % 60);
            if (secondsLeft != previousSeconds) {
                previousSeconds = secondsLeft;
                print(previousSeconds);
            }

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
        if (!oneTimePressed && isTimed) {
            timeLeft = time;
            previousSeconds = -1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isTimed && !oneTimePressed) PressButton(false);
    }
}
