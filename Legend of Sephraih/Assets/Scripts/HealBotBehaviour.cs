using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBotBehaviour : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movementDirection; // from input
    public float movementSpeedInput; //from input

    public GameObject attackingDirection;

    public Transform dps;
    public Transform tank;

    public bool isBot = false;


    void Update()
    {
        if (transform != Camera.main.GetComponent<camerafollow>().target)
        {
            Move();
            Aim();
            UseSkills();
        }
    }



    //move player based on input and play movement animation
    void Move()
    {

        if (Vector2.Distance(transform.position, dps.position) > 5.0f)
        {
            movementDirection = new Vector2(-1 * (rb.position.x - dps.transform.position.x), -1 * (rb.position.y - dps.transform.position.y));
            movementDirection.Normalize();

        }
        else movementDirection = new Vector2(0, 0);

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

    void UseSkills() {
        if(Camera.main.GetComponent<camerafollow>().target.GetComponent<HealthController>().health != Camera.main.GetComponent<camerafollow>().target.GetComponent<HealthController>().MaxHealth)
        this.GetComponent<HealBolt>().Blast();
    }
  


}
