using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{

    public GameObject attackingDirection;
    public Camera mainCam;

    public Vector3 guardPoint = new Vector3(5.0f, 5.0f, 0f);
    public float guardinitx;
    public float guardinity;

    public Vector2 movementDirection;
    private float msi;
    public float distanceToTarget;


    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = guardPoint;
        player = Camera.main.GetComponent<camerafollow>().target;
        GetComponent<FireBolt>().startcd = 5.0f;
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

        player = Camera.main.GetComponent<camerafollow>().target;
        distanceToTarget = Vector2.Distance(transform.position, player.position);
        if (distanceToTarget < 5.0f)
        {
            movementDirection = player.transform.position - transform.position;
            if (distanceToTarget < 1.0f) { movementDirection = Vector2.zero; }
        }

        else
        {
            movementDirection = guardPoint - transform.position;
            if (Vector2.Distance(transform.position, guardPoint) < 0.1f) { movementDirection = Vector2.zero; }
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
        else if (distanceToTarget < 10.0f)
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

        this.GetComponent<ChargeAttack>().charge(player);

        if (distanceToTarget > 5.0f && distanceToTarget < 10.0f) {
            GetComponent<FireBolt>().Blast();
        }
    }

    private void Die()
    {
        if (this.GetComponent<HealthController>().health <= 0)
        {
            Instantiate((Resources.Load("Prefabs/Guard") as GameObject), new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
