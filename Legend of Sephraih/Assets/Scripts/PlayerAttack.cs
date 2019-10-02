using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float delay;
    public float startDelay;
    public int damage;

    public Vector2 movementDirection;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRangeX;
    public float attackRangeY;

    public GameObject slashEffect;

    Vector2 crossAim;

    GameObject slash;

    void Start() {
      crossAim=new Vector2(0,-2);
    }

    void Update() {

     crossAim = this.GetComponent<PlayerController>().attackingDirection;
     attackPos.localPosition = new Vector2(crossAim.x,crossAim.y);
     Attack();
     if(delay>=0){
         delay -= Time.deltaTime;
     }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
    }

    void Attack(){
        if(delay <= 0){

         if(Input.GetKey(KeyCode.A)){

             slash = Instantiate(slashEffect,transform.position + new Vector3(0.5f*crossAim.x,0.5f*crossAim.y,0),Quaternion.identity);
             slash.transform.Rotate(Mathf.Atan2(crossAim.y,crossAim.x)*Mathf.Rad2Deg-90,-90, 0);
             Destroy(slash,0.2f);

            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY),0, whatIsEnemy);
            for(int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
            }
           
         }
         delay = startDelay;
     }
    }

}
