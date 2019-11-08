using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{

    public GameObject attackingDirection;
    public Camera mainCam;


    public Vector3 guardSpot = new Vector3(5.0f, 5.0f, 0f);
    public float guardMaxChaseRadius = 25.0f;
    public float guardRadius = 5.0f;

    private Vector2 movementDirection;
    private float msi;
    private float distanceToTarget;
    private float distanceToGuardSpot;



    private Transform player;

    void Start()
    {
        transform.position = guardSpot;
        player = Camera.main.GetComponent<camerafollow>().target;
        GetComponent<FireBolt>().startcd = 5.0f;
        Camera.main.GetComponent<camerafollow>().enemylist.Add(transform);

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

        player = Camera.main.GetComponent<camerafollow>().ClosestPlayer(transform);

        distanceToTarget = Vector2.Distance(transform.position, player.position);
        distanceToGuardSpot = Vector2.Distance(transform.position, guardSpot);

        if (distanceToTarget < guardRadius && distanceToGuardSpot < guardMaxChaseRadius)
        {
            movementDirection = player.transform.position - transform.position;
            if (distanceToTarget < 1.0f) { movementDirection = Vector2.zero; }
        }

        else
        {
            movementDirection = guardSpot - transform.position;
            if (Vector2.Distance(transform.position, guardSpot) < 0.1f) { movementDirection = Vector2.zero; }
        }

        movementDirection.Normalize();
        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        GetComponent<MovementController>().Move(movementDirection, msi);

    }

    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection;
        }
        else if (distanceToTarget < 20.0f)
        {
            var x = player.position - transform.position;
            x.Normalize();
            attackingDirection.transform.localPosition = x;

        }
    }


    void Attack()
    {
        if (distanceToTarget < 1.0f)
        {
            this.GetComponent<BasicAttack>().Attack();
        }
        else if (distanceToTarget <5.0f) {
            this.GetComponent<ChargeAttack>().charge(player);
        }

        if (distanceToTarget > 5.0f && distanceToTarget < 10.0f) {
            GetComponent<FireBolt>().Blast();
        }
    }

    private void Die()
    {
        if (this.GetComponent<HealthController>().health <= 0)
        {
            Instantiate((Resources.Load("Prefabs/Guard") as GameObject), new Vector3(0, 0, 0), Quaternion.identity);
            Camera.main.GetComponent<camerafollow>().enemylist.Remove(transform);
            Destroy(gameObject);
        }
    }

}
