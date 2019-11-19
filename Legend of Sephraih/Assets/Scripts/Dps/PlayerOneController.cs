using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator animator;



    public Vector2 movementDirection; // from input
    public float msi; //from input


    private bool useSkill_1;
    private bool useSkill_2;

    private bool useSkill_3;
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
            Aim();
            Attack();
        }
    }

    void ProcessInputs()
    {
        //movement
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        GetComponent<MovementController>().Move(movementDirection, msi);

        //attacks and skills
        baseAttack = Input.GetButtonUp("Attack");
        useSkill_1 = Input.GetButtonUp("Skill1");
        useSkill_2 = Input.GetButtonUp("Skill2");
        useSkill_3 = Input.GetButtonUp("w");

    }


    // set attacking direction object's position
    void Aim()
    {
        //position the attacking direction object infront of the character, keep position when stop moving
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection * 0.5f;
        }
    }



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


