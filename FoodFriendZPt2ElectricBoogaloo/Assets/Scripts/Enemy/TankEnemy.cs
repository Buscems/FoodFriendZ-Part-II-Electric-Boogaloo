using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{

    private Vector3 playerPos;

    public float attackRange;

    public bool follow;
    public bool windUp;
    public bool attack;
    public bool attackReady;
    public bool coolDownAttack;

    public float followTime;
    public float windUpTime;
    public float attackTime;
    public float coolDownTime;

    public BoxCollider2D attackRadius;

    BaseEnemy baseEnemy;

    public PathfindingAI path;

    // Start is called before the first frame update
    void Start()
    {
        attackRadius.enabled = false;
        baseEnemy = GetComponent<BaseEnemy>();
        path = GetComponent<PathfindingAI>();
        attackReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            follow = true;
        }
        if (follow == true)
        {
            path.enabled = true;
            attackRadius.enabled = false;
            playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude < attackRange && attackReady == true)
        {
            StartCoroutine(windingUp());
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude > attackRange && attackReady == true && windUp == true)
        {
            follow = false;
        }


        if (windUp == true)
        {
            baseEnemy.aggroScript.aggro = false;
            path.enabled = false;
        }

        if (attack == true)
        {
            attackRadius.enabled = true;
        }
    }

    IEnumerator windingUp()
    {
        windUp = true;
        follow = false;
        yield return new WaitForSeconds(windUpTime);
        StartCoroutine(attacking());
    }

    IEnumerator attacking()
    {
        attack = true;
        windUp = false;
        yield return new WaitForSeconds(attackTime);
        attackRadius.enabled = false;
        attack = false;
        StartCoroutine(coolDown());
    }

    IEnumerator coolDown()
    {
        follow = true;
        attackReady = false;
        coolDownAttack = true;
        attackRadius.enabled = false;
        yield return new WaitForSeconds(coolDownTime);
        coolDownAttack = false;
        attackReady = true;
    }
}
