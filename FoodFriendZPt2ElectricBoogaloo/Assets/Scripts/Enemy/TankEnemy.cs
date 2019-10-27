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
        windUp = false;
        attack = false;
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

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude < attackRange && attackReady == true && follow == true)
        {
            StartCoroutine(windingUp());
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude > attackRange && attackReady == true && windUp == true)
        {
            follow = false;
        }


        if (windUp == true)
        {
            follow = false;
            baseEnemy.aggroScript.aggro = false;
            path.enabled = false;
        }

        if (attack == true)
        {
            attackRadius.enabled = true;
        }

        if (coolDownAttack == true){
            attackRadius.enabled = false;
        }
    }

    IEnumerator windingUp()
    {
        follow = false;
        windUp = true;
        yield return new WaitForSeconds(windUpTime);
        StartCoroutine(attacking());
    }

    IEnumerator attacking()
    {
        windUp = false;
        attack = true;
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(coolDown());
    }

    IEnumerator coolDown()
    {
        attack = false;
        follow = true;
        attackReady = false;
        coolDownAttack = true;
        attackRadius.enabled = false;
        yield return new WaitForSeconds(coolDownTime);
        coolDownAttack = false;
        attackReady = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "PLayer2" && attack == true)
        {
            StopCoroutine(attacking());
            StartCoroutine(coolDown());
        }
    }
}
