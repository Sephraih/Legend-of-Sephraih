using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    public float acd; //ability cool down
    public float range =5.0f;
    public GameObject chargeEffect;
    public float stunTime = 0.4f;


    private float cd; //cool down remaining
    private Vector2 chargeDirection;

    private float distanceToTarget;
    private float msi;
    private Transform target;




    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void charge(Transform target)
    {
        if (cd <= 0f) // if ability ready to use
        {

            distanceToTarget = Vector2.Distance(transform.position, target.position);
            if ( distanceToTarget <= range && distanceToTarget >= 2.0f)
            {
                chargeDirection = target.position - transform.position;
                chargeDirection.Normalize();
                this.target = target;
                StartCoroutine(ChargeCoroutine());
                cd = acd; //reset cooldown
            }

        }

    }

    void Update()
    {
        if (cd >= 0)
        {
            cd -= Time.deltaTime; //decrease cooldown
        }

    }

    IEnumerator ChargeCoroutine()
    {
        float time = 0.1f;
        float count = 0.0f;
        while (count < time)
        {
            float rotZ = Mathf.Atan2(chargeDirection.y, chargeDirection.x) * Mathf.Rad2Deg;
            
            GameObject cef = Instantiate(chargeEffect, transform.position, Quaternion.Euler(0f, 0f, rotZ - 90));
            cef.transform.parent = transform;
            Destroy(cef, 0.5f);


            GetComponent<MovementController>().stuck = true; //disalow other movement while charging
            GetComponent<MovementController>().WalkTowards(chargeDirection);
            rb.velocity = chargeDirection * 50;
            count += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        target.GetComponent<MovementController>().Stun(stunTime);
        Camera.main.GetComponent<camerafollow>().CamShake();
        GetComponent<MovementController>().stuck = false;
        target.GetComponent<HealthController>().TakeDamage(100);



    }

}
