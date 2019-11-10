using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Script : MonoBehaviour
{
    //[ALL VARIABLES]
    [HideInInspector] public List<BaseEnemy> baseEnemies;

    void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            baseEnemies.Add(collision.gameObject.GetComponent<BaseEnemy>());
        }
    }
}
