using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator animator;


    public Vector2 movementDirection; // from input
    public float movementSpeedInput; //from input

    public bool isBot =true;


    public float movementSpeed = 1.0f;

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
       
        if (transform == Camera.main.GetComponent<camerafollow>().target)
        {
            ProcessInputs();
            Move();
            Aim();
            Attack();
        }
        StatusUpdate();
    }


    void ProcessInputs()
    {

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        movementSpeedInput = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);

        baseAttack = Input.GetButtonUp("Attack");
        useSkill_1 = Input.GetButtonUp("Skill1");
        useSkill_2 = Input.GetButtonUp("Skill2");

    }

    //move player based on input and play movement animation
    void Move()
    {
        rb.velocity = movementDirection * movementSpeedInput * movementSpeed;

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


    void StatusUpdate()
    {
        movementSpeed = this.GetComponent<StatusController>().mvspd;
    }

    void Attack()
    {

        if (useSkill_1)
        {
            this.GetComponent<HealBolt>().Blast();
        }

        if (useSkill_2)
        {
            this.GetComponent<HealBolt>().BlastMouse();
        }

        if (baseAttack)
        {
            this.GetComponent<HealBolt>().Blast();
        }
    }

}
