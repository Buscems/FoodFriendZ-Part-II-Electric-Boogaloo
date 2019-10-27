using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleEnemy : MonoBehaviour
{
    private Vector3 playerPos;

    public BoxCollider2D underground;

    public float attackRange;

    public bool under;
    public bool aboutToJump;
    public bool jump;
    public bool confused;
    public bool canFollow;

    public float underTime;
    public float aboutToJumpTime;
    public float jumpTime;
    public float confusedTime;

    BaseEnemy baseEnemy;

    public PathfindingAI path;


    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        underground.enabled = false;
        path = GetComponent<PathfindingAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baseEnemy.aggroScript.aggro == true && !aboutToJump && !jump && !confused)
        {
            under = true;
                }
        if (under == true) {
            baseEnemy.anim.SetBool("isBuried", true);
            confused = false;
            underground.enabled = false;
            playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
            path.enabled = true;
        }

        if ((baseEnemy.aggroScript.currentPos - playerPos).magnitude < attackRange && !aboutToJump && !jump && !confused)
        {
            StartCoroutine(readyingUp());
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude > attackRange && aboutToJump == true)
        {
            under = false;
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude > attackRange && jump == true)
        {
            under = false;
        }

        if ((playerPos - baseEnemy.aggroScript.currentPos).magnitude > attackRange && confused == true)
        {
            under = false;
        }

        if (aboutToJump == true){
            under = false;
            canFollow = false;
            path.enabled = false;
        }

        if (jump == true){
            aboutToJump = false;
            underground.enabled = true;
            baseEnemy.walkIntoDamage = 1;
        }

        if(confused == true){
            path.enabled = false;
            baseEnemy.walkIntoDamage = 0;
        }

        if (canFollow == true){
            path.enabled = true;
            baseEnemy.aggroScript.enabled = true;
        }
    }

    IEnumerator readyingUp(){
        //the timer where the enemy stops and winds up the jump
        aboutToJump = true;
        under = false;
        baseEnemy.aggroScript.aggro = false;
        yield return new WaitForSeconds(aboutToJumpTime);
        StartCoroutine(jumping());
    }

    IEnumerator jumping(){
        //ther timer where the enemy jumps from underground and the player can get hit
        under = false;
        baseEnemy.anim.SetBool("isBuried", false);
        jump = true;
        aboutToJump = false;
        yield return new WaitForSeconds(jumpTime);
        StartCoroutine(confusing());
    }

    IEnumerator confusing(){
        //the timer where the enemy is confused and can get hit
        under = false;
        confused = true;
        jump = false;
        path.enabled = false;
        baseEnemy.aggroScript.enabled = false;
        yield return new WaitForSeconds(confusedTime);
        confused = false;
        canFollow = true;
    }
}
