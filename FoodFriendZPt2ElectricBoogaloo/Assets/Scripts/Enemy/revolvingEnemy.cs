using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolvingEnemy : MonoBehaviour
{ 
    public GameObject[] projectiles;
    public float numProjectiles;

    public float rangeOfSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= numProjectiles; i++)
        {
            int rand = Random.Range(0, 1);
            var temp = Instantiate(projectiles[rand], transform.position, transform.rotation);
            temp.transform.position = new Vector3(Random.Range(temp.transform.position.x - rangeOfSpawn, temp.transform.position.x + rangeOfSpawn), Random.Range(temp.transform.position.y - rangeOfSpawn, temp.transform.position.y + rangeOfSpawn));
        }
    }
}
