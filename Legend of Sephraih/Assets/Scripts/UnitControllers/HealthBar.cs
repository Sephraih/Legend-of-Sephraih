using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    

    private HealthController hc;
    public Image fgi;

    private void Start()
    {
        hc = GetComponentInParent<HealthController>();
    }

    private void Update()
    {
        fgi.fillAmount = 1.0f / ((float)hc.MaxHealth / (float)hc.health);
    }


}
