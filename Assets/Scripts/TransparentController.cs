using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentController : MonoBehaviour
{

    #region Private attributes
    private Animator animator;
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
		animator.SetBool("IsOnView", false);
	}

    public void SetIsOnView(bool value) { animator.SetBool("IsOnView", value); }
	public bool GetIsOnView() { return animator.GetBool("IsOnView"); }
}
