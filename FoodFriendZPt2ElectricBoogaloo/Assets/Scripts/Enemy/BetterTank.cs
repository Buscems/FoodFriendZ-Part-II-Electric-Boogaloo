using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterTank : MonoBehaviour
{
    private Vector3 playerPos;

    public float attackRange;

    public bool windUp;
    public bool attack;
    public bool attackReady;
    public bool coolDownAttack;

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
        baseEnemy = GetComponent<BaseEnemy>();
        path = GetComponent<PathfindingAI>();
        anim = GetComponent<Animator>();
        attackRadius.size = new Vector2(originalScaleX, originalScaleY);
        attackReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseEnemy.aggroScript.aggro){
            playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude < attackRange && attackReady == true)
        {
            StartCoroutine(windingUp());
            path.enabled = false;
        }
    }

    IEnumerator windingUp()
    {
        windUp = true;
        attackReady = false;
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
        coolDownAttack = true;
        path.enabled = true;
        attackRadius.size = new Vector2(originalScaleX, originalScaleY);
        yield return new WaitForSeconds(coolDownTime);
        coolDownAttack = false;
        attackReady = true;
    }
}
