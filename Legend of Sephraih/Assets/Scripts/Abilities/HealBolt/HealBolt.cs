﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//an ability launching a projectile healing characters friendly to the caster, see documentation of firebolt
public class HealBolt : MonoBehaviour
{
    private float offset = -90.0f;
    public string target = "Player";
    

    public GameObject projectile;
    public Transform shotPoint;

    private float cd;
    public float startcd;

    private void Update()
    {
        cd -= Time.deltaTime;

    }

    public void Blast()
    {
        Vector2 difference = transform.position - shotPoint.transform.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset);

        if (cd <= 0)
        {


            var bolt = Instantiate(projectile, shotPoint.position, shotPoint.transform.rotation);
            bolt.GetComponent<HealBoltProjectile>().target = target;
            bolt.GetComponent<HealBoltProjectile>().heal += this.GetComponent<StatusController>().matk;
            bolt.GetComponent<HealBoltProjectile>().user = transform;
           cd = startcd;

        }

    }



    //same use as blast but shooting towards mouse position, delay zero for testing
    public void BlastMouse()
    {


        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 180;
        shotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset);

        if (cd <= 0)
        {

            var bolt = Instantiate(projectile, transform.position, shotPoint.transform.rotation);
            bolt.GetComponent<HealBoltProjectile>().target = target;
            bolt.GetComponent<HealBoltProjectile>().heal += this.GetComponent<StatusController>().matk;
            bolt.GetComponent<HealBoltProjectile>().user = transform;

            cd = startcd;

        }

    }

}


