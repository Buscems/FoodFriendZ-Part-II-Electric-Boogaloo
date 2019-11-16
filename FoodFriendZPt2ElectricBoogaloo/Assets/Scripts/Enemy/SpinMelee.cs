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
    [SerializeField] float turnspeed;
    [HideInInspector] public bool swordActive;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        spin = GetComponent<Spinning>();
        MainPlayer = GetComponent<MainPlayer>();
        meleeObject.SetActive(false);
        swordActive = false;
    }

    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            meleeObject.SetActive(true);
            swordActive = true;
            
        } else
        {
            meleeObject.SetActive(false);
            swordActive = false;
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
