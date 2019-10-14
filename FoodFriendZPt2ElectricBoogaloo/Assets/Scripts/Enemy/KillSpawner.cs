using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSpawner : MonoBehaviour
{

    public GameObject enemy;
    public float numEnemies;

    [Tooltip("This is going to be how far away from the enemy, (up, down, left and right), that you want the small enemies to be able to be spawned")]
    public float rangeOfSpawn;

    BaseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(baseEnemy.health <= 0)
        {
            for (int i = 0; i <= numEnemies; i++)
            {
                var temp = Instantiate(enemy, transform.position, transform.rotation);
                temp.transform.position = new Vector3(Random.Range(temp.transform.position.x - rangeOfSpawn, temp.transform.position.x + rangeOfSpawn), Random.Range(temp.transform.position.y - rangeOfSpawn, temp.transform.position.y + rangeOfSpawn));
            }
        }
    }
}
