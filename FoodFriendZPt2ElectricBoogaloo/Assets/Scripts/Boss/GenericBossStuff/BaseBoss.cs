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


    private float bleedTimer = 0;
    private float bleedDamage;
    private float bleedTickRate = 0;
    private float currentBleedTickRate = 0;
    private float burnTimer = 0;
    private float burnDamage;
    private float poisonTimer = 0;
    private float poisonDamage;
    private float freezeTimer = 0;
    private float stunTimer = 0;

    private float slowDownPercentage = 1;

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

        speed = speed * slowDownPercentage;

        StatusEffectTimers();
        StatusEffects();
    }

    private void StatusEffectTimers()
    {
        bleedTimer -= Time.deltaTime;
        currentBleedTickRate -= Time.deltaTime;
        burnTimer -= Time.deltaTime;
        poisonTimer -= Time.deltaTime;
        freezeTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
    }

    public void StatusEffects()
    {
        if (bleedTimer > 0)
        {
            if (currentBleedTickRate < 0)
            {
                currentBleedTickRate = bleedTickRate;
                health -= bleedDamage;
            }
        }

        if (burnTimer > 0)
        {
            health -= burnDamage;
        }

        if (poisonTimer > 0)
        {
            health -= poisonDamage;
        }
        else
        {
            slowDownPercentage = 1;
        }

        if (freezeTimer < 0 && stunTimer < 0)
        {
            slowDownPercentage = 1;
        }

    }

    public void SetBleed(float[] _bleedVariables)
    {
        bleedDamage = _bleedVariables[0];
        bleedTimer = _bleedVariables[1];
        bleedTickRate = _bleedVariables[2];
    }
    public void SetBurn(float[] _burnVariables)
    {
        burnDamage = _burnVariables[0];
        burnTimer = _burnVariables[1];
    }
    public void SetPoison(float[] _poisonVariables)
    {
        poisonDamage = _poisonVariables[0];
        poisonTimer = _poisonVariables[1];
        freezeTimer = _poisonVariables[1]; ;
        stunTimer = _poisonVariables[1];
        slowDownPercentage = _poisonVariables[2];
    }
    public void SetFreeze(float[] _freezeVariables)
    {
        freezeTimer = _freezeVariables[0];
        stunTimer = _freezeVariables[0];
        slowDownPercentage = _freezeVariables[1];
    }
    public void SetStun(float _stun)
    {
        stunTimer = _stun;
        freezeTimer = _stun;
        slowDownPercentage = 0;
    }

    public void Death()
    {
        
    }

}
