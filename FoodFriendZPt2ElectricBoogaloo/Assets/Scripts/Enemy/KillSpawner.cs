using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSpawner : MonoBehaviour
{

    public GameObject enemy;
    public Rigidbody2D rb;
    public bool isDead;
    public float numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            isDead = true;
            for (int i = 0; i <= numEnemies; i++){
                var temp = Instantiate(enemy, transform.position, transform.rotation);
                temp.transform.position = new Vector3(Random.Range(temp.transform.position.x - 0.5f, temp.transform.position.x + 0.5f), Random.Range(temp.transform.position.y - 0.5f,temp.transform.position.y + 0.5f));
            }
            Destroy(gameObject);
        }
    }
}
