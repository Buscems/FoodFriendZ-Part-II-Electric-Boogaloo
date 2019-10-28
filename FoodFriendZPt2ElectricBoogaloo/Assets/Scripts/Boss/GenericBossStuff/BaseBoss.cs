using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{

    [Header("Generic Enemy Values")]
    [Tooltip("How much health the enemy will have(This will be a high number for now so that the player can have high damage numbers")]
    public float maxHealth;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float healthPercent;
    [Tooltip("How fast we want the enemy to move")]
    public float speed;
    [Tooltip("How much damage this enemy deals to the player when the player runs into them (Should only be between 0 and 1)")]
    [Range(0, 1)]
    public int walkIntoDamage;

    Animator anim;

    [Tooltip("How much money the boss will drop when killed")]
    public int money;

    public Aggro aggroScript;

    public enum BossStage { stage1, stage2, stage3}
    [HideInInspector]
    public BossStage stage;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //keeping track of the percentage of the bosses health to have different stages
        healthPercent = health / maxHealth;

        if(healthPercent > .5f)
        {
            stage = BossStage.stage1;
        }
        if (healthPercent > .25f)
        {
            stage = BossStage.stage2;
        }
        else
        {
            stage = BossStage.stage3;
        }

    }

    public void Death()
    {
        
    }

}
