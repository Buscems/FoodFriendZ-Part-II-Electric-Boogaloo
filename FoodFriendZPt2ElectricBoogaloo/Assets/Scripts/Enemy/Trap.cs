using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    MainPlayer mainPlayer;
    BaseEnemy baseEnemy;

    private float resetSpeed;
    public float TrapTime;
    private bool done;

    void Start()
    {
        mainPlayer = GetComponent<MainPlayer>();
        baseEnemy = GetComponent<BaseEnemy>();

        resetSpeed = mainPlayer.currentChar.Mspeed;
        done = false;
    }


    void Update()
    {

        if (done)
        {
            Debug.Log("self destruct");
            mainPlayer.health -= 1;
            Destroy(gameObject);
        }
    }

    IEnumerator Trapped()
    {
        Debug.Log("start trap");
        yield return new WaitForSeconds(TrapTime);
        Debug.Log("end trap");
        mainPlayer.currentChar.Mspeed = resetSpeed;
        done = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            mainPlayer.currentChar.Mspeed = 0;
            StartCoroutine(Trapped());
        }
    }
}
