using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;

    public Animator animator;
    public Vector2 movement;
    public float movementSpeed;
    public float MOVEMENT_BASE_SPEED = 1.0f;

    public ParticleSystem BloodEffect;

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
      Animate();
    }

    void Move(){
        
        movement = new Vector2(-1*(rb.position.x - player.transform.position.x),-1* (rb.position.y - player.transform.position.y));
        movement.Normalize();
        movementSpeed = Mathf.Clamp(movement.magnitude,0.0f,1.0f);
        rb.velocity = movement * movementSpeed *MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        if(movement != Vector2.zero){
            animator.SetFloat("moveX",movement.x);
            animator.SetFloat("moveY",movement.y);
        
        }
            animator.SetFloat("Speed",movementSpeed);
    }

    public void TakeDamage(int damage){
        health -= damage;
        BloodEffect.GetComponent<ParticleSystem>().Play();
        //ParticleSystem dmgEffect =Instantiate(BloodEffect, transform.position, Quaternion.identity);
        //Destroy(dmgEffect, 3);
        
        Debug.Log("took dmg" + damage);
        if(health <= 0){
            Destroy(gameObject);
        }
    }
        
}
