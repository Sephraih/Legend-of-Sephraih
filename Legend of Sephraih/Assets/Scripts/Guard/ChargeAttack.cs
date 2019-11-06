using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    public float acd; //ability cool down
    private float cd; //cool down remaining
    private Vector2 chargeDirection;

    public void charge(Transform target)
    {
        if ( cd <= 0f){

            if (Vector2.Distance(transform.position, target.position) < 5.0f)
            {
                chargeDirection = new Vector2(-1 * (transform.position.x - target.transform.position.x), -1 * (transform.position.y - target.transform.position.y));
                chargeDirection.Normalize();

                transform.GetComponent<StatusController>().IncreaseMovementSpeed(10.0f, 0.2f);
                target.GetComponent<HealthController>().TakeDamage(100);
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

}
