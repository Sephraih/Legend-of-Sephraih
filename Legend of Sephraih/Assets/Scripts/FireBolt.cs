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
    public float starTimeBtwShots;

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
            //instantiates a projectile prefab which contains an attached projectile script handling the movement and destruction of itself
            var bolt = Instantiate(projectile, shotPoint.position, shotPoint.transform.rotation);
            bolt.GetComponent<Projectile>().enemy = enemy;
            bolt.GetComponent<Projectile>().dmg += this.GetComponent<StatusController>().matk;
            bolt.GetComponent<Projectile>().dotd += this.GetComponent<StatusController>().matk;

            timeBtwShots = starTimeBtwShots;

        }

    }
}



//mouse position
//  Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.transform.position;
