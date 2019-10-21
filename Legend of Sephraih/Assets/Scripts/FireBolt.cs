using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : MonoBehaviour
{
    private float offset =-90.0f;
    public string enemy="Enemy";

    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        timeBtwShots -= Time.deltaTime;

    }

    public void Blast()
    {
        Vector2 difference = transform.position - shotPoint.transform.position;
        
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset);

        if (timeBtwShots <= 0)
        {
            

            var bolt = Instantiate(projectile, shotPoint.position, shotPoint.transform.rotation);
            bolt.GetComponent<Projectile>().enemy = enemy;
            bolt.GetComponent<Projectile>().dmg += this.GetComponent<StatusController>().matk;
            bolt.GetComponent<Projectile>().dotd += this.GetComponent<StatusController>().matk;

            timeBtwShots = startTimeBtwShots;

        }

    }



    //same use as blast but shooting towards mouse position, delay zero for testing
    public void BlastMouse()
    {


        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.transform.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg -180;
        shotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset);

        if (timeBtwShots <= 0)
        {
            
            var bolt = Instantiate(projectile, shotPoint.position, shotPoint.transform.rotation);
            bolt.GetComponent<Projectile>().enemy = enemy;
            bolt.GetComponent<Projectile>().dmg += this.GetComponent<StatusController>().matk;
            bolt.GetComponent<Projectile>().dotd += this.GetComponent<StatusController>().matk;
            

            timeBtwShots = 0f;

        }

    }

}


