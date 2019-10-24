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

    private Vector3 playerPos;

    public bool follow;
    public bool windUp;
    public bool attack;

    public float followTime;
    public float windUpTime;
    public float attackTime;

    public BoxCollider2D attackRadius;

    BaseEnemy baseEnemy;

    public PathfindingAI path;

    // Start is called before the first frame update
    void Start()
    {
        attackRadius.enabled = false;
        StartCoroutine(following());
        baseEnemy = GetComponent<BaseEnemy>();
        path = GetComponent<PathfindingAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            if (follow == true)
            {
                path.enabled = true;
                attackRadius.enabled = false;
                baseEnemy.aggroScript.aggro = true;
                playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" && attack == true)
        {
            collision.GetComponent<MainPlayer>().GetHit(1);
        }
    }
}
