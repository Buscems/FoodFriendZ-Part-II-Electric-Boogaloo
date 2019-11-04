using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{

    BaseEnemy baseEnemy;
    PathfindingAI path;
    [SerializeField] GameObject spike;
    [SerializeField]  GameObject frontLeft;
    [SerializeField]  GameObject midLeft;
    [SerializeField]  GameObject backLeft;
    [SerializeField]  GameObject frontRight;
    [SerializeField]  GameObject midRight;
    [SerializeField]  GameObject backRight;
    [SerializeField] float DownTime;
    [SerializeField] private bool reload;
    private float savedSpeed;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        path = GetComponent<PathfindingAI>();
        reload = false;
        savedSpeed = baseEnemy.speed;
    }

    
    void Update()
    {
        /*
        if (reload)
        {
             StartCoroutine(Reloading());
            reload = false;
        }*/

        if(baseEnemy.aggroScript.aggro && reload == false)
        {
            Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);
            Instantiate(spike, midLeft.transform.position, frontLeft.transform.rotation);
            Instantiate(spike, backLeft.transform.position, frontLeft.transform.rotation);
            Instantiate(spike, frontRight.transform.position, frontLeft.transform.rotation);
            Instantiate(spike, midRight.transform.position, frontLeft.transform.rotation);
            Instantiate(spike, backRight.transform.position, frontLeft.transform.rotation);
            StartCoroutine(Reloading());
            //reload = false;
            //path.enabled = false;
            //Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);
            //reload = true;

            Debug.Log(reload);
        }

    }

    /*void Shoot()
    {
        baseEnemy.speed = 0;
        reload = true;
        Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, midLeft.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, backLeft.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, frontRight.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, midRight.transform.position, frontLeft.transform.rotation);
        Instantiate(spike, backRight.transform.position, frontLeft.transform.rotation);
    }*/

    IEnumerator Reloading()
    {
        reload = true;
        path.enabled = true;
        yield return new WaitForSeconds(DownTime);
        reload = false;
    }
}
