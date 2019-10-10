using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrBee : MonoBehaviour
{

    BaseEnemy baseEnemy;

    Vector3 direction;

    public GameObject honeyProjectile;

    Rigidbody2D rb;

    enum BossState { StageOne, StageTwo, Stage3 }
    BossState state;

    float maxHealth;
    float healthPercent;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        rb = GetComponent<Rigidbody2D>();

        maxHealth = baseEnemy.health;

    }

    // Update is called once per frame
    void Update()
    {

        healthPercent = baseEnemy.health / maxHealth;

    }
}
