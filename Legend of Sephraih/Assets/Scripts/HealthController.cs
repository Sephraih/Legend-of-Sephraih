using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 100;
    public int health = 100;
    private GameObject healthText;
    private GameObject BloodEffect;
    public Vector3 RandomizeIntensity = new Vector3(0.5f,0,0);

    public GameObject ht;
    public int lastdmg;

    void Start()
    {
        health = MaxHealth;

        BloodEffect = Resources.Load("Prefabs/BloodEffectPrefab") as GameObject;
        healthText = Resources.Load("Prefabs/HealthTextPrefab") as GameObject;
    }

    public void TakeDamage(int damage)
    {
        
        GameObject blood = Instantiate(BloodEffect, transform.position, Quaternion.identity);
        blood.transform.parent = transform;
        Destroy(blood, 0.7f);

        ShowHealthText(damage);
        lastdmg = damage;
        health -= damage;
        
        Debug.Log("took dmg" + damage);

    }

    public void Heal(int heal)
    {
        health += heal;
    }

    public void ShowHealthText(int damage)
    {

        if (healthText)
        {
            ht = Instantiate(healthText, transform.position +new Vector3(0,1,0), Quaternion.identity);
            ht.GetComponent<TextMesh>().text = damage.ToString();
            ht.transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.y,RandomizeIntensity.x),
                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
           

            Destroy(ht, 2.0f);
        }

    }

    

}

