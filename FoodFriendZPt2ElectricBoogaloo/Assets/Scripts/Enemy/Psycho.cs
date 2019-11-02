using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psycho : MonoBehaviour
{

    BaseEnemy baseEnemy;
    float origHealth;
    float origSpeed;
    float timer;
    [SerializeField] float speedBoost;


    void Start()
    {
        origHealth = baseEnemy.health;
        origSpeed = baseEnemy.speed;
    }

    
    void Update()
    {
        if(baseEnemy.health < origHealth / 2)
        {
            timer += Time.deltaTime;
            baseEnemy.speed += speedBoost;
            if(timer >= 3)
            {
                baseEnemy.health += 2;
                timer = 0;
            }
        }
    }
}
