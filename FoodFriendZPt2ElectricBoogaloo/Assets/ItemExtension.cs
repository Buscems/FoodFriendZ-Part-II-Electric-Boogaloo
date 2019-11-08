using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtension : MonoBehaviour
{
    //[ALL VARIABLES]
    ItemManager iMScript;

    BaseEnemy bEScript;

    bool needEnemyScript;


    void Awake()
    {
        //assign variables
        iMScript = GetComponent<ItemManager>();
    }

    void Update()
    {
        if (needEnemyScript)
        {

        }
    }
}
