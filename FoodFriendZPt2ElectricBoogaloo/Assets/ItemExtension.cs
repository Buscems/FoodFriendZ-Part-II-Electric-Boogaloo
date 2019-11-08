using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtension : MonoBehaviour
{
    //[ALL VARIABLES]
    ItemManager iMScript;
    MainPlayer mPScript;

    [HideInInspector] public BaseEnemy bEScript;
    [HideInInspector] public BaseEnemy bEScript_mAtkPlayer;

    GameObject eDetectGO;


    GetOddsScript gOScript;

    //bools
    [HideInInspector] public bool needEnemyScript;

    [HideInInspector] public bool hasJunkFood;
    float junkFoodChance = .25f;
    float junkFoodModifer = .8f;

    [HideInInspector] public bool hasPeacefulTea;

    [HideInInspector] public bool hasPineAppleSlice;
    float pineAppleSliceChance = .5f;


    [HideInInspector] public bool hasPlayerHitEnemy;
    [HideInInspector] public bool hasEnemyHitPlayer;
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
                    print("pineappleslice");
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

                //reset
                hasPlayerHitEnemy = false;
            }
        }

        if (hasPeacefulTea)
        {
            mPScript.isFast = !areEnemiesInProxy;
        }

    }

    public void EnableEnemyDetector()
    {
        eDetectGO.SetActive(true);
    }
}
