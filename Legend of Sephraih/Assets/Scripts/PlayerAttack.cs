using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float delay;
    public float startDelay;
    public int damage;


    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;

    Vector2 crossAim;

    GameObject slash;

    void Start() {
      slash = Resources.Load("Slash") as GameObject;
    }

    void Update() {
     crossAim = this.GetComponent<PlayerController>().attackingDirection;
     attackPos.position = new Vector2(0,0);
     if(delay <= 0){

         if(Input.GetKey(KeyCode.Space)){
             Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
            }
             GameObject projectile = Instantiate(slash,transform.position + new Vector3(0.5f*crossAim.x,0.5f*crossAim.y+0.4f,0), Quaternion.identity);
             
             projectile.transform.Rotate(0,0, Mathf.Atan2(crossAim.y,crossAim.x)*Mathf.Rad2Deg-90);
             Destroy(projectile, 0.5f);
         }
         delay = startDelay;
     }else {
         delay -= Time.deltaTime;
     }
    

    
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

}
