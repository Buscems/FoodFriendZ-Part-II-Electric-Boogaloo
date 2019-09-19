using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{

    [Tooltip("How far away we want the enemy to be able to see the player")]
    public float aggroRange;
    [Tooltip("How long we want the enemy to be aggro after losing sight of the player")]
    public float aggroTimerMax;

    float aggroTimer;

    bool aggro;

    [Tooltip("This will be for how many players are in the game just in case we think about doing multiplayer")]
    public Transform[] target;
    Transform currentTarget;

    [HideInInspector]
    public Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        target[0] = GameObject.FindGameObjectWithTag("Player1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        AggroHandler();

        Debug.Log(aggro);

    }

    void AggroHandler()
    {

        for (int i = 0; i < target.Length; i++)
        {

            if ((target[i].transform.position - currentPos).magnitude < aggroRange && !aggro)
            {
                aggro = true;
                currentTarget = target[i];
                aggroTimer = aggroTimerMax;
            }
            else
            {
                if (aggro)
                {
                    aggroTimer -= Time.deltaTime;
                }
            }
        }

        if (aggro)
        {

            if (aggroTimer <= 0)
            {
                aggroTimer = aggroTimerMax;
                aggro = false;
                currentTarget = null;
            }

        }

    }

}
