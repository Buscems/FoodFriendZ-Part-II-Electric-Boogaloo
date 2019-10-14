using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite : MonoBehaviour
{
    BaseEnemy baseEnemy;
    MeleeEnemy MeleeEnemy;
    ShootingEnemy ShootingEnemy;
    EnemyBullet Bullet;
    

    void Start()
    {
        baseEnemy = this.GetComponent<BaseEnemy>();
        MeleeEnemy = this.GetComponent<MeleeEnemy>();
        ShootingEnemy = this.GetComponent<ShootingEnemy>();
        Bullet = this.GetComponent<EnemyBullet>();

        baseEnemy.health = baseEnemy.health * 4;
        baseEnemy.speed = baseEnemy.speed * 2;
        Bullet.damage = Bullet.damage * 2;
    }

    
    void Update()
    {
        
    }
}
