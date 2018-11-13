using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentController : MonoBehaviour
{

    #region Private attributes
    private Animator animator;
    #endregion

    public void SetIsOnView(bool value) { 
		if (value) print("Hi!");
		animator.SetBool("IsOnView", value); 
	}

    void Start()
    {
        animator = GetComponent<Animator>();
	}
}
