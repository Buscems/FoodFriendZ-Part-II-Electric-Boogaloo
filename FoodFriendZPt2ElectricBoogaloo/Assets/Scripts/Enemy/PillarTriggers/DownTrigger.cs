using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownTrigger : MonoBehaviour
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
            pillar.spawnPtDown.SetActive(true);
            pillar.spawnPtLeft.SetActive(false);
            pillar.spawnUp = false;
            pillar.spawnRight = false;
            pillar.spawnDown = true;
            pillar.spawnLeft = false;
        }
        else
        {
            pillar.baseEnemy.walkIntoDamage = 1;
        }
    }
}
