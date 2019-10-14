using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{

   
    public Vector2 attDir = new Vector2(0,1);
    public float abilitySpeed = 25;
    
    Vector2 attackingDirection;

    GameObject rock;
    GameObject slash;

    public LayerMask whatIsEnemy;


   

    public void Start(){
      rock = Resources.Load("Rock") as GameObject;
      slash = Resources.Load("SlashProjectile") as GameObject;
    }
    


        
    public void RockMouse(){
            
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        attDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        attDir.Normalize();

        GameObject projectile = Instantiate(slash,transform.position + new Vector3(0.5f*attDir.x,0.5f*attDir.y,0), Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = attDir *abilitySpeed;
        projectile.transform.Rotate(0,0, Mathf.Atan2(attDir.y,attDir.x)*Mathf.Rad2Deg-90);
        Destroy(projectile, 2.0f);
    }


    public void RockAim(){
        //instantiate arrow / throwable on player position + offset to shooting direction + offset to center according to playersprite
        attackingDirection = this.GetComponent<PlayerController>().attackingDirection.transform.localPosition;

            GameObject projectile = Instantiate(slash,transform.position + new Vector3(0.5f*attackingDirection.x,0.5f*attackingDirection.y,0), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = attackingDirection *abilitySpeed;
            projectile.transform.Rotate(0,0, Mathf.Atan2(attackingDirection.y,attackingDirection.x)*Mathf.Rad2Deg-90);
            Destroy(projectile, 0.2f);
    }
}


