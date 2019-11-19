using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float acd; //ability cool down
    private float cd; //cool down remaining

    public float range = 5.0f;
    public GameObject chargeEffect;
    public Transform attackPos;
    public LayerMask boundaries;

    public void Backjump()
    {
        Vector3 direction = transform.position - attackPos.position;
        direction.Normalize();

        for (float range = this.range; range > 0; range--) //shorter jump distance if jump were out of boundary
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, range, boundaries); //check for boundary colliders
            if (hitInfo.collider == null)
            {
                if (cd <= 0f) // if ability ready to use
                {
                    transform.position = transform.position + direction * range;
                    cd = acd;
                    break;
                }
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

}
