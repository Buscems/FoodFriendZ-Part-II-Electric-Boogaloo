using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unkillable : MonoBehaviour
{
    BaseEnemy baseEnemy;

    private float starthealth;

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
}
