using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Vector2 movementDirection; // from input
    public float movementSpeed; //from input
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float ROCK_BASE_SPEED =1.0f;
    public bool useSkill_1;
    public bool useSkill_2;
    public bool baseAttack;
    public Rigidbody2D rb;
    public Animator animator;
    
    public Vector2 attackingDirection;
    
    public GameObject crosshair;

    
    private void Start() {
    crosshair.transform.localPosition = new Vector2(0,-3);
    }

    void Update()
    {
        ProcessInputs();
        Move();
        Aim();
        Attack();
    }

    void ProcessInputs(){
        movementDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        movementSpeed = Mathf.Clamp(movementDirection.magnitude,0.0f,1.0f);

        useSkill_1 = Input.GetButtonUp("Fire1");
        useSkill_2 = Input.GetButtonUp("Fire2");
        baseAttack = Input.GetButtonUp("Fire3");
    }

    void Move(){
        rb.velocity = movementDirection * movementSpeed *MOVEMENT_BASE_SPEED;
        animateMovement();
    }

    void animateMovement()
    {
        if(movementDirection != Vector2.zero){
            animator.SetFloat("moveX",movementDirection.x);
            animator.SetFloat("moveY",movementDirection.y);
        
        }
            animator.SetFloat("Speed",movementSpeed);
    }
        
    void Aim(){
        
            if (movementDirection != Vector2.zero){
            crosshair.transform.localPosition = movementDirection*3;
            }
        }

    void Attack(){
        attackingDirection = crosshair.transform.localPosition; // vector to crosshair obj
        attackingDirection.Normalize(); //ignore magnitude
        

        if(useSkill_1){ 
          this.GetComponent<Abilities>().rockAim();
        }
        
        
         if(useSkill_2){
          this.GetComponent<Abilities>().rockMouse();
        }
      }
    
}
