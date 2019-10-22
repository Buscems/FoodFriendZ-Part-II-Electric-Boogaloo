using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    [HideInInspector]
    public MainPlayer stats;
    [HideInInspector]
    public BasePlayer baseStats;

    [Tooltip("Power-Up Names")]
    public string powerUpName = "";

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

    [Header("Healing")]
    public int healAmount = 0;
    public float vampHeal = 0;

    [Header("Timer Things")]
    public float stunTimer;
    public float maxStunTimer;

    public enum Rarity { wellDone, mediumWell, mediumRare, rare }
    [Header("Rarity")]
    [Tooltip("This will be how rare the item is so that it will have different chances to appear depending on rarity")]
    public Rarity rarity;


    public void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player1").GetComponent<MainPlayer>();
        baseStats = stats.currentChar;
    }

    //this gives drop down menu selects, make new line write name then put comma
    public enum PowerUpTypes
    {
        Null,

        //[HEALING]
        Heal,
        FullHeal,

        //[PLAYER STATUS EFFECTS]
        DebuffRemove,
        SlowTime,

        //[ATTACK ENHANCER]
        PassivePoison,

        //[CONDITIONAL BUFF]
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

        //[ENEMY STATUS EFFECTS]
        DebuffTrail,
        EnemyConfusion,

        //[ENEMY BEHAVIOR]
        AvoidPlayer,
        SlowEnemy,

        //[PERMANENT ADDITION ONTO PLAYER]
        ExtraLife,

        //**[[ITEMS]]**
        //[ATTACKS]
        Shrapnel,
        ShrapnelMod,
        RamAttackLine,
        AOETenderizer,
        AOEWhisk,
        ShootAttackLine,
        EasterEgg,
        StunAttack,


        ItemReedem, //??
        NullAttack //??
    }

    public PowerUpTypes currentPowerUp;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }//END OF ON TRIGGER

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
            //case6
            case PowerUpTypes.FullHeal:

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