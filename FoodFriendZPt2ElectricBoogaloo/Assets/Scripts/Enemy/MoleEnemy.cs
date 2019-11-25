using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleEnemy : MonoBehaviour
{
    private Vector3 playerPos;

    public BoxCollider2D underground;
    public BoxCollider2D sideFlames;
    public BoxCollider2D aboveFlames;

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

    float aboutToJumpScaleX = 0.00001f;
    float aboutToJumpScaleY = 0.00001f;

    float originalScaleX = 1f;
    float originalScaleY = 1f;

    //scales for the side flames
    float sideScaleX = 4f;
    float sideScaleY = 0.1f;

    float originalSideScaleX = 0.1f;

    //scales for the up flames
    float upScaleX = 0.1f;
    float upScaleY = 4f;

    float originalUpScaleY = 0.1f;

    public PathfindingAI path;


    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        underground.enabled = true;
        sideFlames.enabled = false;
        aboveFlames.enabled = false;
        path = GetComponent<PathfindingAI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = baseEnemy.aggroScript.currentTarget.transform.position;

        if (baseEnemy.aggroScript.aggro == true && !aboutToJump && !jump && !confused)
        {
            under = true;
                }
        if (under == true) {
            baseEnemy.anim.SetBool("isBuried", true);
            confused = false;
            underground.enabled = false;

            path.enabled = true;
        }

        if ((baseEnemy.aggroScript.currentPos - playerPos).magnitude <= attackRange && !aboutToJump && !jump && !confused)
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
            aboveFlames.enabled = true;
            sideFlames.enabled = true;
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
        underground.size = new Vector2(aboutToJumpScaleX, aboutToJumpScaleY);
        yield return new WaitForSeconds(aboutToJumpTime);
        underground.size = new Vector2(originalScaleX, originalScaleY);
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
        sideFlames.size = new Vector2(sideScaleX, sideScaleY);
        aboveFlames.size = new Vector2(upScaleX, upScaleY);
        baseEnemy.aggroScript.enabled = false;
        yield return new WaitForSeconds(confusedTime);
        confused = false;
        canFollow = true;
        sideFlames.size = new Vector2(originalSideScaleX, sideScaleY);
        aboveFlames.size = new Vector2(upScaleX, originalUpScaleY);
    }
}
