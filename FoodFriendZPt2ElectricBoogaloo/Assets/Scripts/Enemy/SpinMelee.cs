using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMelee : MonoBehaviour
{
    BaseEnemy baseEnemy;
    Spinning spin;
    MainPlayer MainPlayer;

    public int damage;

    public GameObject meleeObject;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        spin = GetComponent<Spinning>();
        MainPlayer = GetComponent<MainPlayer>();
    }

    
    void Update()
    {

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(damage);
        }
    }
}
