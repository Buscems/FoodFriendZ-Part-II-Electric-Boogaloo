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

    //[HideInInspector]
    public bool aggro;

    [Tooltip("This will be for how many players are in the game just in case we think about doing multiplayer")]
    public Transform[] target;
    [HideInInspector]
    public Transform currentTarget;

    [HideInInspector]
    public Vector3 currentPos;

    private void Awake()
    {
        target[0] = GameObject.FindGameObjectWithTag("Player1").transform;
        currentTarget = target[0];
        aggro = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        target[0] = GameObject.FindGameObjectWithTag("Player1").transform;
        aggro = false;
    }

    // Update is called once per frame
    void Update()
    {
        AggroHandler();

        //currentPos = this.transform.position;
        currentPos = this.transform.position;

        //Debug.Log(aggro);
        //Debug.Log((target[0].transform.position - currentPos).magnitude);

    }

    void AggroHandler()
    {
        //if this is a regular enemy, do the normal aggro things
        if (this.GetComponent<BaseEnemy>() != null)
        {
            for (int i = 0; i < target.Length; i++)
            {

                if ((target[i].transform.position - currentPos).magnitude < aggroRange)
                {
                    currentTarget = target[i];
                    aggro = true;
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
                    aggro = false;
                    currentTarget = null;
                    aggroTimer = aggroTimerMax;
                }

            }
        }
        //if this is a boss, ignore the normal aggro things and just set it true if the player enters the room in the room trigger script
    }

}
