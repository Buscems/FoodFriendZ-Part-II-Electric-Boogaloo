using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleEnemy : MonoBehaviour
{
    private Vector3 playerPos;

    public BoxCollider2D underground;

    public bool under;
    public bool aboutToJump;
    public bool jump;
    public bool confused;

    public float underTime;
    public float aboutToJumpTime;
    public float jumpTime;
    public float confusedTime;

    BaseEnemy baseEnemy;


    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        underground.enabled = false;
        StartCoroutine(following());
    }

    // Update is called once per frame
    void Update()
    {
        if (under == true)
        {
            if (baseEnemy.aggroScript.aggro == true)
            {
                underground.enabled = false;
                playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
            }
        }
        if (jump == true){
            underground.enabled = true;
            baseEnemy.aggroScript.aggro = false;
        }
    }

    IEnumerator following(){
        //the timer where the enemy is underground and follows the player
        under = true;
        confused = false;
        baseEnemy.aggroScript.aggro = true;
        yield return new WaitForSeconds(underTime);
        StartCoroutine(readyingUp());
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
        jump = true;
        aboutToJump = false;
        yield return new WaitForSeconds(jumpTime);
        StartCoroutine(confusing());
    }

    IEnumerator confusing(){
        //the timer where the enemy is confused and can get hit
        confused = true;
        jump = false;
        yield return new WaitForSeconds(confusedTime);
        StartCoroutine(following());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(1);
        }
    }
}
