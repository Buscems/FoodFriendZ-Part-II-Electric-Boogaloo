using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : MonoBehaviour
{

    private Vector3 playerPos;

    public BaseEnemy baseEnemy;

    public BoxCollider2D phase;

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
            transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TilesHere"){
            phase.enabled = false;
        }
        else{
            phase.enabled = true;
        }
    }
}

