using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    [Tooltip("This is how many triggers need to be activated when the player gets into the room, to lock them in")]
    public GameObject[] doors;

    [Tooltip("How many enemies are in the room")]
    public GameObject[] enemies;

    public bool[] enemyTrue;
    bool close;

    // Start is called before the first frame update
    void Start()
    {
        enemyTrue = new bool[enemies.Length];

        for (int i = 0; i < enemyTrue.Length; i++)
        {
            enemyTrue[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i = 0; i <enemies.Length; i++)
        {
            if(enemies[i] == null)
            {
                enemyTrue[i] = false;
            }
        }

        for(int i = 0; i < enemyTrue.Length; i++)
        {
            if (enemyTrue[i])
            {
                break;
            }

            if(i == enemyTrue.Length-1)
            {
                close = true;
            }

        }

        if (close)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("locked", false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //needs fixing to add a second player
        if(collision.gameObject.tag == "Player1")
        {
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("locked", true);
            }

            try
            {
                if (enemies[0].GetComponent<BaseBoss>() != null)
                {
                    var boss = enemies[0].GetComponent<BaseBoss>();
                    boss.playerEntered = true;
                }
            }
            catch { }

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<BaseEnemy>().aggroScript.aggro == true)
                {

                }
            }

        }
    }



}
