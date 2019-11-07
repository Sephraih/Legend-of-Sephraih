using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DpsBotBehaviour : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movementDirection; // from input
    public float movementSpeedInput; //from input
    public GameObject attackingDirection;
    Transform target;

    public bool isBot = false;


    void Update()
    {
        if (transform != Camera.main.GetComponent<camerafollow>().target)
        {

            target = Camera.main.GetComponent<camerafollow>().enemy;
            if (target)
            {
                Move();
                Aim();
                UseSkills();
            }
        }

    }



    //move player based on input and play movement animation
    void Move()
    {

        movementDirection = new Vector2(-1 * (rb.position.x - target.transform.position.x), -1 * (rb.position.y - target.transform.position.y));
        movementDirection.Normalize();
        movementSpeedInput = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        if (Vector2.Distance(transform.position, target.position) < 1.0f) { movementSpeedInput = 0.5f; }
            rb.velocity = movementDirection * movementSpeedInput * this.GetComponent<StatusController>().mvspd;

        //mv anim
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", movementDirection.x);
            animator.SetFloat("moveY", movementDirection.y);

        }
        animator.SetFloat("Speed", movementSpeedInput);
    }


    // set attacking direction object's position
    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection * 0.5f;
        }
    }

    void UseSkills()
    {
        if (Vector2.Distance(transform.position, target.position) < 1.0f)
        {
            this.GetComponent<MultiSlash>().Attack();
        }
    }



}
