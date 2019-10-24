using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEnemy : MonoBehaviour
{

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

    public Transform target;

    public GameObject pillar;

    public BaseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        target = GameObject.FindGameObjectWithTag("Player1").transform;
        spawnPtUp.SetActive(false);
        spawnPtRight.SetActive(false);
        spawnPtDown.SetActive(false);
        spawnPtLeft.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (baseEnemy.aggroScript.currentTarget.position - baseEnemy.aggroScript.currentPos).normalized;

        if (direction.x > 0 && direction.y < 0)
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
        else if (direction.x < 0 && direction.y < 0)
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
        else if (direction.x > 0 && direction.y > 0)
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
        else if (direction.x < 0 && direction.y > 0)
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
