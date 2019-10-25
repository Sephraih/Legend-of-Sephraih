using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHeal : MonoBehaviour
{

    private float cd = 2.0f; //balancing right here.
    private float cdtimer = 0;
    public GameObject effect;


    void Update()
    {
        if (cdtimer >= 0)
        {
            cdtimer -= Time.deltaTime;
        }
    }

    //heal yourself based on your matk.
    public void Heal()
    {
        if (cdtimer <= 0)
        {
            GetComponent<HealthController>().Heal(GetComponent<StatusController>().matk);
            cdtimer = cd;
            GameObject a = Instantiate(effect, transform.position, Quaternion.identity);
            a.transform.parent = transform;
            Destroy(a, 0.5f);
        }

    }

}
