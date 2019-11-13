using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBotBehaviour : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movementDirection; // from input
    public float msi; //from input

    public GameObject attackingDirection;

    public Transform dps;
    public Transform tank;

    public bool isBot = false;


    void Update()
    {
        if (transform != Camera.main.GetComponent<camerafollow>().target) //if not followed by camera it is not the active char
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

        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);

        GetComponent<MovementController>().Move(movementDirection, msi);

    }


    // set attacking direction object's position
    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection * 0.5f;
        }
        else
        {
            var dir = Camera.main.GetComponent<camerafollow>().LowestHealthOtherPlayer(transform).transform.position - transform.position;
            attackingDirection.transform.localPosition = dir / dir.magnitude;

        }
    }

    void UseSkills()
    {
        if (Camera.main.GetComponent<camerafollow>().LowestHealthOtherPlayer(transform).GetComponent<HealthController>().health 
            != Camera.main.GetComponent<camerafollow>().LowestHealthOtherPlayer(transform).GetComponent<HealthController>().MaxHealth) //if the lowest health target that isnt the healer itself is not at max health.
            this.GetComponent<HealBolt>().Blast();
    }



}
