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

    public GameObject poof;

    MainPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        enemyTrue = new bool[enemies.Length];

        for (int i = 0; i < enemyTrue.Length; i++)
        {
            enemyTrue[i] = true;
            try
            {

                if (enemies[i].tag == "SpinningTurret" || enemies[i].tag == "Worm")
                {
                    enemies[i].transform.GetChild(0).GetComponent<Aggro>().doorEnemy = true;
                }
                else
                {
                    enemies[i].GetComponent<Aggro>().doorEnemy = true;
                }
                if (!enemies[i].GetComponent<BaseBoss>())
                {
                    enemies[i].SetActive(false);
                }
            }
            catch
            {

            }
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
            player = collision.GetComponent<MainPlayer>();
            player.canMove = false;
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<Animator>().SetBool("locked", true);
            }

            if (enemies[0].GetComponent<BaseBoss>() != null)
            {
                var boss = enemies[0].GetComponent<BaseBoss>();
                boss.playerEntered = true;
                for (int i = 0; i < enemies.Length; i++)
                {
                    boss.extraEnemies.Add(enemies[i]);
                }
            }
            else
            {
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(.1f);
            enemies[i].SetActive(true);
            Instantiate(poof, enemies[i].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(.5f);
        player.canMove = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].tag == "SpinningTurret" || enemies[i].tag == "Worm")
            {
                enemies[i].transform.GetChild(0).GetComponent<BaseEnemy>().aggroScript.aggro = true;
            }
            else
            {
                enemies[i].GetComponent<BaseEnemy>().aggroScript.aggro = true;
            }
        }
    }

}
