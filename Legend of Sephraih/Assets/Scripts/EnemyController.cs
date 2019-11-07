using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Animator animator;
    public GameObject attackingDirection;
    public Camera mainCam;

    public Vector2 movementDirection;
    private float msi;


    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //GameObject.Find("Player");


        movementDirection = new Vector2(-1 * (rb.position.x - player.transform.position.x), -1 * (rb.position.y - player.transform.position.y));
        movementDirection.Normalize();

        msi = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        GetComponent<MovementController>().Move(movementDirection, msi);
        GetComponent<MovementController>().MovementAnimation();

    }

    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection;
        }
    }

    void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) < 1.0f)
        {
            this.GetComponent<BasicAttack>().Attack();
        }
    }

    private void Die()
    {
        if (this.GetComponent<HealthController>().health <= 0)
        {

            Instantiate((Resources.Load("Prefabs/Enemy") as GameObject), new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
