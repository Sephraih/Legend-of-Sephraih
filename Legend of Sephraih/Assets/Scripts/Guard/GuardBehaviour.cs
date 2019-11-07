using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{

    public GameObject attackingDirection;
    public Camera mainCam;

    public Vector2 guardPoint = new Vector2(5.0f, 5.0f);
    public float guardinitx;
    public float guardinity;

    public Vector2 movementDirection;
    private float movementSpeedInput;



    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = guardPoint;
        player = Camera.main.GetComponent<camerafollow>().target;
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


        if (Vector2.Distance(transform.position, player.position) < 5.0f)
        {
            movementDirection = new Vector2(-1 * (rb.position.x - player.transform.position.x), -1 * (rb.position.y - player.transform.position.y));
            movementDirection.Normalize();

        }
        else movementDirection = new Vector2(-1 * (rb.position.x - guardPoint.x), -1 * (rb.position.y - guardPoint.y));

        movementSpeedInput = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        
        GetComponent<MovementController>().Move(movementDirection,movementSpeedInput);
      
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

        this.GetComponent<ChargeAttack>().charge(player);
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
