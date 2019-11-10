using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtension : MonoBehaviour
{
    //[ALL VARIABLES]
    //scripts
    ItemManager iMScript;
    MainPlayer mPScript;

    [HideInInspector] public BaseBoss bossScript;

    [HideInInspector] public EnemyBullet projectileScripts;

    [HideInInspector] public BaseEnemy bEScript;
    [HideInInspector] public BaseEnemy bEScript_mAtkPlayer;

    GameObject eDetectGO;

    GetOddsScript gOScript;

    //prefabs
    public GameObject AOE;
    [HideInInspector] public AOE_Script aoeScript;

    //bools
    [HideInInspector] public bool hasSalt;
    [HideInInspector] public bool hasSpatula;
    [HideInInspector] public bool hasPeacefulTea;

    [HideInInspector] public bool hasKissTheCookApron;
    float kissTheCookChance = 1f;

    [HideInInspector] public bool hasJunkFood;
    float junkFoodChance = .25f;
    float junkFoodModifer = .8f;

    [HideInInspector] public bool hasPopcorn;
    float popCornChance = 1f;

    [HideInInspector] public bool hasPineAppleSlice;
    float pineAppleSliceChance = .5f;

    [HideInInspector] public bool hasLunchTray;
    float lunchTrayChance = 1f;

    //enemy related bools
    [HideInInspector] public bool needEnemyScript;
    [HideInInspector] public bool needBossScript;
    [HideInInspector] public bool needProjectileScript;

    [HideInInspector] public bool hasPlayerHitEnemy;
    [HideInInspector] public bool hasEnemyHitPlayer;

    [HideInInspector] public bool hasProjectileHitPlayer;

    [HideInInspector] public bool hasPlayerHitBoss;
    [HideInInspector] public bool hasBossHitPlayer;

    [HideInInspector] public bool areEnemiesInProxy;


    void Awake()
    {
        //assign variables
        iMScript = GetComponent<ItemManager>();
        mPScript = GetComponent<MainPlayer>();
        gOScript = GetComponent<GetOddsScript>();

        eDetectGO = GameObject.Find("EnemyDetector");
        eDetectGO.SetActive(false);
    }

    void Update()
    {
        //if enemy script is needed
        if (needEnemyScript)
        {
            if (hasEnemyHitPlayer)
            {
                if (hasPineAppleSlice)
                {
                    if (gOScript.GetStunOdds(pineAppleSliceChance))
                    {
                        bEScript_mAtkPlayer.health *= .5f;
                    }
                }

                //reset detector
                hasEnemyHitPlayer = false;
            }

            if (hasPlayerHitEnemy)
            {
                if (hasJunkFood)
                {
                    if (gOScript.GetStunOdds(junkFoodChance))
                    {
                        bEScript.speed *= junkFoodModifer;
                    }
                }

                if (hasSalt)
                {
                    bEScript.health *= .9f;
                }

                if (hasPopcorn)
                {
                    print("has PopCorn");
                    if (gOScript.GetStunOdds(popCornChance))
                    {
                        Instantiate(AOE, bEScript.transform.position, Quaternion.identity);

                    }
                }

                if (hasKissTheCookApron)
                {
                    if (gOScript.GetStunOdds(kissTheCookChance))
                    {
                        mPScript.health++;
                    }
                }
                //reset
                hasPlayerHitEnemy = false;
            }
        }

        if (needBossScript)
        {
            if (hasPlayerHitBoss)
            {
                if (hasSpatula)
                {
                    print("has Spatula");

                    bossScript.health *= .9f;
                }
            }

            //reset
            hasPlayerHitBoss = false;
        }

        if (needProjectileScript)
        {
            if (hasProjectileHitPlayer)
            {
                if (hasLunchTray)
                {
                    if (gOScript.GetStunOdds(lunchTrayChance))
                    {
                        projectileScripts.returnToSender = true;
                    }
                }
            }

            //reset
            hasProjectileHitPlayer = false;
        }

        if (hasPeacefulTea)
        {
            //if enemies are not in proxy, make player fast
            mPScript.isFast = !areEnemiesInProxy;
        }


    }

    public void EnableEnemyDetector()
    {
        eDetectGO.SetActive(true);
    }
}
