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
    public Transform enemy;
    public bool allActive;
    public List<Transform> enemylist;
    public Animator ShakeAnimation;


    private void Start()
    {
       // ni.gameObject.SetActive(true);
       // san.gameObject.SetActive(true);
    }

    // follow target
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
                //target.gameObject.SetActive(false);
                target = ni;
            }
            else if (target == ni)
            {
                // target.gameObject.SetActive(false);
                target = san;
            }
            else if (target == san)
            {
                //target.gameObject.SetActive(false);
                target = ichi;
            }

            target.gameObject.SetActive(true);
        }



    }

    public void CamShake() {
        ShakeAnimation.SetTrigger("shake");
    }

    public Transform ClosestPlayer(Transform self)
    {

        if (ni.gameObject.activeSelf && san.gameObject.activeSelf) { 
        var one = Vector2.Distance(self.position, ichi.position);
        var two = Vector2.Distance(self.position, ni.position);
        var three = Vector2.Distance(self.position, san.position);
        if (one < two && one < three) return ichi; //one is the closest
        if (three < two) return san; //one is not the closest, three is closer than two
        return ni;
        }
        return ichi;
    }

    public Transform ClosestEnemy(Transform self) {
        var distance = Vector2.Distance(self.position, enemylist[0].position);
        enemy = enemylist[0];
        foreach (Transform e in enemylist) {
            if (Vector2.Distance(self.position, e.position) < distance) {
                distance = Vector2.Distance(self.position, e.position);
                enemy = e;
            }
        }

        return enemy;
    }

    public Transform LowestHealthPlayer()
    {

        if (ni.gameObject.activeSelf && san.gameObject.activeSelf)
        {
            var one = ichi.GetComponent<HealthController>().health;
            var two = ni.GetComponent<HealthController>().health;
            var three = san.GetComponent<HealthController>().health;
            if (one < two && one < three) return ichi; //one is the lowest
            if (three < two) return san; //one is not the lowest, three is lower than two
            return ni;
        }
        return ichi;
    }
    public Transform LowestHealthOtherPlayer(Transform self)
    {
        if (self == ichi) { if (ni.GetComponent<HealthController>().health <= san.GetComponent<HealthController>().health) return ni; else return san; }
        else if (self == ni) { if (ichi.GetComponent<HealthController>().health <= san.GetComponent<HealthController>().health) return ichi; else return san; }
        else { if (ichi.GetComponent<HealthController>().health <= ni.GetComponent<HealthController>().health) return ichi; else return ni; }
    }

}
