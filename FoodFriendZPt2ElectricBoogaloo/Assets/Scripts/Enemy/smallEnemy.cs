using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemy : MonoBehaviour
{

    public enemyMovement move;
    public Rigidbody2D rb;
    public GameObject player;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, move.speed * Time.deltaTime);
    }
}
