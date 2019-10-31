using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEnemy : MonoBehaviour
{
    Vector3 playerPos;    

    public bool keepAway;
    public bool summoning;
    public bool push;

    public bool spawnUp;
    public bool spawnRight;
    public bool spawnDown;
    public bool spawnLeft;

    public GameObject spawnPtUp;
    public GameObject spawnPtRight;
    public GameObject spawnPtDown;
    public GameObject spawnPtLeft;

    public GameObject pillar;

    public BaseEnemy baseEnemy;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        rb = GetComponent<Rigidbody2D>();
        spawnPtUp.SetActive(false);
        spawnPtRight.SetActive(false);
        spawnPtDown.SetActive(false);
        spawnPtLeft.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = baseEnemy.aggroScript.currentTarget.transform.position;

        if (baseEnemy.aggroScript.aggro)
        {
            Vector3 direction = (baseEnemy.aggroScript.currentPos - playerPos).normalized;
            Vector2 force = direction * baseEnemy.speed * Time.deltaTime;
            rb.MovePosition(rb.position + force);

            if (direction.x >= 0 )
            {
                spawnPtRight.SetActive(false);
                spawnPtUp.SetActive(false);
                spawnPtDown.SetActive(false);
                spawnPtLeft.SetActive(true);
                spawnUp = false;
                spawnRight = false;
                spawnDown = false;
                spawnLeft = true;
            }
            else 
            {
                spawnPtRight.SetActive(true);
                spawnPtUp.SetActive(false);
                spawnPtDown.SetActive(false);
                spawnPtLeft.SetActive(false);
                spawnUp = false;
                spawnRight = true;
                spawnDown = false;
                spawnLeft = false;
            }
            if (direction.y >= 0 )
            {
                spawnPtRight.SetActive(false);
                spawnPtUp.SetActive(true);
                spawnPtDown.SetActive(false);
                spawnPtLeft.SetActive(false);
                spawnUp = true;
                spawnRight = false;
                spawnDown = false;
                spawnLeft = false;
            }
            else 
            {
                spawnPtRight.SetActive(false);
                spawnPtUp.SetActive(false);
                spawnPtDown.SetActive(true);
                spawnPtLeft.SetActive(false);
                spawnUp = false;
                spawnRight = false;
                spawnDown = true;
                spawnLeft = false;
            }
        }
    }
}
