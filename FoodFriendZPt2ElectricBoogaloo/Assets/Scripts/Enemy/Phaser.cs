﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : MonoBehaviour
{

    private Vector3 playerPos;

    public BaseEnemy baseEnemy;

    public BoxCollider2D phase;

    public bool midPhase;

    public Rigidbody2D rb;

    public AudioSource phaseSound;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        phase.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {  

        if (baseEnemy.aggroScript.aggro == true)
        {
            playerPos = baseEnemy.aggroScript.currentTarget.transform.position;

            Vector3 direction = (playerPos - baseEnemy.aggroScript.currentPos).normalized;
            Vector2 force = direction * baseEnemy.speed * Time.deltaTime;
            rb.MovePosition(rb.position + force);
            
        }

        if (midPhase == true){
            phase.enabled = false;
        }

        if (midPhase == false){
            phase.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TilesHere")
        {
            midPhase = true;
            phase.enabled = false;
            phaseSound.Play();
            Debug.Log("entered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        midPhase = false;
        phase.enabled = true;
    }
}


