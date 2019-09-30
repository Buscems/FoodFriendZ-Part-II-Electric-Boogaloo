using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryShieldZone : MonoBehaviour
{

    public Parry parry;
    GameObject Foe;

    void Start()
    {
        //parry = GetComponent<Parry>();
        Foe = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Projectile" || collision.gameObject.name == "Sword Holder Holder")
        {
            parry.isBlocking = true;
            Debug.Log(parry.isBlocking);
        }
    }
}
