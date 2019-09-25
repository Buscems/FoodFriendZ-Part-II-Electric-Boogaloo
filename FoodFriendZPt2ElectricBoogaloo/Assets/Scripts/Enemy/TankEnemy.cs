using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    /*things i need for the script
     * 1. timer to track
     * 2. timer to wind up
     * 3. timer for AOE attack
      */

    public GameObject player;
    public enemyMovement move;
    private Vector3 playerPos;

    public bool follow;
    public bool windUp;
    public bool attack;

    public float followTime;
    public float windUpTime;
    public float attackTime;

    public BoxCollider2D attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        attackRadius.enabled = false;
        follow = true;
        StartCoroutine(following());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        if (follow == true){
            attackRadius.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, move.speed * Time.deltaTime);
        }
        
        if (attack == true){
            attackRadius.enabled = true;
        }
    }

    IEnumerator following(){
        follow = true;
        attack = false;
        yield return new WaitForSeconds(followTime);
        StartCoroutine(windingUp());
    }

    IEnumerator windingUp(){
        windUp = true;
        follow = false;
        yield return new WaitForSeconds(windUpTime);
        StartCoroutine(attacking());
    }

    IEnumerator attacking(){
        attack = true;
        windUp = false;
        yield return new WaitForSeconds(attackTime);
        attack = true;
        StartCoroutine(following());
    }
}
