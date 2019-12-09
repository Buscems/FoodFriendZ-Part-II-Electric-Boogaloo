using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUps : MonoBehaviour
{
    //[ALL VARIABLES]
    #region [ALL VARIABLES]

    float storedSpeedMult;

    //scripts
    public MainPlayer stats;
    [HideInInspector] public BasePlayer baseStats;

    ItemExtension ieScript;
    ItemManager imScript;

    Collider2D myCollider;

    public bool effectIsActive = true;

    [Space]
    public string powerUpName = "";
    public string powerUpDesc;
    [Space]

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

    [Range(0, 5)]
    [Tooltip("This number will reflect how much of an increase in stat the player gets")]
    public float critChance = 1;

    public float bleedChance = 1;
    public float burnChance = 1;
    public float poisonChance = 1;
    public float freezeChance = 1;
    public float stunChance = 1;

    #endregion

    #region Healing
    [Header("Healing")]
    public int healAmount = 0;
    public float vampHeal = 0;
    #endregion

    [Header("Timer Things")]
    public float stunTimer;
    public float maxStunTimer;

    public enum Rarity { wellDone, mediumWell, mediumRare, rare }
    [Header("Rarity")]

    [Tooltip("This will be how rare the item is so that it will have different chances to appear depending on rarity")]
    public Rarity rarity;

    //for chest
    private float cantPickUpTime = 1;

    //cooldown/ effect duration
    public float effectDuration;
    public float maxCoolDownDuration;
    #endregion

    //[AWAKE]
    public void Awake()
    {
        //assign main player script
        stats = GameObject.FindGameObjectWithTag("Player1").GetComponent<MainPlayer>();
        ieScript = GameObject.FindGameObjectWithTag("Player1").GetComponent<ItemExtension>();
        imScript = GameObject.FindGameObjectWithTag("Player1").GetComponent<ItemManager>();

        //temporary fail safe
        if (powerUpDesc == null || powerUpDesc == "")
        {
            powerUpDesc = "Nothing atm";
        }

        baseStats = stats.currentChar;
    }

    public enum PowerUpTypes
    {
        Null,//only for base stat powerups, ex. raise atk, health, speed

        #region [NEW EQUIPMENT]
        Coconut,
        KitchenKnife,
        SweetCarolina,
        RatPosion,
        IceCubes,
        CookingAppron,
        Salt,
        //NOV 8 
        JunkFood,
        PeacefulTea,
        PineAppleSlice,
        PopCorn,
        GreenMushroom,
        Spatula,
        LunchTray,
        KissTheCookApron,
        Popsicle,
        BagOfIce,
        RocketPopsicle,
        //NOV 11
        IcedTea,
        #endregion

        #region [OLD EQUIPMENT]
        Heal,
        FullHeal,

        SlowTime,
        DebuffRemove,

        PassivePoison,

        TemporaryAttackPowerUp,

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

        DebuffTrail,
        EnemyConfusion,

        AvoidPlayer,
        SlowEnemy,
        #endregion

        //**[[ITEMS]]**
        #region [ITEMS]


        //[non-attacks]
        Rosemary,
        Eggtimer,

        //[ATTACKS]
        Shrapnel,
        ShrapnelMod,
        RamAttackLine,
        AOETenderizer,
        AOEWhisk,
        ShootAttackLine,
        EasterEgg,
        StunAttack,
        Starfruit,

        //NOV8
        EspressoShot,
        #endregion

    }

    public PowerUpTypes currentPowerUp;

    public IEnumerator Pickup()
    {
        switch (currentPowerUp)
        {
            //[NEW EQUIPMENT]
            #region Cooking Appron
            case PowerUpTypes.CookingAppron:
                print("Using Cooking appron");

                //[STAT BOOST]
                stats.evasiveChance = .1f;

                yield return null;
                break;
            #endregion

            #region Salt
            case PowerUpTypes.Salt:

                //[STAT BOOST]
                ieScript.needEnemyScript = true;
                ieScript.hasSalt = true;
                yield return null;
                break;
            #endregion

            #region Coconut
            case PowerUpTypes.Coconut:
                print("Using Coconut");

                //[STAT BOOST]
                stunChance = 1;

                yield return null;
                break;
            #endregion

            #region KitchenKnife
            case PowerUpTypes.KitchenKnife:
                print("Using KitchenKnife");

                //[STAT BOOST]
                bleedChance = 1;

                yield return null;
                break;
            #endregion

            #region SweetCarolina
            case PowerUpTypes.SweetCarolina:
                print("Using SweetCarolina");

                //[STAT BOOST]
                burnChance = 1;

                yield return null;
                break;
            #endregion

            #region RatPoison
            case PowerUpTypes.RatPosion:
                print("Using RatPoison");

                //[STAT BOOST]
                poisonChance = 1;

                yield return null;
                break;
            #endregion

            #region IceCubes
            case PowerUpTypes.IceCubes:
                print("Using IceCubes");

                //[STAT BOOST]
                freezeChance = 1;

                yield return null;
                break;
            #endregion
            //[NOV 8]
            #region JunkFood
            case PowerUpTypes.JunkFood:
                print("Using JunkFood");

                //[STAT BOOST]
                ieScript.needEnemyScript = true;
                ieScript.hasJunkFood = true;

                yield return null;
                break;
            #endregion

            #region PeacefulTea
            case PowerUpTypes.PeacefulTea:
                print("Using Peaceful Tea");

                //[STAT BOOST]
                ieScript.EnableEnemyDetector();
                ieScript.hasPeacefulTea = true;

                yield return null;
                break;
            #endregion

            #region PineAppleSlice
            case PowerUpTypes.PineAppleSlice:

                //[STAT BOOST]
                ieScript.needEnemyScript = true;
                ieScript.hasPineAppleSlice = true;

                yield return null;
                break;
            #endregion

            #region PopCorn
            case PowerUpTypes.PopCorn:

                //[STAT BOOST]
                ieScript.needEnemyScript = true;
                ieScript.hasPopcorn = true;

                yield return null;
                break;
            #endregion

            #region Popsicle
            case PowerUpTypes.Popsicle:

                //[STAT BOOST]
                imScript.rechargeRateMultiplier += 1f;
                yield return null;
                break;
            #endregion

            #region BagOfIce
            case PowerUpTypes.BagOfIce:
                imScript.rechargeRateMultiplier = 1 + GetProportional(imScript.rechargeRateMultiplier, stats.health, 10, false);
                // print(imScript.rechargeRateMultiplier);

                yield return null;
                break;
            #endregion

            #region RocketPopsicle
            case PowerUpTypes.RocketPopsicle:
                ieScript.needEnemyScript = true;
                ieScript.hasRocketPopsicle = true;

                yield return null;
                break;
            #endregion

            #region GreenMushroom
            case PowerUpTypes.GreenMushroom:

                //[STAT BOOST]
                stats.GreenMushrooms++;
                yield return null;
                break;
            #endregion

            #region Spatula
            case PowerUpTypes.Spatula:

                //[STAT BOOST]
                ieScript.needBossScript = true;
                ieScript.hasSpatula = true;

                yield return null;
                break;
            #endregion

            #region LunchTray
            case PowerUpTypes.LunchTray:

                //[STAT BOOST]
                ieScript.needProjectileScript = true;
                ieScript.hasLunchTray = true;

                yield return null;
                break;
            #endregion

            #region KissTheCookApron
            case PowerUpTypes.KissTheCookApron:
                ieScript.needEnemyScript = true;
                ieScript.hasKissTheCookApron = true;
                //[STAT BOOST]

                yield return null;
                break;
            #endregion
            //[NOV 11]
            #region IcedTea
            case PowerUpTypes.IcedTea:

                ieScript.hasIcedTea = true;

                yield return null;
                break;
            #endregion

            //**********************************

            #region Slow Time
            case PowerUpTypes.SlowTime:
                float timer = 2;
                yield return new WaitForSeconds(timer);
                break;
            #endregion

            #region Attack Speed Health Proportion
            case PowerUpTypes.AttackSpeedHealthProportion:
                //tempoaray variable
                float hotSauceBoost = 2 - ((float)stats.health / 10);

                Debug.Log("Hot Sauce Boost: " + hotSauceBoost);

                stats.attackSpeedMultiplier *= hotSauceBoost;

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

            //[HEALING]
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

            //[TEMPORARY BUFFS]
            #region Temporary Attack Power Up
            case PowerUpTypes.TemporaryAttackPowerUp:

                if (effectIsActive)
                {
                    stats.baseDamageMulitplier *= 2;
                }
                else
                {
                    stats.baseDamageMulitplier /= 2;
                }

                break;
            #endregion

            //**[[ITEM]]**
            #region Rosemary
            case PowerUpTypes.Rosemary:

                stats.health++;

                break;
            #endregion

            #region Starfruit
            case PowerUpTypes.Starfruit:
                //[ACTIVE STATE]
                if (effectIsActive)
                {
                    Debug.Log("Effect is active");
                    stats.gameObject.layer = 15;    //invincible
                }
                //[DEACTIVE STATE]
                else
                {
                    Debug.Log("Effect is deactivated");
                    stats.gameObject.layer = 8;     //player
                }
                break;
            #endregion

            #region Eggtimer
            case PowerUpTypes.Eggtimer:
                //[ACTIVE STATE]
                if (effectIsActive)
                {
                    Debug.Log("Effect is active");
                    stats.critChanceMultiplier *= 3;
                }
                //[DEACTIVE STATE]
                else
                {
                    Debug.Log("Effect is deactivated");
                    stats.critChanceMultiplier /= 3;
                    effectIsActive = false;
                }
                break;
            #endregion

            //NOV8
            #region EspressoShot
            case PowerUpTypes.EspressoShot:
                //[ACTIVE STATE]
                if (effectIsActive)
                {
                    Debug.Log("Effect is active");
                    storedSpeedMult = stats.speedMultiplier;
                    stats.speedMultiplier = 3;
                }
                //[DEACTIVE STATE]
                else
                {
                    Debug.Log("Effect is deactivated");
                    stats.speedMultiplier = storedSpeedMult;
                    effectIsActive = false;
                }
                break;
            #endregion

            #region Null
            case PowerUpTypes.Null:
                break;
            #endregion
            default:
                break;
        }
    }

    //[PROPORTIONAL VALUE GENERATOR]
    #region [PROPORTIONAL VALUE GENERATOR]
    public float GetProportional(float modifiedValue, float curValue, float maxValue, bool isScalingUp)
    {
        //value to be returned
        float newModifiedValue = 0;

        //[Proportionally Higher (low input low output)]
        if (isScalingUp)
        {
            newModifiedValue = (curValue / maxValue);
        }

        //[Proportionally Lower (low input high output)]
        else
        {
            newModifiedValue = 1 - (curValue / maxValue);
        }

        return newModifiedValue;
    }
    #endregion
}