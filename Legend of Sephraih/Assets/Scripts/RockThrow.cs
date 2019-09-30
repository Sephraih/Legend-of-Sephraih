using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : Ability
{
   
   GameObject prefab;
   public float abilitySpeed = 25;

   public Vector2 attDir = new Vector2(0,1);

   public void Start() {
      prefab = Resources.Load("Throwable") as GameObject;
   }
   
   public override void trigger(){
        
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        attDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        attDir.Normalize();

        GameObject projectile = Instantiate(prefab,transform.position + new Vector3(0.5f*attDir.x,0.5f*attDir.y+0.4f,0), Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = attDir *abilitySpeed;
        projectile.transform.Rotate(0,0, Mathf.Atan2(attDir.y,attDir.x)*Mathf.Rad2Deg);
        Destroy(projectile, 5.0f);
   }

}

