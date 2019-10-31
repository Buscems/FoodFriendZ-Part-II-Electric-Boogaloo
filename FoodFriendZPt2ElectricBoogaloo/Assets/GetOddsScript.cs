using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOddsScript : MonoBehaviour
{
    //note: bad name, this a generic RNG
    public bool GetStunOdds(float chance)
    {
        bool isSuccessful = false;

        //rng for deteriminging success


        if (Random.Range(0.0f, 1.0f) <= chance)
        {
            isSuccessful = true;
        }
        return isSuccessful;
    }
}
