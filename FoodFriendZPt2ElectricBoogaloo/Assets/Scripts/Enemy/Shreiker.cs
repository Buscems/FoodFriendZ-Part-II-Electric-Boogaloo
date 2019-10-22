using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shreiker : MonoBehaviour
{
    Aggro agrro;
    BaseEnemy baseEnemy;
    private bool alerted;
    CircleCollider2D Shriekcollider;

    void Start()
    {
        agrro = GetComponent<Aggro>();
        baseEnemy = GetComponent<BaseEnemy>();
        Shriekcollider = GetComponent<CircleCollider2D>();
        alerted = false;
    }


    void Update()
    {
        Shriekcollider.radius += 1 * Time.deltaTime;
        if(Shriekcollider.radius > 7)
        {
            Shriekcollider.radius = 7;
        }

        if (baseEnemy.aggroScript.aggro)
        {

            alerted = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Enemy" && alerted)
        {
            Debug.Log("alerting");
            collision.gameObject.GetComponent<BaseEnemy>().aggroScript.aggroRange = collision.gameObject.GetComponent<BaseEnemy>().aggroScript.aggroRange * 2;
            collision.gameObject.GetComponent<BaseEnemy>().aggroScript.aggro = true;
        }
    }
}
