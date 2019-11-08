using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtension : MonoBehaviour
{
    //[ALL VARIABLES]
    ItemManager iMScript;

    [HideInInspector] public BaseEnemy bEScript;

    GetOddsScript gOScript;

    //bools
    [HideInInspector] public bool needEnemyScript;

    [HideInInspector] public bool hasJunkFood;
    float junkFoodChance = .25f;
    float junkFoodModifer = .8f;

    [HideInInspector] public bool hasPlayerHitEnemy;



    void Awake()
    {
        //assign variables
        iMScript = GetComponent<ItemManager>();
        gOScript = GetComponent<GetOddsScript>();
    }

    void Update()
    {
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
    }
}
