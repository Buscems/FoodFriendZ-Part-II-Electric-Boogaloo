using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : MonoBehaviour
{

    private Vector3 playerPos;

    public BaseEnemy baseEnemy;

    public BoxCollider2D phase;

    public bool midPhase;

    public Rigidbody2D rb;

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
            Debug.Log("entered");
        }
        else
        {
            midPhase = false;
            phase.enabled = true;
        }
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(1);
        }
    }
    }


