using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMelee : MonoBehaviour
{
    BaseEnemy baseEnemy;
    Spinning spin;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        spin = GetComponent<Spinning>();
    }

    
    void Update()
    {

        if (spin.meleeDelete)
        {
            Destroy(gameObject);
        }

    }
}
