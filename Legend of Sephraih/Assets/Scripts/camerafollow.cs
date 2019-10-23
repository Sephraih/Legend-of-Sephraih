using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{

    public Transform target;
    public Transform ichi;
    public Transform ni;
    public Transform san;
    public Vector3 offset;



    private void Start()
    {
        ni.gameObject.SetActive(true);
        san.gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;

    }

    private void Update()
    {



        if (Input.GetButtonDown("Swap"))
        {
            if (target == ichi)
            {
                target.gameObject.SetActive(false);
                target = ni;
            }
            else if (target == ni)
            {
                target.gameObject.SetActive(false);
                target = san;
            }
             else if (target == san)
             {
                 target.gameObject.SetActive(false); 
                 target = ichi;
             }
             
            target.gameObject.SetActive(true);
        }



    }
}
