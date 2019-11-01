using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTrigger : MonoBehaviour
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
            pillar.spawnPtRight.SetActive(true);
            pillar.spawnPtUp.SetActive(false);
            pillar.spawnPtDown.SetActive(false);
            pillar.spawnPtLeft.SetActive(false);
            pillar.spawnUp = false;
            pillar.spawnRight = true;
            pillar.spawnDown = false;
            pillar.spawnLeft = false;
        }
    }
}
