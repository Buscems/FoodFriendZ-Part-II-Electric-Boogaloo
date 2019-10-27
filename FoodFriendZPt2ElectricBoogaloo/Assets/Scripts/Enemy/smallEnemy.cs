using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemy : MonoBehaviour
{

    private Vector3 playerPos;

    BaseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

        if (baseEnemy.aggroScript.aggro)
        {
            playerPos = baseEnemy.aggroScript.currentTarget.transform.position;

            transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(1);
        }
    }

}
