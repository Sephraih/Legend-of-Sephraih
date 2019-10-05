using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private float delay;
    public float startDelay;
    public int damage;


    public LayerMask whatIsEnemy;

    private Transform attackPos;
    public float attackRangeX=2;
    public float attackRangeY=1;

    private GameObject slashEffect;




    private void Start()
    {
      attackPos = transform.GetChild(0);
      slashEffect = Resources.Load("ParticleSlash") as GameObject;
    }
    void Update() {



        if (delay>=0){
         delay -= Time.deltaTime;
     }
    }

    public void Attack(){
        if(delay <= 0){

            //effect
            GameObject slash = Instantiate(slashEffect,transform.position + attackPos.localPosition ,Quaternion.identity);
            slash.transform.parent = transform;
            slash.transform.Rotate(Mathf.Atan2(attackPos.localPosition.x,attackPos.localPosition.y)*Mathf.Rad2Deg,+90, 0);
            Destroy(slash,0.2f);

            //determine damaged enemies, apply damage
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX,attackRangeY),attackPos.localPosition.x*90, whatIsEnemy);
            for(int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<HealthController>().TakeDamage(damage);
            }
           
         delay = startDelay;
     }
    }

}



/*void OnDrawGizmosSelected(){
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));

  }*/
