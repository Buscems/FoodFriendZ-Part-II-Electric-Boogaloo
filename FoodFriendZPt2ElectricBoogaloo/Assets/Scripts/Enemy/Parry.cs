using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public GameObject ShieldTriggerZone;
    Vector3 playerPos;
    BaseEnemy baseEnemy;
    public GameObject Shield;
    [HideInInspector]
    public bool isBlocking;

    [Tooltip("Amount of Time the PARRY is down and the enemy is vulnerable")]
    public float ParryDownTime;
    [Tooltip("Amount of Time the PARRY is up and the enemy is vulnerable")]
    public float ParryUpTime;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        isBlocking = false;
        Shield.SetActive(false);

    }

    
    void Update()
    {

        if (baseEnemy.aggroScript.aggro)
        {
            isBlocking = true;
            if (isBlocking)
            {
                Shield.SetActive(true);
                Block();
            }

            if(isBlocking == false)
            {
                Shield.SetActive(false);
            }
        }

        /*if (baseEnemy.aggroScript.aggro)
        {
            playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
        }*/

        
    }

    private void LateUpdate()
    {
        //rb.moveposition(transform.position + vector3 direction * speed * time.deltatime);
    }

    void Block()
    {
        StartCoroutine(ParryDown());
    }

    IEnumerator ParryDown()
    {
        yield return new WaitForSeconds(ParryUpTime);
        Shield.SetActive(false);
        isBlocking = false;
        yield return new WaitForSeconds(ParryDownTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            baseEnemy.health = baseEnemy.health - 1;
        }

    }
}
