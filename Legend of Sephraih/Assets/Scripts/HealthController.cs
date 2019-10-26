using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int MaxHealth = 100;
    public int health = 100;
    private GameObject dmgText;
    private GameObject healText;
    private GameObject BloodEffect;
    private Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    public GameObject ht;

    void Start()
    {
        health = MaxHealth;

        BloodEffect = Resources.Load("Prefabs/BloodEffectPrefab") as GameObject;
        dmgText = Resources.Load("Prefabs/DmgTextPrefab") as GameObject;
        healText = Resources.Load("Prefabs/HealTextPrefab") as GameObject;
    }

    public void TakeDamage(int damage)
    {

        GameObject blood = Instantiate(BloodEffect, transform.position, Quaternion.identity);
        blood.transform.parent = transform;
        Destroy(blood, 0.7f);

        ShowDamageText(damage);
        health -= damage;

        Debug.Log("took dmg" + damage);

    }

    public void Heal(int heal)
    {
        if (health < MaxHealth) { health += heal; }
        if (health > MaxHealth) { health = MaxHealth; }
        ShowHealText(heal);
    }

    public void ShowDamageText(int damage)
    {

        if (dmgText)
        {
            ht = Instantiate(dmgText, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            ht.GetComponent<TextMesh>().text = damage.ToString();
            ht.transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.y, RandomizeIntensity.x),
                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));


            Destroy(ht, 2.0f);
        }

    }

    public void ShowHealText(int heal)
    {

        if (healText)
        {
            ht = Instantiate(healText, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            ht.GetComponent<TextMesh>().text = heal.ToString();
            ht.transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.y, RandomizeIntensity.x),
                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));


            Destroy(ht, 2.0f);
        }

    }



}

