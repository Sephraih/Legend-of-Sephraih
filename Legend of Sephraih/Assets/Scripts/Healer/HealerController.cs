using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{


    public Rigidbody2D rb;
    

    public Vector2 movementDirection; // from input
    public float msi; //from input

    public bool isBot =true;


    
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
            Aim();
            Attack();
        }
    }


    void ProcessInputs()
    {

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        GetComponent<MovementController>().Move(movementDirection, msi);
        GetComponent<MovementController>().MovementAnimation();


        baseAttack = Input.GetButtonUp("Attack");
        useSkill_1 = Input.GetButtonUp("Skill1");
        useSkill_2 = Input.GetButtonUp("Skill2");

    }

    //move player based on input and play movement animation
  

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
            this.GetComponent<HealWave>().BlastMouse();
        }

        if (useSkill_2)
        {
            this.GetComponent<HealBolt>().BlastMouse();
        }

        if (baseAttack)
        {
            this.GetComponent<SelfHeal>().Heal();
        }
    }

}
