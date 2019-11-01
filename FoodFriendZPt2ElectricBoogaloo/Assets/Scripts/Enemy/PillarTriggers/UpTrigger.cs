using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTrigger : MonoBehaviour
{

    public PillarEnemy pillar;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1"){
            pillar.spawnPtRight.SetActive(false);
            pillar.spawnPtUp.SetActive(true);
            pillar.spawnPtDown.SetActive(false);
            pillar.spawnPtLeft.SetActive(false);
            pillar.spawnUp = true;
            pillar.spawnRight = false;
            pillar.spawnDown = false;
            pillar.spawnLeft = false;
        }
    }
}
