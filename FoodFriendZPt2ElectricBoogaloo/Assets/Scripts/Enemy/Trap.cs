using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    MainPlayer mainPlayer;
    BaseEnemy baseEnemy;

    private float resetSpeed;
    public float TrapTime;
    private bool done;

    void Start()
    {
        mainPlayer = GetComponent<MainPlayer>();
        baseEnemy = GetComponent<BaseEnemy>();

        resetSpeed = mainPlayer.currentChar.speed;
        done = false;
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            mainPlayer.currentChar.speed = 0;
            StartCoroutine(Trapped());
        }

        if (done)
        {
            Debug.Log("self destruct");
            Destroy(gameObject);
        }
    }

    IEnumerator Trapped()
    {
        Debug.Log("start trap");
       yield return new WaitForSeconds(TrapTime);
        Debug.Log("end trap");
        mainPlayer.currentChar.speed = resetSpeed;
        done = true;
    }
}
