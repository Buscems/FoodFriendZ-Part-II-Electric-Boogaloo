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

    public bool following;
    public bool spawnPillar;
    public bool throwingPillar;
    public bool canSpawn;

    public float spawnTime;
    public float throwTime;
    public float rechargeTime;

    public GameObject spawnPtUp;
    public GameObject spawnPtRight;
    public GameObject spawnPtDown;
    public GameObject spawnPtLeft;

    public GameObject upTrigger;
    public GameObject rightTrigger;
    public GameObject leftTrigger;
    public GameObject downTrigger;

    public GameObject pillarLeft;
    public GameObject pillarRight;
    public GameObject pillarUp;
    public GameObject pillarDown;

    public BaseEnemy baseEnemy;

    public PathfindingAI path;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<PathfindingAI>();
        spawnPtUp.SetActive(false);
        spawnPtRight.SetActive(false);
        spawnPtDown.SetActive(false);
        spawnPtLeft.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = baseEnemy.aggroScript.currentTarget.transform.position;

        if (baseEnemy.aggroScript.aggro == true && canSpawn == true){
            StartCoroutine(spawningPillar());
        }


    }

    IEnumerator spawningPillar(){
        spawnPillar = true;
        path.enabled = false;
        canSpawn = false;
        if (spawnUp == true && spawnDown == false && spawnLeft == false && spawnRight == false){
            Instantiate(pillarUp, spawnPtUp.transform.position, Quaternion.identity);
            Debug.Log("ooofff");
        }
        if (spawnRight == true && spawnDown == false && spawnLeft == false && spawnUp == false)
        {
            Instantiate(pillarRight, spawnPtRight.transform.position, Quaternion.identity);
        }
        if (spawnPtDown == true && spawnUp == false && spawnLeft == false && spawnRight == false)
        {
            Instantiate(pillarDown, spawnPtDown.transform.position, Quaternion.identity);
        }
        if (spawnPtLeft == true && spawnDown == false && spawnUp == false && spawnRight == false)
        {
            Instantiate(pillarLeft, spawnPtLeft.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(throwPillar());
    }

    IEnumerator throwPillar(){
        spawnPillar = false;
        throwingPillar = true;
        yield return new WaitForSeconds(throwTime);
        StartCoroutine(recharge());
    }

    IEnumerator recharge(){
        throwingPillar = false;
        path.enabled = true;
        yield return new WaitForSeconds(rechargeTime);
        canSpawn = true;
    }
}