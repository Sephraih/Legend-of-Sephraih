using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public Rigidbody2D rb;

    public Vector2 movementDirection; // from input
    public float movementSpeed; //from input
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public Animator animator;

    private bool useSkill_1;
    private bool useSkill_2;
    private bool baseAttack;



    public GameObject attackingDirection;


    private void Start()
    {
        attackingDirection.transform.localPosition = new Vector2(0, -0.5f);
    }

    void Update()
    {
        ProcessInputs();
        Move();
        Aim();
        Attack();
    }

    void ProcessInputs()
    {

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);

        useSkill_1 = Input.GetButtonUp("Fire1");
        useSkill_2 = Input.GetButtonUp("Fire2");
        baseAttack = Input.GetButtonUp("Fire3");
    }

    //move player based on input and play movement animation
    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;

        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", movementDirection.x);
            animator.SetFloat("moveY", movementDirection.y);

        }
        animator.SetFloat("Speed", movementSpeed);
    }


    // set attacking direction object's position
    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection * 0.5f;
        }
    }

    void Attack()
    {

        if (useSkill_1)
        {
            this.GetComponent<BasicAttack>().Attack();

        }


        if (useSkill_2)
        {
            this.GetComponent<FireBolt>().Blast();
        }

        if (baseAttack)
        {
            this.GetComponent<TrippleAttack>().Attack();
        }
    }

}
