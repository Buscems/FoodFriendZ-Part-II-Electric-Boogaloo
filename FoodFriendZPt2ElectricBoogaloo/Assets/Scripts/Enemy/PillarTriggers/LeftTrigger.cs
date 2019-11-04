using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTrigger : MonoBehaviour
{

    public PillarEnemy pillar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            pillar.baseEnemy.walkIntoDamage = 0;
            pillar.spawnPtRight.SetActive(false);
            pillar.spawnPtUp.SetActive(false);
            pillar.spawnPtDown.SetActive(false);
            pillar.spawnPtLeft.SetActive(true);
            pillar.spawnUp = false;
            pillar.spawnRight = false;
            pillar.spawnDown = false;
            pillar.spawnLeft = true;
        } else
        {
            pillar.baseEnemy.walkIntoDamage = 1;
        }
    }
}
