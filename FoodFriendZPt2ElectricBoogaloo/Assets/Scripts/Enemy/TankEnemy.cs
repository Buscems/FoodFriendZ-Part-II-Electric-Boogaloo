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
    float attackScaleX = 4;
    float attackScaleY = 4;

    float originalScaleX = 1.51f;
    float originalScaleY = 1.27f;

    BaseEnemy baseEnemy;

    [HideInInspector]
    public PathfindingAI path;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        windUp = false;
        attack = false;
        baseEnemy = GetComponent<BaseEnemy>();
        path = GetComponent<PathfindingAI>();
        anim = GetComponent<Animator>();
        attackReady = true;
        attackRadius.size = new Vector2(originalScaleX, originalScaleY);
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
            if (baseEnemy.aggroScript.aggro)
            {
                playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
            }
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
        attackRadius.size = new Vector2(attackScaleX, attackScaleY);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(coolDown());
    }

    IEnumerator coolDown()
    {
        attack = false;
        follow = true;
        attackReady = false;
        coolDownAttack = true;
        attackRadius.size = new Vector2(originalScaleX, originalScaleY);
        yield return new WaitForSeconds(coolDownTime);
        coolDownAttack = false;
        attackReady = true;

    }
}
