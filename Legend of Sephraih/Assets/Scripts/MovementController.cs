using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Animator animator;

    private Vector2 md;
    private float msi;

    private Rigidbody2D rb;
    public bool charging;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //md is the movement direction, msi is a value between zero and one to determine movement speed from input
    public void Move(Vector2 md, float msi)
    {
        if (!charging)
        {
            this.md = md;
            this.msi = msi;
            rb.velocity = md * msi * this.GetComponent<StatusController>().mvspd;
        }
    }

    public void MovementAnimation()
    {
        //movement animation
        if (md != Vector2.zero)
        {
            animator.SetFloat("moveX", md.x);
            animator.SetFloat("moveY", md.y);

        }
        animator.SetFloat("Speed", msi);
    }


}
