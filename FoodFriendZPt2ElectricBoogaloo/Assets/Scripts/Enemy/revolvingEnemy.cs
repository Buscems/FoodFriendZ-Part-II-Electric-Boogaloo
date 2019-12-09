using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolvingEnemy : MonoBehaviour
{
    public GameObject[] enemy;
    public float numEnemies;

    [Tooltip("This is going to be how far away from the enemy, (up, down, left and right), that you want the small enemies to be able to be spawned")]
    public float rangeOfSpawn;

    public float spawnTime;

    BaseEnemy baseEnemy;

    public bool plezSpawn;

    public float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        plezSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (baseEnemy.aggroScript.aggro == true)
        {
            StartCoroutine(MoveFaster());
        }
    }

    IEnumerator MoveFaster(){
        yield return new WaitForSeconds(moveTime);
        baseEnemy.speed = 5f;
    }
}


    /*IEnumerator spawnProjectiles(){
        plezSpawn = false;
        int rand = Random.Range(0, 1);
        var temp = Instantiate(enemy[rand], transform.position, transform.rotation);
        temp.transform.position = new Vector3(Random.Range(temp.transform.position.x - rangeOfSpawn, temp.transform.position.x + rangeOfSpawn), Random.Range(temp.transform.position.y - rangeOfSpawn, temp.transform.position.y + rangeOfSpawn));
        yield return new WaitForSeconds(spawnTime);
    }
}*/
