using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Animator animator;
    public GameObject attackPos; //the unit's attackPos transform

    private Vector2 md;
    private float msi;

    private Rigidbody2D rb;
    public bool stuck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //md is the movement direction, msi is a value between zero and one to determine movement speed from input
    public void Move(Vector2 md, float msi)
    {
        if (!stuck)
        {
            this.md = md;
            this.msi = msi;
            rb.velocity = md * msi * this.GetComponent<StatusController>().mvspd;
            MovementAnimation();
        }
    }

    public void MovementAnimation()
    {
        //movement animation
        if (md != Vector2.zero && !stuck)
        {
            animator.SetFloat("moveX", md.x);
            animator.SetFloat("moveY", md.y);

        }
        animator.SetFloat("Speed", msi);
    }

    public void ChargeLookat(Vector2 chargeDir) {
        if (animator.isInitialized)
        {
            animator.SetFloat("moveX", chargeDir.x);
            animator.SetFloat("moveY", chargeDir.y);
            attackPos.transform.localPosition = chargeDir.normalized;
            animator.SetFloat("Speed", 1.0f);
        }
    }


}
