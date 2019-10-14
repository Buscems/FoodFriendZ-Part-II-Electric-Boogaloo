using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public GameObject waterObj;
    public float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            collision.GetComponent<MainPlayer>().speed -= 2;
            Debug.Log("slowww");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            other.GetComponent<MainPlayer>().speed++;
            Debug.Log("fastttt");
        }
    }
}
