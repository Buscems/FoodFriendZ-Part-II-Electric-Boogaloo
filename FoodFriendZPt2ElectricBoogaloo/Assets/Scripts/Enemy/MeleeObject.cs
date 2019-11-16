using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeObject : MonoBehaviour
{

    [SerializeField] int damage;
    BaseEnemy baseEnemy;
    [SerializeField] SpinMelee spinmelee;
    GameObject player;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        player = GameObject.FindGameObjectWithTag("Player1");
    }

    void Update()
    {
        if (spinmelee.swordActive)
        {
            Vector2.MoveTowards(transform.position, player.transform.position, 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(damage);
        }
    }
}
