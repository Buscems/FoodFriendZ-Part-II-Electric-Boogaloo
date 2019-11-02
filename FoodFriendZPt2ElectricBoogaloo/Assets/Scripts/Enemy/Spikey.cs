using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{

    BaseEnemy baseEnemy;
    [SerializeField] GameObject spike;
    [SerializeField]  GameObject frontLeft;
    [SerializeField]  GameObject midLeft;
    [SerializeField]  GameObject backLeft;
    [SerializeField]  GameObject frontRight;
    [SerializeField]  GameObject midRight;
    [SerializeField]  GameObject backRight;
    [SerializeField] float DownTime;
    private bool reload;
    private float savedSpeed;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        reload = false;
        savedSpeed = baseEnemy.speed;
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {

            if (reload)
            {
                StartCoroutine(Reloading());
            }

            if(reload == false)
            {
                Shoot();
            }

        }

    }

    void Shoot()
    {
        baseEnemy.speed = 0;
        reload = true;
        Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, midLeft.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, backLeft.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, frontRight.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, midRight.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, backRight.transform.position, frontLeft.transform.rotation);
    }

    IEnumerator Reloading()
    {
        baseEnemy.speed = savedSpeed;
        yield return new WaitForSeconds(DownTime);
        reload = false;
    }
}
