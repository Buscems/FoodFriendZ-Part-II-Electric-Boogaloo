﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{

    BaseEnemy baseEnemy;
    PathfindingAI path;
    [SerializeField] GameObject spike;
    [SerializeField] GameObject spikeRight;
    [SerializeField]  GameObject frontLeft;
    [SerializeField]  GameObject midLeft;
    [SerializeField]  GameObject backLeft;
    [SerializeField]  GameObject frontRight;
    [SerializeField]  GameObject midRight;
    [SerializeField]  GameObject backRight;
    [SerializeField] float DownTime;
    [SerializeField] private bool reload;
    private float savedSpeed;

    public AudioSource attackSound;

    public Animator anim;

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
            attackSound.Play();
            Instantiate(spike, frontLeft.transform.position, Quaternion.Euler(0, 0, 120));
            Instantiate(spike, midLeft.transform.position, Quaternion.Euler(0, 0, 180));
            Instantiate(spike, backLeft.transform.position, Quaternion.Euler(0, 0, 225));
            Instantiate(spikeRight, frontRight.transform.position, Quaternion.Euler(0, 0, -120));
            Instantiate(spikeRight, midRight.transform.position, Quaternion.Euler(0, 0, -180));
            Instantiate(spikeRight, backRight.transform.position, Quaternion.Euler(0, 0, -225));
            StartCoroutine(Reloading());
            //reload = false;
            //path.enabled = false;
            //Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);
            //reload = true;
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
