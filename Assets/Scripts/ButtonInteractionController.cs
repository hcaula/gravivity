using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionController : MonoBehaviour
{
    public bool initialState;
    private bool isActive;
	public bool GetIsActive() { return isActive; }

    void Start()
    {
        isActive = initialState;
    }

    public void ActivateButtonInteration()
    {
        isActive = !isActive;
    }
}
