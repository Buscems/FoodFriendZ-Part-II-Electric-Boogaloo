using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public enum SpawnType { SingleEnemy, TurnMultipleEnemy, RandomMultipleEnemy }
    [Tooltip("SingleEnemy is when you want only one enemy to spawn over and over \n \n" +
        "TurnMultipleEnemy is when you want multiple types of enemies to spawn but they will spawn in the order you add them to the array \n \n" +
        "RandomMultipleEnemy is when you want multiple types of enemies to spawn but they will spawn in a completely random order")]
    public SpawnType spawning;

    [Tooltip("This will be the enemy prefab that you want this spawner to spawn and it has a size in case you want to spawn multiple types of enemies")]
    public GameObject[] enemyType;

    [Tooltip("This is the min and max of the timer to delay enemy spawns. If you want the enemies to spawn at an even interval, make both numbers equal to each other")]
    public Vector2 spawnTimer;

    [Tooltip("This is how many enemies you want the spawner to spawn before destroying itself")]
    public int maxEnemies;

    Queue<GameObject> enemies = new Queue<GameObject>();

    public Aggro aggroScript;

    bool startSpawning;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < enemyType.Length; i++)
        {
            enemies.Enqueue(enemyType[i]);
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (aggroScript.aggro && !startSpawning)
        {
            //Debug.Log("Spawn");
            StartCoroutine(SpawnEnemies());
            startSpawning = true;
        }

    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimer.x, spawnTimer.y));
            if(spawning == SpawnType.SingleEnemy)
            {
                Instantiate(enemies.Peek(), transform.position, Quaternion.identity);
            }
            if (spawning == SpawnType.TurnMultipleEnemy)
            {
                Instantiate(enemies.Peek(), transform.position, Quaternion.identity);
                var current = enemies.Dequeue();
                enemies.Enqueue(current);
            }
            if (spawning == SpawnType.RandomMultipleEnemy)
            {
                Instantiate(enemyType[Random.Range(0, enemyType.Length)], transform.position, Quaternion.identity);
            }
        }

        Destroy(this.gameObject);
        
    }

}
