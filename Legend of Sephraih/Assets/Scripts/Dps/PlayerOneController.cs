using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the character controller of character one (and three, which is an identical copy)
public class PlayerOneController : MonoBehaviour
{


    public Rigidbody2D rb; //physic entity
    public Animator animator; // to display movement animation



    public Vector2 movementDirection; // from input based on x and y axis
    public float msi; // strength of input between zero and one

    // used in the process of determining whether the player wants to use a skill
    private bool useSkill_1;
    private bool useSkill_2;

    private bool useSkill_3;
    private bool baseAttack;



    public GameObject attackingDirection; // object used to calculate a vector of attack

    // called once
    private void Start()
    {
        attackingDirection.transform.localPosition = new Vector2(0, -0.5f); // set an attacking direction before the player moves for the first time

    }

    // called each frame
    void Update()
    {
        if (transform == Camera.main.GetComponent<camerafollow>().target)
        {
            ProcessInputs();
            Aim();
            Attack();
        }
    }

    void ProcessInputs()
    {
        // movement based on input
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        GetComponent<MovementController>().Move(movementDirection, msi);

        // attacks and skills
        baseAttack = Input.GetButtonUp("Attack");
        useSkill_1 = Input.GetButtonUp("Skill1");
        useSkill_2 = Input.GetButtonUp("Skill2");
        useSkill_3 = Input.GetButtonUp("w");

    }


    // set attacking direction object's position
    void Aim()
    {
        //position the attacking direction object infront of the character, keep position when it stops moving
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection * 0.5f;
        }
    }


    // using the skills assigned to the keys depending on input
    void Attack()
    {

        if (useSkill_1)
        {
            this.GetComponent<FireBolt>().Blast();
        }

        if (useSkill_2)
        {
            this.GetComponent<ChargeAttack>().charge(Camera.main.GetComponent<camerafollow>().ClosestEnemy(transform));
        }

        if (baseAttack)
        {
            this.GetComponent<MultiSlash>().Attack();
        }

        if (useSkill_3)
        {
            this.GetComponent<Teleport>().Backjump();
        }
    }
}


