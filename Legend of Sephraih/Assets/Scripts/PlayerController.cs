using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Vector2 movement;
    public float movementSpeed;
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float ROCK_BASE_SPEED =1.0f;
    public bool useSkill_1;
    public bool useSkill_2;
    public bool baseAttack;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject rockPrefab;
    public GameObject treePrefab;

    public GameObject crosshair;


     void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        Aim();
        Attack();
    }

    void ProcessInputs(){
        movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        movement.Normalize();
        movementSpeed = Mathf.Clamp(movement.magnitude,0.0f,1.0f);

        useSkill_1 = Input.GetButtonUp("Fire1");
        useSkill_2 = Input.GetButtonUp("Fire2");
        baseAttack = Input.GetButtonUp("Fire3");
    }


    void Animate()
    {
        if(movement != Vector2.zero){
            animator.SetFloat("moveX",movement.x);
            animator.SetFloat("moveY",movement.y);
        
        }
            animator.SetFloat("Speed",movementSpeed);
    }
        

    void Move(){
        rb.velocity = movement * movementSpeed *MOVEMENT_BASE_SPEED;
    }
    void Aim(){
            if (movement != Vector2.zero){
            crosshair.transform.localPosition = movement*3;
            }
        }

    void Attack(){
        Vector2 attackingDirection = crosshair.transform.localPosition;
        attackingDirection.Normalize();

        if(useSkill_1){
            //instantiate arrow / throwable on player position + offset to shooting direction + offset to center according to playersprite
            GameObject arrow = Instantiate(rockPrefab,transform.position + new Vector3(0.5f*attackingDirection.x,0.5f*attackingDirection.y+0.4f,0), Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = attackingDirection *ROCK_BASE_SPEED;
            arrow.transform.Rotate(0,0, Mathf.Atan2(attackingDirection.y,attackingDirection.x)*Mathf.Rad2Deg);
            Destroy(arrow, 5.0f);
        }
         if(useSkill_2){
            //instantiate arrow / throwable on player position + offset to shooting direction + offset to center according to playersprite
            GameObject arrow = Instantiate(treePrefab,transform.position + new Vector3(0.5f*attackingDirection.x,0.5f*attackingDirection.y+0.4f,0), Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = attackingDirection *ROCK_BASE_SPEED;
            arrow.transform.Rotate(0,0, Mathf.Atan2(attackingDirection.y,attackingDirection.x)*Mathf.Rad2Deg);
            Destroy(arrow, 5.0f);
        }
      }
    
}
