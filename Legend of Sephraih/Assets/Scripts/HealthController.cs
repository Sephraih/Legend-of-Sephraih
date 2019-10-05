using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 100;
    public int health = 100;
    private GameObject BloodEffect;


    void Start()
    {
        health = MaxHealth;

        BloodEffect = Resources.Load("BloodEffect") as GameObject;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        GameObject blood = Instantiate(BloodEffect, transform.position, Quaternion.identity);
        blood.transform.parent = transform;
        Destroy(blood, 0.7f);
        

        Debug.Log("took dmg" + damage);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal) {
        health += heal;
    }

}
