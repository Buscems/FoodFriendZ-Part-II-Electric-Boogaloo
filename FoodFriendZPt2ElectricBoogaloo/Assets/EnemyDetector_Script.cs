using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector_Script : MonoBehaviour
{
    //[ALL VARIABLES]
    ItemExtension ieScript;

    public int numOfEnemiesInProxy;
    int _numOfEnemiesInProxy;

    void Awake()
    {
        //assign variables
        ieScript = GetComponentInParent<ItemExtension>();

        //detector setup
        _numOfEnemiesInProxy = numOfEnemiesInProxy;
    }

    void Update()
    {
        //detector
        if (numOfEnemiesInProxy != _numOfEnemiesInProxy)
        {
            //returns bool
            ieScript.areEnemiesInProxy = areEnemiesInProxy(numOfEnemiesInProxy);

            //reset detector
            _numOfEnemiesInProxy = numOfEnemiesInProxy;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            numOfEnemiesInProxy++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            numOfEnemiesInProxy--;
        }
    }

    public bool areEnemiesInProxy(int numOfEnemies)
    {
        if (numOfEnemies <= 0)
        {
            numOfEnemiesInProxy = 0;

            return false;
        }
        else
        {
            return true;
        }
    }
}
