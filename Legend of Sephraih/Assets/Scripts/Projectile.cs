using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string enemy ="Enemy";
    public float speed;
    public float lifetime;
    public float distance;
    public int dmg;
    public int dotd;
    public int dott;
    public float slow;

    public LayerMask whatIsEnemy;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }
    private void Update()
    {
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


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        GameObject a = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(a, 1);
        Destroy(gameObject);
    }

}
