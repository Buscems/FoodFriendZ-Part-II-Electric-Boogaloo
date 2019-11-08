using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtension : MonoBehaviour
{
    //[ALL VARIABLES]
    ItemManager iMScript;
    MainPlayer mPScript;

    [HideInInspector] public BaseEnemy bEScript;

    GameObject eDetectGO;


    GetOddsScript gOScript;

    //bools
    [HideInInspector] public bool needEnemyScript;

    [HideInInspector] public bool hasJunkFood;
    float junkFoodChance = .25f;
    float junkFoodModifer = .8f;

    [HideInInspector] public bool hasPeacefulTea;


    [HideInInspector] public bool hasPlayerHitEnemy;
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
        if (needEnemyScript && hasPlayerHitEnemy)
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

        if (hasPeacefulTea)
        {
            mPScript.isFast = areEnemiesInProxy;
        }
    }

    public void EnableEnemyDetector()
    {
        eDetectGO.SetActive(true);
    }
}
