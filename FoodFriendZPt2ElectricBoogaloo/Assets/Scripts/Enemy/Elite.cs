using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite : MonoBehaviour
{
    BaseEnemy baseEnemy;
    MeleeEnemy MeleeEnemy;
    ShootingEnemy ShootingEnemy;
    

    void Start()
    {
        baseEnemy = this.GetComponent<BaseEnemy>();
        MeleeEnemy = this.GetComponent<MeleeEnemy>();
        ShootingEnemy = this.GetComponent<ShootingEnemy>();

        baseEnemy.health = baseEnemy.health * 4;
        baseEnemy.speed = baseEnemy.speed * 2;
    }

    
    void Update()
    {
        
    }
}
