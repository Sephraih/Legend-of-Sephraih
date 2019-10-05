using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public Animator animator;
    public Vector2 movementDirection;
    public GameObject attackingDirection;

    public float movementSpeed;
    public float MOVEMENT_BASE_SPEED = 1.0f;

    
    private Rigidbody2D rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
     Move();
     Aim();
     Attack();
    }

    void Move(){
        
        movementDirection = new Vector2(-1*(rb.position.x - player.transform.position.x),-1* (rb.position.y - player.transform.position.y));
        movementDirection.Normalize();
        movementSpeed = Mathf.Clamp(movementDirection.magnitude,0.0f,1.0f);
        rb.velocity = movementDirection * movementSpeed *MOVEMENT_BASE_SPEED;

        // movement animation
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", movementDirection.x);
            animator.SetFloat("moveY", movementDirection.y);

        }
        animator.SetFloat("Speed", movementSpeed);
    }

    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            attackingDirection.transform.localPosition = movementDirection;
        }
    }

    
    void Attack(){
        this.GetComponent<BasicAttack>().Attack();
    }
        
}
