using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoltProjectile : MonoBehaviour
{
    public string enemy ="Enemy";
    public float speed;
    public float lifetime;
    public float distance;
    public int dmg;
    public int dotd;
    public int dott;
    public float slow;

    private List<Collider2D> damagedTargets = new List<Collider2D>();

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
            
            if (collider.CompareTag(enemy) && collider.isTrigger)
            {
                if (!damagedTargets.Contains(collider))
                {
                    Debug.Log("enemy damage taken");
                    collider.GetComponent<HealthController>().TakeDamage(dmg);
                    collider.GetComponent<StatusController>().Burn(dotd, dott, slow);
                    damagedTargets.Add(collider);

                    DestroyProjectile();
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


/*
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