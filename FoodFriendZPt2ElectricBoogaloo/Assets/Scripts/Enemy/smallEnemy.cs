using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemy : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject player;
    private Vector3 playerPos;

    BaseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
    }
}
