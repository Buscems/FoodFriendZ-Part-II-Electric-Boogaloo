using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    BaseEnemy baseEnemy;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<MainPlayer>().health--;
            Destroy(gameObject);
        }
    }
}
