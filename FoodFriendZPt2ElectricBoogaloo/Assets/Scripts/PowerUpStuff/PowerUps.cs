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

    [Header ("Base Stats")]
    [Range(0,1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float movementSpeed = 1;
    [Range(0,1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float attackSize = 1;
    [Range(0,1)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float attackSpeed = 1;
    [Range(0,1)]
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
    //if last line, do not put comma at end of word
    public enum PowerUpTypes
    {
        Null,//see this one has a comma
        Heal,
        DebuffRemove,
        SlowTime,
        AvoidPlayer,
        SlowEnemy,
        AbsorbAttackUp,
        AbsorbProjectile,
        AbsorbLine,
        Shrapnel,
        ShrapnelMod,
        DebuffTrail,
        RamAttackLine,
        AOETenderizer,
        AOEWhisk,
        ShootAttackLine,
        EasterEgg,
        StunAttack,
        AttackSpeedHealthProportion,
        AttackSpeedDamageProportion,
        NullAttack,
        Poison,
        ItemReedem,
        AttackDmgHitProportion,
        PaasivePoison,
        ExtraLife,
        EnemyConfusion,
        HealthGainPerDMG,
        FatalChance//last one so no comma

    }//END OF ENUMS

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
            //case 1
            case PowerUpTypes.SlowTime:

                //WRITE STUFF HERE
                
                float timer = 2;
                yield return new WaitForSeconds(timer);
                break;

            //case2
            case PowerUpTypes.AttackSpeedHealthProportion:

                break;

            //case3
            case PowerUpTypes.AttackSpeedDamageProportion:

                break;

            //case4
            case PowerUpTypes.StunAttack:
                stats.stunTimer = 5;

                break;
            //case5
            case PowerUpTypes.HealthGainPerDMG:

                int totalVampHeal = Mathf.RoundToInt((baseStats.baseDamage * vampHeal));
                if (totalVampHeal >= 1)
                {
                    healAmount += totalVampHeal;
                }

                break;    
            //case6
            case PowerUpTypes.Heal:
                
                break;
            //case last - null for base stats - DO NOT FILL IN LEAVE BLANK
            case PowerUpTypes.Null:

                break;

            default:
                Debug.Log("Basic Power Up");
                break;


        }//END OF SWITCH
    }//END OF IENUMERATOR    
}//END OF SCRIPT