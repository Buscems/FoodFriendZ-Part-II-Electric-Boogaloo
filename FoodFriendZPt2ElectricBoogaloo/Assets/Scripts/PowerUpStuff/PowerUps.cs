using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    //[ALL VARIABLES]
    #region [ALL VARIABLES]
    //[SCRIPTS]
    [HideInInspector] public MainPlayer stats;
    [HideInInspector] public BasePlayer baseStats;

    [Tooltip("Power-Up Names")]
    public string powerUpName = "";

    #region Base Stats
    [Header("Base Stats")]
    [Range(0, 1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float movementSpeed = 1;
    [Range(0, 1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float attackSize = 1;
    [Range(0, 1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float attackSpeed = 1;
    [Range(0, 1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float attackDamage = 1;
    #endregion

    #region Healing
    [Header("Healing")]
    public int healAmount = 0;
    public float vampHeal = 0;
    #endregion

    #region Timer Things
    [Header("Timer Things")]
    public float stunTimer;
    public float maxStunTimer;
    #endregion

    public enum Rarity { wellDone, mediumWell, mediumRare, rare }
    [Header("Rarity")]
    [Tooltip("This will be how rare the item is so that it will have different chances to appear depending on rarity")]
    public Rarity rarity;

    private float cantPickUpTime = 1;
    #endregion


    //[START]
    public void Start()
    {
        //assign main player script
        stats = GameObject.FindGameObjectWithTag("Player1").GetComponent<MainPlayer>();

        baseStats = stats.currentChar;
        GetComponent<BoxCollider2D>().enabled = false;
        gameObject.tag = "Blank";
    }

    //[UPDATE]
    private void Update()
    {
        cantPickUpTime -= Time.deltaTime;

        if (cantPickUpTime < 0)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            gameObject.tag = "StatBoost";
        }
    }

    //this gives drop down menu selects, make new line write name then put comma
    public enum PowerUpTypes
    {
        Null,//only for base stat powerups, ex. raise atk, health, speed

        #region [HEALING]
        Heal,
        FullHeal,
        #endregion

        #region [GAMEPLAY EFFECTS]
        SlowTime,
        #endregion  DebuffRemove,

        #region [ATTACK ENHANCER (poison, burn)]
        PassivePoison,
        #endregion

        #region [TEMPORARY BUFF]
        TemporaryAttackPowerUp,
        #endregion

        #region [CONDITIONAL BUFF]
        //absorb
        AbsorbAttackUp,
        AbsorbProjectile,
        AbsorbLine,

        //proportion
        AttackDmgHitProportion,
        AttackSpeedHealthProportion,
        AttackSpeedDamageProportion,

        HealthGainPerDMG,

        FatalChance,
        #endregion

        #region [ENEMY STATUS EFFECTS]
        DebuffTrail,
        EnemyConfusion,
        #endregion

        #region [ENEMY BEHAVIOR]
        AvoidPlayer,
        SlowEnemy,
        #endregion

        #region [PERMANENT ADDITION ONTO PLAYER]
        ExtraLife,
        #endregion

        //**[[ITEMS]]**
        #region [ITEMS]
        //[ATTACKS]
        Shrapnel,
        ShrapnelMod,
        RamAttackLine,
        AOETenderizer,
        AOEWhisk,
        ShootAttackLine,
        EasterEgg,
        StunAttack,
        #endregion

        ItemReedem, //coupons
        NullAttack //nullify one attack ex. plastic wrap
    }

    public PowerUpTypes currentPowerUp;

    //[COLLIDER]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    //LAST LEFT OFF - NEED TO FIND WHY THIS DOESNT WORK
    IEnumerator Pickup(Collider2D player)
    {
        switch (currentPowerUp)
        {
            #region Slow Time
            case PowerUpTypes.SlowTime:
                float timer = 2;
                yield return new WaitForSeconds(timer);
                break;
            #endregion

            #region Attack Speed Health Proportion
            case PowerUpTypes.AttackSpeedHealthProportion:

                break;
            #endregion

            #region Attack Speed Damage Proportion
            case PowerUpTypes.AttackSpeedDamageProportion:

                break;
            #endregion

            #region StunAttack
            case PowerUpTypes.StunAttack:
                stats.stunTimer = 5;

                break;
            #endregion

            #region Health Gain per damage
            case PowerUpTypes.HealthGainPerDMG:

                int totalVampHeal = Mathf.RoundToInt((baseStats.baseDamage * vampHeal));
                if (totalVampHeal >= 1)
                {
                    healAmount += totalVampHeal;
                }

                break;
            #endregion

            #region Heal
            //case6
            case PowerUpTypes.Heal:

                break;
            #endregion

            #region Full Heal
            case PowerUpTypes.FullHeal:

                stats.health = 10;      //note: not real full health, need to adjust in future
                Debug.Log("Garnish" + stats.baseDamageMulitplier);

                break;
            #endregion

            #region Temporary Attack Power Up
            case PowerUpTypes.TemporaryAttackPowerUp:

                stats.baseDamageMulitplier *= 2;
                Debug.Log("Paprika" + stats.baseDamageMulitplier);

                break;
            #endregion



            #region Null
            //case last - null for base stats - DO NOT FILL IN LEAVE BLANK
            case PowerUpTypes.Null:

                break;
            #endregion
            default:
                break;
        }
    }
}