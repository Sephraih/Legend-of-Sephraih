using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoltProjectile : MonoBehaviour
{
    public string enemy ="Enemy"; // caster's enemy, from fire bolt script, default set to enemy units
    public float speed;
    public float lifetime;
    public int dmg; // damage of direct hit
    public int dotd; // damage over time damage
    public int dott; // damage over time duration
    public float slow; // slow amount

    private List<Collider2D> damagedTargets = new List<Collider2D>(); // list to save targets that have been damaged,

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }
    private void FixedUpdate()
    {
      

        Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);

        foreach (Collider2D collider in overlapColliders)
        {
            
            if (collider.CompareTag(enemy) && collider.isTrigger) // all enemy colliders, each character has 2 colliders, only the trigger
            {
                if (!damagedTargets.Contains(collider)) //before the bolt is destroyed, it checks for colliders on every frame, which might result in duplicate application
                {
                    collider.GetComponent<HealthController>().TakeDamage(dmg);
                    collider.GetComponent<StatusController>().Burn(dotd, dott, slow);
                    damagedTargets.Add(collider);

                    DestroyProjectile(); // destroy on first hit, optional
                }
            }

        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        GameObject a = Instantiate(destroyEffect, transform.position, Quaternion.Euler(0, 0f, 0f));
        Destroy(a, 1);
        Destroy(gameObject);
    }

}

// checking for enemy using raycast
/*
 * 
 * public float distance; // distance of collision detection relative to projectile
    
    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance);
     
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag(enemy))
            {
                Debug.Log("enemy damage taken");
                hitInfo.collider.GetComponent<HealthController>().TakeDamage(dmg);
                hitInfo.collider.GetComponent<StatusController>().Burn(dotd, dott,slow);

                DestroyProjectile();
            }

        }
*/
