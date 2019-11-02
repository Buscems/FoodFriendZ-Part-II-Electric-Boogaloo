 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{

    [SerializeField] GameObject bomb;
    BaseEnemy baseEnemy;
    Rigidbody2D rb;

    [HideInInspector] public bool hasThrown;
    [SerializeField] private float DownTime;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        hasThrown = false;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            if (hasThrown == false)
            {
                Instantiate(bomb, transform.position, Quaternion.identity);
                hasThrown = true;
            }

            if (hasThrown)
            {
                Debug.Log("start down");
                StartCoroutine(Down());
                Debug.Log("end down");
            }
         }
    }

    void ThrowBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
        hasThrown = true;
    }

    IEnumerator Down()
    {
        Debug.Log("pause");
        yield return new WaitForSeconds(DownTime);
        hasThrown = false;
    }
}
