using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    public float acd; //ability cool down
    public GameObject chargeEffect;

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
        if (cd <= 0f)
        {

            distanceToTarget = Vector2.Distance(transform.position, target.position);
            if ( distanceToTarget <= 5.0f && distanceToTarget >= 2.0f)
            {
                chargeDirection = new Vector2(-1 * (transform.position.x - target.transform.position.x), -1 * (transform.position.y - target.transform.position.y));
                chargeDirection.Normalize();
                this.target = target;
                msi = Mathf.Clamp(chargeDirection.magnitude, 0.0f, 1.0f);
                StartCoroutine(ChargeCoroutine());
                cd = acd;
            }

        }

    }

    void Update()
    {
        if (cd >= 0)
        {
            cd -= Time.deltaTime;
        }

    }

    IEnumerator ChargeCoroutine()
    {
        float time = 0.1f;
        float count = 0.0f;
        while (count < time)
        {
            GetComponent<MovementController>().charging = true;

            rb.velocity = chargeDirection * msi * this.GetComponent<StatusController>().mvspd * 10;
            count += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<MovementController>().charging = false;
        target.GetComponent<HealthController>().TakeDamage(100);



    }

}
