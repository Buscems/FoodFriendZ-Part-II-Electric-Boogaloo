using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unkillable : MonoBehaviour
{
    BaseEnemy baseEnemy;

    private float starthealth;
    [SerializeField] int damage;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();

        starthealth = baseEnemy.health;
        
    }

    
    void Update()
    {
        if(baseEnemy.health <= 1)
        {
            baseEnemy.health = starthealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<MainPlayer>().health -= damage;
        }
    }
}
