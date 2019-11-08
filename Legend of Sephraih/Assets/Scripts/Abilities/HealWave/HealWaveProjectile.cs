using System.Collections.Generic;
using UnityEngine;

public class HealWaveProjectile : MonoBehaviour
{
    public string friendly = "Player";
    public string hostile = "Enemy";
    public Transform user;

    public GameObject destroyEffect;

    public float speed;
    public float lifetime;
    public int heal;

    public float slow;

    private List<Collider2D> healedTargets = new List<Collider2D>();
    private List<Collider2D> damagedTargets = new List<Collider2D>();


    private bool inbound = false;

    private void Start()
    {
        Invoke("Turn", lifetime * 0.5f);
    }
    private void FixedUpdate()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance);


        Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);

        foreach (Collider2D collider in overlapColliders)
        {
            if (collider.CompareTag(friendly) && collider.transform != user && collider.isTrigger)
            {
                if (!healedTargets.Contains(collider))
                {
                    healedTargets.Add(collider);
                    collider.GetComponent<HealthController>().Heal(heal);
                }
            }

            if (collider.CompareTag(hostile) && collider.isTrigger)
            {
                if (!damagedTargets.Contains(collider))
                {
                    damagedTargets.Add(collider);
                    collider.GetComponent<HealthController>().TakeDamage(heal/2);
                }
            }

        }


      
        if (inbound)
        {
            Vector2 direction = user.position - transform.position;

            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            
            foreach (Collider2D collider in overlapColliders)
            {
                if (collider.transform == user)
                {
                    DestroyProjectile();
                }

            }
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //GameObject a = Instantiate(destroyEffect, transform.position, Quaternion.Euler(0f, 0f, 0f));
        //Destroy(a, 1);
        Destroy(gameObject);
    }
    void Turn()
    {
        inbound = true;
        healedTargets.Clear();
        damagedTargets.Clear();
    }



}
