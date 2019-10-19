using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Animator animator;
    public GameObject attackingDirection;

    public Vector2 movementDirection;
    private float movementSpeedInput;

    public float movementSpeed = 1.0f;



    private Rigidbody2D rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Aim();
        Attack();
        Die();
        UpdateStatus();
    }

    void Move()
    {

        player = GameObject.Find("Player");

        movementDirection = new Vector2(-1 * (rb.position.x - player.transform.position.x), -1 * (rb.position.y - player.transform.position.y));
        movementDirection.Normalize();
        movementSpeedInput = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        rb.velocity = movementDirection * movementSpeedInput * movementSpeed;

        // movement animation
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", movementDirection.x);
            animator.SetFloat("moveY", movementDirection.y);

        }
        animator.SetFloat("Speed", movementSpeedInput);
    }

    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection;
        }
    }

    void UpdateStatus() {
        movementSpeed = this.GetComponent<StatusController>().mvspd;
        
    }


    void Attack()
    {
        this.GetComponent<BasicAttack>().Attack();
    }

    private void Die()
    {
        if (this.GetComponent<HealthController>().health <= 0)
        {

            Instantiate((Resources.Load("Enemy") as GameObject), new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
