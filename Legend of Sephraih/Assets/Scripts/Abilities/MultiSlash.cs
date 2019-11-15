using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSlash : MonoBehaviour
{
    public int damage;
    public float startDelay;
    private float delay;
    private int maxCombo = 4;
    private int comboCount = 1;
    private float comboDelay = 0.1f;


    public LayerMask whatIsEnemy;

    private Transform attackPos;
    private float attackRangeX = 2.5f;
    private float attackRangeY = 1.5f;

    private GameObject slashEffect;


    private void Start()
    {
        attackPos = transform.GetChild(0);
        slashEffect = Resources.Load("Prefabs/ParticleSlashPrefab") as GameObject;
    }

    void Update()
    {
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (delay <= 0)
        {

            if (comboCount > 2)DoubleSlash();
            else if (comboCount == 1)RightSlash();
            else LeftSlash();

            var atk = transform.GetComponent<StatusController>().atk;
            //determine damaged enemies, apply damage
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), attackPos.localPosition.x * 90, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].isTrigger)
                    enemiesToDamage[i].GetComponent<HealthController>().TakeDamage((damage + atk) + comboCount*atk);
            }
            comboCount++;
            delay = comboDelay;

            if (comboCount > maxCombo)
            {
                delay = startDelay;
                comboCount = 1;
            }

        }
    }

    private void LeftSlash()
    {
        Slash(-30, Color.cyan);
    }

    private void RightSlash()
    {
        Slash(30, Color.cyan);
    }

    private void DoubleSlash()
    {
        Color sc = new Color(0.2f, 0, 0.7f,1);
        Slash(30, sc);
        Slash(-30, sc);
        Camera.main.GetComponent<camerafollow>().CamShake();
    }

    private void Slash(float angle, Color color)
    {

        // instantiate slash prefab
        GameObject slash = Instantiate(slashEffect, transform.position + attackPos.localPosition, Quaternion.identity);


        //get particle system to set it's color
        ParticleSystem.MainModule slashParticleMain = slash.GetComponent<ParticleSystem>().main;
        slashParticleMain.startColor = color;

        //effect
        slash.transform.parent = transform; // to set the simulation space
        slash.transform.Rotate(Mathf.Atan2(attackPos.localPosition.x, attackPos.localPosition.y) * Mathf.Rad2Deg, +90, 0); // direction user is facing
        slash.transform.Rotate(angle, 0, 0); // turn the slash

        Destroy(slash, 0.2f); //free memory


    }

}