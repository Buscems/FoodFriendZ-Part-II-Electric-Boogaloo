using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    BaseEnemy BaseEnemy;
    MainPlayer MainPlayer;

    public GameObject Melee;
    public Vector2 startPos;

    public int damage;

    void Start()
    {
        BaseEnemy = this.GetComponent<BaseEnemy>();
        MainPlayer = this.GetComponent<MainPlayer>();
        startPos = transform.position;
    }

    
    void Update()
    {
        if (BaseEnemy.aggroScript.aggro)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            MainPlayer.health = MainPlayer.health - damage;
        }
    }
}
