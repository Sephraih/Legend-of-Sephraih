﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    //the script is attached to all character objects.

    public int MaxHealth = 100; // maximal health the number here is default, overwritten in inspector
    public int health = 100; // current health - default set to avoid errors
    private GameObject dmgText; // a damage text prefab to be displayed when the character takes damage
    private GameObject healText; // a text prefab to display the amount of health recovered as a number
    private GameObject bloodEffect; //a blood effect spawned by the character when damage is taken
    private GameObject healedEffect; // a recovery effect
    private float htSpawnCount =0; // a counter to influence the spawn position relative to the character of any health change numbers

    public GameObject ht; // health text variable, this can be a heal or dmg text

    void Start()
    {
        health = MaxHealth; // initialize character to start at max health

        //loading prefabs to be instantiated later
        bloodEffect = Resources.Load("Prefabs/BloodEffectPrefab") as GameObject;
        healedEffect = Resources.Load("Prefabs/HealedEffectPrefab") as GameObject;

        dmgText = Resources.Load("Prefabs/DmgTextPrefab") as GameObject;
        healText = Resources.Load("Prefabs/HealTextPrefab") as GameObject;
    }

    // take damage, display number and blood effect
    public void TakeDamage(int damage)
    {

        GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity); // at character's position without any rotation
        blood.transform.parent = transform; // make the effect child of the character to let the effect follow it
        Destroy(blood, 0.7f);

        ShowDamageText(damage);
        health -= damage;
        if (health < 0) { health = 0; }

        Debug.Log("took dmg" + damage);

    }

    // recover damage, display number and recovery effect
    public void Heal(int heal)
    {
        GameObject hef = Instantiate(healedEffect, transform.position, Quaternion.identity); 
        hef.transform.parent = transform;
        Destroy(hef, 1.0f);

        if (health < MaxHealth) { health += heal; }
        if (health > MaxHealth) { health = MaxHealth; }
        ShowHealText(heal);
    }

    public void ShowDamageText(int damage)
    {
        if (htSpawnCount > 0.6) htSpawnCount = 0;
        if (dmgText)
        {
            ht = Instantiate(dmgText, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            ht.GetComponent<TextMesh>().text = damage.ToString();
            ht.transform.localPosition += new Vector3(htSpawnCount, htSpawnCount, 0);
            htSpawnCount+=0.3f;
            Destroy(ht, 2.0f);
        }

    }

    public void ShowHealText(int heal)
    {

        if (htSpawnCount > 0.6) htSpawnCount = 0;
        if (healText)
        {
            ht = Instantiate(healText, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            ht.GetComponent<TextMesh>().text = heal.ToString();
            ht.transform.localPosition += new Vector3(-htSpawnCount, htSpawnCount, 0);
            htSpawnCount+=0.3f;
            Destroy(ht, 2.0f);
        }

    }



}

