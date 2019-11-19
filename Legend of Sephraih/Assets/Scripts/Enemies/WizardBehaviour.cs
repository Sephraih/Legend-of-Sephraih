using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehaviour : MonoBehaviour
{

    public GameObject attackingDirection;
    public Camera mainCam;



    private Vector2 movementDirection;
    private float msi;
    public float distanceToTarget;



    private Transform target;

    void Start()
    {
        target = Camera.main.GetComponent<camerafollow>().target;
        GetComponent<FireBolt>().startcd = 1.0f;
        Camera.main.GetComponent<camerafollow>().enemylist.Add(transform);
        GetComponent<StatusController>().matk = 100;


    }

    void Update()
    {
        Camera.main.GetComponent<camerafollow>().enemy = transform;
        Move();
        Aim();
        Attack();
        Die();
    }

    void Move()
    {

        target = Camera.main.GetComponent<camerafollow>().ClosestPlayer(transform);
        distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget >= 15.0f && distanceToTarget <= 20.0f) // walk to target within targeting range, out of cast range
        {
            movementDirection = target.transform.position - transform.position;
        }
        if (distanceToTarget <= 10.0f && distanceToTarget > 5.0f || distanceToTarget > 20.0f) movementDirection = Vector2.zero; // don't move
        if (distanceToTarget <= 5.0f)
        {
            GetComponent<Teleport>().Backjump();
            movementDirection = transform.position - target.transform.position; //walk away from target
        } 

        movementDirection.Normalize();
        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        GetComponent<MovementController>().Move(movementDirection, msi);

    }

    void Aim()
    {
            var x = target.position - transform.position;
            x.Normalize();
            attackingDirection.transform.localPosition = x;
        if (movementDirection == Vector2.zero) { 
        GetComponent<MovementController>().LookAt(target.position);
        }
    }


    void Attack()
    {
        if (distanceToTarget >= 3.0f && distanceToTarget <= 15.0f)
        {
            GetComponent<FireBolt>().Blast();
        }
    }

    private void Die()
    {
        if (this.GetComponent<HealthController>().health <= 0)
        {
            Instantiate((Resources.Load("Prefabs/Wizard") as GameObject), new Vector3(0, 0, 0), Quaternion.identity);
            Camera.main.GetComponent<camerafollow>().enemylist.Remove(transform);
            Destroy(gameObject);
        }
    }

}
