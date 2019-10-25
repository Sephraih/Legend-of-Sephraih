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
    Transform enemy;

    public bool isBot = false;


    void Update()
    {
        if (transform != Camera.main.GetComponent<camerafollow>().target)
        {

            enemy = Camera.main.GetComponent<camerafollow>().enemy;
            if (enemy)
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

        movementDirection = new Vector2(-1 * (rb.position.x - enemy.transform.position.x), -1 * (rb.position.y - enemy.transform.position.y));
        movementDirection.Normalize();
        movementSpeedInput = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
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
        this.GetComponent<MultiSlash>().Attack();
    }



}
